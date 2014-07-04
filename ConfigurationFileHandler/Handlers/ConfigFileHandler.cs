using ConfigurationFileHandler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationFileHandler.FileHandler
{
    public class ConfigFileHandler
    {
        string basePath;

        public ConfigFileHandler(string _BasePath)
        {
            basePath = _BasePath;
        }

        private bool checkExistenceofProject(string _projectName)
        {
            return System.IO.Directory.Exists(basePath + _projectName);
        }

        private string createDirectoryforProject(string _projectName)
        {
            System.IO.DirectoryInfo projectDirInfo = System.IO.Directory.CreateDirectory(basePath + _projectName);
            return projectDirInfo.FullName;
        }

        private bool checkExistenceofConfigFile(string _projectName)
        {
            return System.IO.File.Exists(basePath + _projectName + "/string_detector.config");
        }

        private string writeConfigFileforProject(string path, string config)
        {
            System.IO.File.WriteAllText(path, config);

            if(System.IO.File.Exists(path))
            {
                return path;
            }
            else
            {
                return string.Empty;
            }
        }



        public ActionResult<string> AddorUpdateConfig(string _projectName, string config)
        {
            if(!checkExistenceofProject(_projectName))  //no directory of confi file exist for this project
            {
                string projectPath = createDirectoryforProject(_projectName);
            }

            if(!checkExistenceofConfigFile(_projectName)) //currently don't care
            {

            }

            string configPath = writeConfigFileforProject(basePath + _projectName + "/string_detector.config", config);

            return ActionResultFactory.getStringResult(true, configPath);
        }

        public ActionResult<string> DeleteConfig(string _projectName)
        {
            //Delete configFile
            if(checkExistenceofConfigFile(_projectName))
            {
                System.IO.File.Delete(basePath + _projectName + "/string_detector.config");
            }

            //Delete Directory
            if (checkExistenceofProject(_projectName))
            {
                System.IO.Directory.Delete(basePath + _projectName);
            }

            return ActionResultFactory.getPlainResult(true);
        }

        public ActionResult<string> ReadConfigAsync(string _projectName)
        {
            string filePath = basePath + _projectName + "/string_detector.config";
            string content;
            using (System.IO.StreamReader fileStream = new System.IO.StreamReader(filePath))
            {
                Task<string> contentResult = fileStream.ReadToEndAsync();
                content = contentResult.Result;
            }
            return ActionResultFactory.getStringResult(true, content);
        }

    }
}
