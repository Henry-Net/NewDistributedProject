using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Common
{
    public class ResultList<T> : ResultCommon where T : class
    {
        public List<T> List { get; set; }
        public int Total { get; set; }
    }
}
