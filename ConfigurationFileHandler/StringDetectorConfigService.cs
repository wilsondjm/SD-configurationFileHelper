using ConfigurationFileHandler.FileHandler;
using ConfigurationFileHandler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationFileHandler
{
    public class StringDetectorConfigService : IConfigService
    {
        private string basePath;
        private ConfigFileHandler configHandler;

        public StringDetectorConfigService()
        {
            Init();
        }

        private void Init()
        {
            basePath = "C:/Projects/StringDetector/Configurations/";
            configHandler = new ConfigFileHandler(basePath);
        }

        public ActionResult<string> AddorUpdateConfiguration(string _projectName, string _configuration)
        {
            var result = configHandler.AddorUpdateConfig(_projectName, _configuration);

            return result;
        }

        public ActionResult<string> GetConfiguration(string _projectName)
        {
            ActionResult<string> configResult = configHandler.ReadConfigAsync(_projectName);

            return configResult;
        }

        public ActionResult<string> DeleteConfiguration(string _projectName)
        {
            ActionResult<string> deleteactionresult = configHandler.DeleteConfig(_projectName);

            return ActionResultFactory.getPlainResult(deleteactionresult.Result);
        }

    }
}
