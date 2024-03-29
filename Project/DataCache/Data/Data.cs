﻿using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCache.Data
{
    [ExcludeFromCodeCoverage]
    public class Data : IData
    {
        public Dictionary<Query, Dictionary<DateTime, List<IConsumption>>> queries { get; set; }
        public Data()
        {
            queries = new Dictionary<Query, Dictionary<DateTime, List<IConsumption>>>();
        }
    }
}
