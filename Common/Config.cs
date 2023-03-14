namespace Common;

public static class Config
{
    public static string DatabasePath { get; } = "/data/data.db";
    public static string DataSourcePath { get; } = "C:";
    public static int NumberOfFoldersToIndex { get; } = 0; // Use 0 or less for indexing all folders
}