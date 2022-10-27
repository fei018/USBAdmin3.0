using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using ToolsCommon;

namespace AgentLib
{
    public class AgentUpdate
    {
        // C:\ProgramData\USBAdmin
        private string _baseDir;

        private string _downloadDir;

        private string _setupExe;

        private string _zipFile;

        private static bool _IsUpdating = false;

        public AgentUpdate()
        {
            try
            {
                _baseDir = AgentRegistry.AgentDataDir;

                _downloadDir = Path.Combine(_baseDir, "download");

                _setupExe = Path.Combine(_downloadDir, "Setup.exe");

                _zipFile = Path.Combine(_downloadDir, "Release.zip");
            }
            catch (Exception ex)
            {
                throw new Exception("public AgentUpdate(): " + ex.GetBaseException().Message);
            }
        }

        #region + public static bool CheckNeedUpdate()
        public static bool CheckNeedUpdate()
        {
            try
            {
                if (_IsUpdating)
                {
                    return false;
                }

                var agentResult = AgentHttpHelp.HttpClient_Get(AgentRegistry.AgentConfigUrl);

                string newVersion = agentResult.AgentConfig.AgentVersion;

                // get local version
                var ver = AgentRegistry.AgentVersion;

                if (ver.Equals(newVersion, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                AgentLogger.Error("AgentUpdate.CheckNeedUpdate(): " + ex.Message);
                return false;
            }
        }
        #endregion

        #region + public void Update()
        public void Update()
        {
            try
            {
                _IsUpdating = true;

                CleanDownloadDir();

                DownloadFile();

                if (File.Exists(_setupExe))
                {
                    Process proc = Process.Start(_setupExe);
                }
                else
                {
                    throw new Exception("setup.exe download failed.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("AgentUpdate.Update(): " + ex.Message);
            }
            finally
            {
                _IsUpdating = false;
            }
        }
        #endregion

        #region + private void CleanDownloadDir()
        private void CleanDownloadDir()
        {
            try
            {
                if (Directory.Exists(_downloadDir))
                {
                    Directory.Delete(_downloadDir, true);
                }

                Directory.CreateDirectory(_downloadDir);
                UtilityTools.SetDirACL_AuthenticatedUsers_Modify(_downloadDir);
            }
            catch (Exception ex)
            {
                throw new Exception("AgentUpdate.CleanDownloadDir(): " + ex.Message);
            }
        }
        #endregion

        #region + private void DownloadFile()
        private void DownloadFile()
        {

            try
            {
                var agentResult = AgentHttpHelp.HttpClient_Get(AgentRegistry.AgentUpdateUrl);

                byte[] downloadCache = Convert.FromBase64String(agentResult.DownloadFileBase64); // convert base64String to byte[]

                using (FileStream fs = new FileStream(_zipFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    fs.Write(downloadCache, 0, downloadCache.Length);
                }

                if (!File.Exists(_zipFile))
                {
                    throw new Exception("File.Exists(_zipFile) failed.");
                }

                ZipFile.ExtractToDirectory(_zipFile, _downloadDir);
            }
            catch (Exception ex)
            {
                throw new Exception("AgentUpdate.DownloadFile(): " + ex.Message);
            }
        }
        #endregion

    }
}
