using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetCoreMvcFull.Utilities.Web
{
    public class WebResponseMessage
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public string ParamOne { get; set; }
        public string[] ArrayData { get; set; }
        public List<string[]> ArrayListData { get; set; }
        public object[] JsonData { get; set; }
    }
}
