namespace SearchAPI;

public class SearchLogic
{
    Database _database;
    readonly Dictionary<string, int> _words;

    public SearchLogic(Database database)
    {
        _database = database;
        _words = _database.GetAllWords();
    }

    public int GetIdOf(string word)
    {
        if (_words.TryGetValue(word, out int value))
            return value;
        return -1;
    }

    public List<KeyValuePair<int, int>> GetDocuments(List<int> wordIds)
    {
        return _database.GetDocuments(wordIds);
    }

    public List<string> GetDocumentDetails(List<int> docIds)
    {
        return _database.GetDocDetails(docIds);
    }
}
