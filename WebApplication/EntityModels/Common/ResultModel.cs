using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Common
{
    public class ResultModel<T> :ResultCommon where T:class
    {
        public T Model { get; set; }
    }
}
