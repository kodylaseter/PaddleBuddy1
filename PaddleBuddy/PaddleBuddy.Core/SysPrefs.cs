namespace PaddleBuddy.Core
{
    public class SysPrefs
    {
        //used for local server and genymotion
        //public static string ApiBase => "http://10.0.3.3:4000/api/mobile/";
        public static bool UseLocalDB => false;

        public static string ApiBase
        {
            get
            {
                return UseLocalDB
                    ? "http://10.0.3.3:4000/api/mobile/"
                    : "http://paddlebuddy-pbdb.rhcloud.com/api/mobile/";
            }
        }

        public static string Website => "http://paddlebuddy-pbdb.rhcloud.com";
    }
}
