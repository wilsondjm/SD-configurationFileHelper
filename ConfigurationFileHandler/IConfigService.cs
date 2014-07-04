using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationFileHandler
{
    public interface IConfigService
    {
        ConfigurationFileHandler.Utils.ActionResult<string> AddorUpdateConfiguration(string key, string config);
        ConfigurationFileHandler.Utils.ActionResult<string> DeleteConfiguration(string key);
        ConfigurationFileHandler.Utils.ActionResult<string> GetConfiguration(string key);
    }
}
