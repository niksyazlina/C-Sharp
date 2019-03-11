using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TEST_SYNC_FILE
{
    class ConfigReader
    {

        private string path;
        public string gs_OutputPath;
        public string gs_BatchFilePath;
        public string gs_LabFilePath;
        public string gs_solderOpenLabel;
        public string gs_LogFilePath;
        public int gi_WaitingTime;
        public string gs_AllowPrintFlag;
        public string gs_DeleteOutputFlag;
        public string gs_VerifySNFlag;
        public string gs_Verify2DFlag;
        public string gs_SuperUser;
        public string gs_UserPath;
        public string gs_UserPath2;
        public string gs_AllowActiveXFlag;
        public string gs_LicenseCheck;
        public string gs_ServerPath;


        public string gs_Schema;
        public int gi_TotalSchema;

        public string gs_dbUserID;

        internal double IniReadDouble(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        public string gs_dbPassword;
        public string gs_dbDataSource;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);

        public ConfigReader(string INIpath)
        {
            path = INIpath;
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        public int IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            if (temp.Length == 0)
                return -1;
            return Convert.ToInt16(temp.ToString());
        }

        public string IniReadString(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }
    }
}
