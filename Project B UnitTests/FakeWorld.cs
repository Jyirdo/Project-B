namespace ProjectB;

public class FakeWorld : IWorld
{
    private DateTime? _now = null;
    private int _linesRead = 0;
    private readonly Dictionary<string, int> _filesTimesRead = new();
    private readonly Dictionary<string, List<string>> _previousFiles = new();

    // You can override these in the code block after the constructor
    public DateTime Now
    {
        get => _now ?? throw new NullReferenceException();
        set => _now = value;
    }
    public List<string> LinesWritten { get; } = new();
    public List<string> LinesToRead { private get; set; } = new();
    public Dictionary<string, string> Files = new();
    public bool IncludeLinesReadInLinesWritten { get; set; } = false;

    public void WriteLine(string line)
    {
        LinesWritten.Add(line);
    }
    
    public string ReadLine()
    {
        string line = LinesToRead.ElementAt(_linesRead++);
        if (IncludeLinesReadInLinesWritten)
            WriteLine(line);
        return line;
    }

    public string ReadAllText(string path)
    {
        _filesTimesRead[path] = _filesTimesRead.GetValueOrDefault(path, 0) + 1;
        return Files[path];
    }

    public void WriteAllText(string path, string content)
    {
        if (!_previousFiles.ContainsKey(path))
            _previousFiles[path] = new();
        _previousFiles[path].Add(Files[path]);
        Files[path] = content;
    }

    private List<string> DebugInfoNow()
    {
        return new() {
            "--- Now",
            _now?.ToString("O") ?? "null"
        };
    }

    private List<string> DebugInfoLinesToRead()
    {
        return new() {
            $"--- LinesToRead",
            $"--- {LinesToRead.Count} lines",
            $"--- {(_linesRead == LinesToRead.Count ? "all" : "only")} {_linesRead} read",
            string.Join("\n", LinesToRead)
        };
    }

    private List<string> DebugInfoLinesWritten()
    {
        return new() {
            $"--- LinesWritten",
            $"--- {LinesWritten.Count} lines",
            $"--- {(IncludeLinesReadInLinesWritten ? "including" : "excluding")} lines read",
            string.Join("\n", LinesWritten)
        };
    }

    private List<string> DebugInfoFiles()
    {
        List<string> info = new() { $"--- Files ({Files.Count} files)" };
        foreach ((string path, string currentContent) in Files)
        {
            List<string> previousContents = _previousFiles.GetValueOrDefault(path, new());
            info.Add($"--- Files[{path}]");
            info.Add($"--- {_filesTimesRead.GetValueOrDefault(path, 0)} times read");
            info.Add($"--- {previousContents.Count} times written");
            info.Add($"--- {previousContents.Count + 1} versions");
            IEnumerable<(string, int)> indexedVersions = previousContents.Select((item, index) => (item, index));
            foreach ((string previousContent, int version) in indexedVersions)
            {
                info.Add($"--- version {version}");
                info.Add(previousContent);
            }
            if (previousContents.Count > 0)
                info.Add($"--- version {previousContents.Count}");
            info.Add(currentContent);
        }
        return info;
    }

    public override string ToString()
    {
        List<string> info = new();
        info.AddRange(DebugInfoNow());
        info.AddRange(DebugInfoLinesToRead());
        info.AddRange(DebugInfoLinesWritten());
        info.AddRange(DebugInfoFiles());
        info.Add("---");
        return string.Join("\n", info);
    }
}