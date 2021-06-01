using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Connection;
using DataAccess.Model;
using DataAccess.Utils;

namespace DataAccess.DAO.Implementation
{
    public class ConsumptionDAOImpl : IConsumptionDAO
    {
        public List<Consumption> Read(string reg, DateTime day)
        {
            string query = "select * from databasetable where regija = :regija and datum = :datum"; // upit za search
            List<Consumption> searchedItems = new List<Consumption>();


            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {

                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "regija", DbType.String, 20);
                    ParameterUtil.AddParameter(command, "datum", DbType.String, 20);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "regija", reg);
                    ParameterUtil.SetParameterValue(command, "datum", day.ToString("dd-MMMM-yyyy"));//moze biti problem mozda kast u string i onda gore to string

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Consumption oneConsumption = new Consumption(reader.GetInt32(0), reader.GetInt32(1),
                                reader.GetString(2), reader.GetDateTime(3));
                            searchedItems.Add(oneConsumption);
                        }
                    }
                }
            }
            //ovdje iz za upis u audit tabelu
            if (searchedItems.Count == 0)
                searchedItems = null;
            return searchedItems;
        }

        public Dictionary<DateTime, List<Consumption>> Read(string reg, List<DateTime> days)
        {
            Dictionary<DateTime, List<Consumption>> wantedData = new Dictionary<DateTime, List<Consumption>>();

            foreach(var day in days)
            {
                List<Consumption> oneDayConsumption = new List<Consumption>();
                oneDayConsumption = Read(reg, day);

                if(oneDayConsumption == null)//ako je razlicito oda 24 cao papi
                {
                    //wantedData = null;
                    //return wantedData;
                //    wantedData.Add(day, null);
                }
                else
                {
                    wantedData.Add(day, oneDayConsumption);
                }

            }
            return wantedData;
        }

        public void SaveError(string message)
        {
            string query = "insert into audittable (greska,datum) values(:greska,:datum)";
            DateTime time = DateTime.Now;
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "greska", DbType.String, 50);
                    ParameterUtil.AddParameter(command, "datum", DbType.String, 20);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "greska", message);
                    ParameterUtil.SetParameterValue(command, "datum", time.ToString("dd-MMMM-yyyy"));//vjr neso oko ovog ce biti problem
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Write(List<Consumption> newData)
        {
            bool ret = false;
            if (Read(newData[0].Region, newData[0].Day) != null)
            {
                SaveError("Date for that region and date already exist");
                return ret;
            }

             // u find country ce dodati     
            
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                FindCountry(newData[0].Region, connection);
                foreach (var data in newData)
                {
                    ret = Write(data, connection);//ako dole foreach ne radi onda ovdje ga pozvati za svaki
                    if (!ret)
                        break;
                }
            }

            return ret;
        }
        

        public bool Write(Consumption newData, IDbConnection connection)
        {
            bool ret = false;
            string insert = "insert into databasetable (br,potrosnja,regija,datum) values (:br,:potrosnja,:regija,:datum)"; 

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = insert;

                ParameterUtil.AddParameter(command, "br", DbType.Int32);
                ParameterUtil.AddParameter(command, "potrosnja", DbType.Int32);
                ParameterUtil.AddParameter(command, "regija", DbType.String, 20);
                ParameterUtil.AddParameter(command, "datum", DbType.String, 20);

                command.Prepare();

                ParameterUtil.SetParameterValue(command, "br", newData.Hour);
                ParameterUtil.SetParameterValue(command, "potrosnja", newData.Load);
                ParameterUtil.SetParameterValue(command, "regija", newData.Region);
                ParameterUtil.SetParameterValue(command, "datum", newData.Day.ToString("dd-MMMM-yyyy"));

                command.ExecuteNonQuery();
                ret = true;
            }

            return ret;
        }

        public List<string> FindAllCountrys()
        {
            List<string> allCountrys = new List<string>();
            string query = "select * from countrys";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allCountrys.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return allCountrys;
        }

        public string FindCountry(string reg,IDbConnection connection)
        {
            string ret = "";
            string check = "select * from countrys where regija = :regija";

      //      using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
        //    {

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = check;

                    ParameterUtil.AddParameter(command, "regija", DbType.String, 20);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "regija", reg);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())//oVJDE NISTA NE CITAA TI BOGA
                        {
                            ret = reader.GetString(0);
                            break;
                        }
                    }
                }
            
            if (ret == "")
                AddCountry(reg);
            return ret;
        }

        public void AddCountry(string reg)
        {
            string query = "insert into countrys (name,regija) values(:name,:regija)";
            DateTime time = DateTime.Now;
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "name", DbType.String, 20);
                    ParameterUtil.AddParameter(command, "regija", DbType.String, 20);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "name", reg);
                    ParameterUtil.SetParameterValue(command, "regija", reg);
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
