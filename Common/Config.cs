using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Common
{
    public static class Config
    {
        public static string DatabasePath { get; } = "C:\\DLS\\SearchEngine.db";
        public static string DataSourcePath { get; } = "C:";
        public static int NumberOfFoldersToIndex { get; } = 0; // Use 0 or less for indexing all folders
    }
}