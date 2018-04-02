using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEquipmentBLL.ErrorLog
{
    class ErrorLogger
    {
        public static string BLLErrorLog = ConfigurationManager.AppSettings.Get("BLLErrorLog");

        public static void LogExceptions(Exception exception)
        {
            string logInformation = "Date:" + DateTime.Now + "/nMethod name: " + exception.TargetSite
                + "/nStack trace: " + exception.StackTrace + "/n-------------------------------------------------/n";

            LogError(logInformation);
        }

        public static void LogError(string logInformation)
        {
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(BLLErrorLog, true);
                writer.Write(logInformation);
            }
            catch (Exception writerException)
            {
                throw writerException;
            }
            finally
            {
                writer.Close();
                writer.Dispose();
            }
        }
    }
}
