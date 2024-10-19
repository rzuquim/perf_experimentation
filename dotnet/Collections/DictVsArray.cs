using BenchmarkDotNet.Attributes;

namespace PerfExperimentation;

public class DictVsArray
{
    private MyClass[]? _array;
    private Dictionary<int, MyClass>? _intDict;
    private Dictionary<Guid, MyClass>? _guidDict;
    private Dictionary<string, MyClass>? _stringDict;
    private Dictionary<string, MyClass>? _insensitiveStringDict;

    private const int _maxN = 100;

    [Params(10, 20, 30, 40, 50, _maxN)]
    public int N;

    private int _needleIdx;

    private static MyClass[] _completeSet = new MyClass[100_000];
    private static Random _random = new Random();

    [GlobalSetup]
    public void SetupCompleteSet()
    {
        for (int i = 0; i < _maxN; i++)
        {
            _completeSet[i] = new MyClass
            {
                IntId = i,
                GuidId = Guid.NewGuid(),
                Name = $"Name {i:N5}",
            };
        }
    }

    [IterationSetup]
    public void SetupCollections()
    {
        _array = new MyClass[N];
        _intDict = new Dictionary<int, MyClass>(N);
        _guidDict = new Dictionary<Guid, MyClass>(N);
        _stringDict = new Dictionary<string, MyClass>(N);
        _insensitiveStringDict = new Dictionary<string, MyClass>(N, StringComparer.OrdinalIgnoreCase);

        for (int i = 0; i < N; i++)
        {
            var item = _completeSet[i];
            _array[i] = item;
            _intDict[item.IntId] = item;
            _guidDict[item.GuidId] = item;
            _stringDict[item.Name] = item;
            _insensitiveStringDict[item.Name] = item;
        }

        _needleIdx = _random.Next(0, N);
    }

    [Benchmark]
    public void ArrayIntKey()
    {
        var needle = _completeSet[_needleIdx];

        for (int i = 0; i < N; i++)
        {
            var candidate = _array![i];
            if (candidate.IntId == needle.IntId)
            {
                if (candidate != needle)
                {
                    throw new Exception("Invalid found!");
                }
                return;
            }
        }

        throw new Exception("Could not find needle");
    }

    [Benchmark]
    public void DictionaryIntKey()
    {
        var needle = _completeSet[_needleIdx];
        if (_intDict!.TryGetValue(needle.IntId, out var found))
        {
            if (found != needle)
            {
                throw new Exception("Invalid found!");
            }
            return;
        }

        throw new Exception("Could not find needle");
    }

    [Benchmark]
    public void ArrayGuidKey()
    {
        var needle = _completeSet[_needleIdx];

        for (int i = 0; i < N; i++)
        {
            var candidate = _array![i];
            if (candidate.GuidId == needle.GuidId)
            {
                if (candidate != needle)
                {
                    throw new Exception("Invalid found!");
                }
                return;
            }
        }

        throw new Exception("Could not find needle");
    }

    [Benchmark]
    public void DictionaryGuidKey()
    {
        var needle = _completeSet[_needleIdx];
        if (_guidDict!.TryGetValue(needle.GuidId, out var found))
        {
            if (found != needle)
            {
                throw new Exception("Invalid found!");
            }
            return;
        }

        throw new Exception("Could not find needle");
    }

    [Benchmark]
    public void ArrayStringKey()
    {
        var needle = _completeSet[_needleIdx];

        for (int i = 0; i < N; i++)
        {
            var candidate = _array![i];
            if (candidate.Name == needle.Name)
            {
                if (candidate != needle)
                {
                    throw new Exception("Invalid found!");
                }
                return;
            }
        }

        throw new Exception("Could not find needle");
    }

    [Benchmark]
    public void DictionaryStringKey()
    {
        var needle = _completeSet[_needleIdx];
        if (_stringDict!.TryGetValue(needle.Name, out var found))
        {
            if (found != needle)
            {
                throw new Exception("Invalid found!");
            }
            return;
        }

        throw new Exception("Could not find needle");
    }

    [Benchmark]
    public void ArrayInsensitiveKey()
    {
        var needle = _completeSet[_needleIdx];

        for (int i = 0; i < N; i++)
        {
            var candidate = _array![i];
            if (String.Equals(candidate.Name, needle.Name, StringComparison.OrdinalIgnoreCase))
            {
                if (candidate != needle)
                {
                    throw new Exception("Invalid found!");
                }
                return;
            }
        }

        throw new Exception("Could not find needle");
    }

    [Benchmark]
    public void DictionaryInsensitiveKey()
    {
        var needle = _completeSet[_needleIdx];
        if (_insensitiveStringDict!.TryGetValue(needle.Name, out var found))
        {
            if (found != needle)
            {
                throw new Exception("Invalid found!");
            }
            return;
        }

        throw new Exception("Could not find needle");
    }


    public class MyClass
    {
        public int IntId { get; init; }
        public Guid GuidId { get; init; }
        public string Name { get; init; } = null!;
    }
}

