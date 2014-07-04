using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Tracing;

namespace WebApplication1.Utility
{
    public class CustomizedTraceWriter : ITraceWriter
    {
        string logFilePath = "C:\\inetpub\\logs\\LogFiles\\TraceLog[STARTTIME].log";

        public CustomizedTraceWriter()
        {
            logFilePath = logFilePath.Replace("[STARTTIME]", System.DateTime.Now.ToString("MM_dd_yyyy-HH_mm"));
        }

        public void Trace(System.Net.Http.HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            try { 
            var record = new TraceRecord(request, category, level);
            traceAction(record);
            //string basePath = HttpContext.Current.Server.MapPath("");
            //string path = HttpContext.Current.Server.MapPath("~/TraceLog.log");
            File.AppendAllText(logFilePath, "[" + record.Level + "]:[" + record.Timestamp + ":[" + record.Status + "]:[" + record.Kind + "]:[" + record.Operator + "]:[" + record.Operation + "]:[Message#" + record.Message + "#]\r\n");
            } catch (Exception e)
            {
                //ignore
            }
        }
    }
}