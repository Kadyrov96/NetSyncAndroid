using System.Collections.Generic;

namespace NetSync_Mobile
{
    public static class AppData
    {
        public static string ServerIPAddress { get; set; }
        public static string ServerHostname { get; set; }
        public static string SelectedFolderPath { get; set; }
        public static string Message1 { get; set; }
        public static string Message2 { get; set; }
        public static string Message3 { get; set; }
        public static List<string> CoflictFilesList { get; set; }
    }
}