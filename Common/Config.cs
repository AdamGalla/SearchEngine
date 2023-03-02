using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Common
{
    public static class Config
    {
        public static string DatabasePath { get; } = @"C:\Program Files (x86)\SQLite\SearchEngine.db";
        public static string DataSourcePath { get; } = "C:\\Users\\adoga\\OneDrive\\Dokumenty\\DataSets\\enron_mail_20150507\\maildir";
        public static int NumberOfFoldersToIndex { get; } = 0; // Use 0 or less for indexing all folders
    }
}