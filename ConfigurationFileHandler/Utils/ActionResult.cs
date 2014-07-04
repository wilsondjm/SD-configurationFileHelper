using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationFileHandler.Utils
{
    public class ActionResult<T> 
    {
        public bool Result { set; get; }
        public T Data { set; get; }

        public ActionResult(bool _Result)
        {
            Result = _Result;
        }

        public ActionResult(bool _Result, T _Data)
        {
            Result = _Result;
            Data = _Data;
        }
    }

    static class ActionResultFactory
    {
        public static ActionResult<string> getStringResult(bool _Result, string _Data)
        {
            return new ActionResult<string>(_Result, _Data);
        }

        public static ActionResult<string> getPlainResult(bool _Result)
        {
            return new ActionResult<string>(_Result);
        }
    }
}
