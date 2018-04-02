using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace SoundEquipment.ErrorLog
{
    public class ErrorLogger
    {
        public static string PresentationErrorLog = ConfigurationManager.AppSettings.Get("PresentationErrorLog");

        public static void LogExceptions(Exception exception)
        {
            string logInformation = "Date: " + DateTime.Now + "\r\nMethod name: " + exception.TargetSite
                + "\r\nStack trace: " + exception.StackTrace + "\r\n\r\n" + "--------------------------------" + "\r\n";

            LogError(logInformation);
        }

        public static void LogError(string logInformation)
        {
            StreamWriter writer = null;
            
            try
            {
                writer = new StreamWriter(PresentationErrorLog, true);
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