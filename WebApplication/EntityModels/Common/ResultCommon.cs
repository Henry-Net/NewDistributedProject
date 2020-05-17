using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Common
{
    public class ResultCommon
    {
        public CommonCode Code { get; set; }
        public string Message { get; set; }
    }

    public enum CommonCode 
    {
        Successful = 1,
        Failure = 2,
        Error = 3,
        Validation = 4
    }
}
