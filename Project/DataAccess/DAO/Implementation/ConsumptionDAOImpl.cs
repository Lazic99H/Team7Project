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
            string query = "select * from databasetable where regija = ':regija' and datum = ':datum'; "; // upit za search

            List<Consumption> searchedItems = new List<Consumption>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "regija", DbType.String, 20);
                    ParameterUtil.AddParameter(command, "datum", DbType.Date);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "regija", reg);
                    ParameterUtil.SetParameterValue(command, "datum", day);//moze biti problem mozda kast u string i onda gore to string

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
            return searchedItems;
        }

        public Dictionary<DateTime, List<Consumption>> Read(string reg, List<DateTime> days)
        {
            Dictionary<DateTime, List<Consumption>> wantedData = new Dictionary<DateTime, List<Consumption>>();

            foreach(var day in days)
            {
                List<Consumption> oneDayConsumption = new List<Consumption>();
                oneDayConsumption = Read(reg, day);

                if(oneDayConsumption.Count != 24)//ako je razlicito oda 24 cao papi
                {
                    wantedData = null;
                    return wantedData;
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
            string query = "insert into audittable (greska,datum) values(:greska,:datum);";
            DateTime time = DateTime.Now;
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "greska", DbType.String, 50);
                    ParameterUtil.AddParameter(command, "datum", DbType.Date);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "greska", message);
                    ParameterUtil.SetParameterValue(command, "datum", time);//vjr neso oko ovog ce biti problem
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Write(List<Consumption> newData)
        {
            bool ret = false;
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                ret = Write(newData, connection);//ako dole foreach ne radi onda ovdje ga pozvati za svaki
            }

            return ret;
        }

        public bool Write(List<Consumption> newData,IDbConnection connection)
        {
            bool ret = false;
            string insert = "insert into databasetable (br,potrosnja,regija,datum)" + "values (:br,:potrosnja,:regija,:datum)"; //inser za datum mozda nije dobar

            using (IDbCommand command = connection.CreateCommand())
            {
                if(Read(newData[0].Region,newData[0].Day) != null)
                {
                    SaveError("Values for that region and time allready exist");
                    return ret;
                }
                else
                {
                    foreach (var consumption in newData)
                    {
                        command.CommandText = insert;

                        ParameterUtil.AddParameter(command, "br", DbType.Int32);
                        ParameterUtil.AddParameter(command, "potrosnja", DbType.Int32);
                        ParameterUtil.AddParameter(command, "regija", DbType.String, 20);
                        ParameterUtil.AddParameter(command, "datum", DbType.Date);//ovo mozda nije dobro

                        command.Prepare();

                        ParameterUtil.SetParameterValue(command, "br", consumption.Day);
                        ParameterUtil.SetParameterValue(command, "potrosnja", consumption.Load);
                        ParameterUtil.SetParameterValue(command, "regija", consumption.Region);
                        ParameterUtil.SetParameterValue(command, "datum", consumption.Region); // ovo mozda nije dobro

                        command.ExecuteNonQuery();
                    }
                }


                command.ExecuteNonQuery();
            }

            return ret;
        }
   
    }
}
