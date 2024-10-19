using BenchmarkDotNet.Attributes;

namespace PerfExperimentation;

[MemoryDiagnoser]
public class EnumerableResolution
{
    [Params(10, 100, 1_000, 10_000, 100_000)]
    public int N;

    [Params(ObjSize.Int, ObjSize.SmallStruct, ObjSize.SmallClass, ObjSize.MediumClass, ObjSize.BigClass)]
    public ObjSize Size;

    [Benchmark(Baseline = true)]
    public void ToArray()
    {
        var count = Size switch
        {
            ObjSize.Int => YieldInts(N).ToArray().Length,
            ObjSize.SmallStruct => YieldSmallStructs(N).ToArray().Length,
            ObjSize.SmallClass => YieldSmallClasses(N).ToArray().Length,
            ObjSize.MediumClass => YieldMediumObjs(N).ToArray().Length,
            ObjSize.BigClass => YieldBigClasses(N).ToArray().Length,
            _ => throw new ArgumentOutOfRangeException(),
        };

        if (count != N)
        {
            throw new Exception("This should not happen");
        }
    }

    [Benchmark]
    public void ToList()
    {
        var count = Size switch
        {
            ObjSize.Int => YieldInts(N).ToList().Count,
            ObjSize.SmallStruct => YieldSmallStructs(N).ToArray().Length,
            ObjSize.SmallClass => YieldSmallClasses(N).ToList().Count,
            ObjSize.MediumClass => YieldMediumObjs(N).ToList().Count,
            ObjSize.BigClass => YieldBigClasses(N).ToArray().Length,
            _ => throw new ArgumentOutOfRangeException(),
        };

        if (count != N)
        {
            throw new Exception("This should not happen");
        }
    }

    public enum ObjSize
    {
        Int,
        SmallClass,
        SmallStruct,
        MediumClass,
        BigClass,
    }

    public struct SmallStruct
    {
        public SmallStruct(int id, string name, double price, bool isAvailable, char symbol)
        {
            (Id, Name, Price, IsAvailable, Symbol) = (id, name, price, isAvailable, symbol);
        }

        public int Id { get; }
        public string Name { get; }
        public double Price { get; }
        public bool IsAvailable { get; }
        public char Symbol { get; }
    }

    public class SmallClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public char Symbol { get; set; }
    }

    public class MediumClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public char Symbol { get; set; }
        public byte Level { get; set; }
        public float Rating { get; set; }
        public long Views { get; set; }
        public short Rank { get; set; }
        public decimal Discount { get; set; }

        public int? NullableId { get; set; }
        public double? NullablePrice { get; set; }
        public bool? NullableIsAvailable { get; set; }
        public char? NullableSymbol { get; set; }
        public byte? NullableLevel { get; set; }
        public float? NullableRating { get; set; }
        public long? NullableViews { get; set; }
        public short? NullableRank { get; set; }
        public decimal? NullableDiscount { get; set; }
    }

    public class BigClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public char Symbol { get; set; }
        public byte Level { get; set; }
        public float Rating { get; set; }
        public long Views { get; set; }
        public short Rank { get; set; }
        public decimal Discount { get; set; }

        public int? NullableId { get; set; }
        public double? NullablePrice { get; set; }
        public bool? NullableIsAvailable { get; set; }
        public char? NullableSymbol { get; set; }
        public byte? NullableLevel { get; set; }
        public float? NullableRating { get; set; }
        public long? NullableViews { get; set; }
        public short? NullableRank { get; set; }
        public decimal? NullableDiscount { get; set; }

        public string Description { get; set; } = null!;
        public string Url { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan? OptionalDuration { get; set; }

        public StatusType Status { get; set; }
        public StatusType? NullableStatus { get; set; }

        public Guid UniqueId { get; set; }
        public Guid? NullableUniqueId { get; set; }

        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public bool IsPremium { get; set; }
    }

    public enum StatusType
    {
        Active,
        Inactive,
        Pending,
        Suspended,
    }

    private static readonly Random _random = new();

    private static IEnumerable<int> YieldInts(int n)
    {
        for (int i = 0; i < n; i++)
        {
            yield return i;
        }
    }

    private static IEnumerable<SmallStruct> YieldSmallStructs(int n)
    {
        for (int i = 0; i < n; i++)
        {
            yield return new SmallStruct(
                id: i,
                name: $"Name {i:N5}",
                price: Math.Round(_random.NextDouble() * 100, 2),
                isAvailable: _random.Next(2) == 0,
                symbol: (char)_random.Next(65, 91)
            );
        }
    }

    private static IEnumerable<SmallClass> YieldSmallClasses(int n)
    {
        for (int i = 0; i < n; i++)
        {
            yield return new SmallClass
            {
                Id = i,
                Name = $"Name {i:N5}",
                Price = Math.Round(_random.NextDouble() * 100, 2),
                IsAvailable = _random.Next(2) == 0,
                Symbol = (char)_random.Next(65, 91),
            };
        }
    }

    private static IEnumerable<MediumClass> YieldMediumObjs(int n)
    {
        for (int i = 0; i < n; i++)
        {
            yield return new MediumClass
            {
                Id = i,
                Name = $"Name {i:N5}",
                Price = Math.Round(_random.NextDouble() * 100, 2),
                IsAvailable = _random.Next(2) == 0,
                Symbol = (char)_random.Next(65, 91),
                Level = (byte)_random.Next(1, 10),
                Rating = (float)Math.Round(_random.NextDouble() * 5, 1),
                Views = _random.Next(0, int.MaxValue),
                Rank = (short)_random.Next(1, 100),
                Discount = Math.Round((decimal)_random.NextDouble() * 30, 2),

                NullableId = _random.Next(2) == 0 ? null : _random.Next(),
                NullablePrice = _random.Next(2) == 0 ? null : (double?)Math.Round(_random.NextDouble() * 100, 2),
                NullableIsAvailable = _random.Next(2) == 0 ? null : (bool?)(_random.Next(2) == 0),
                NullableSymbol = _random.Next(2) == 0 ? null : (char?)_random.Next(65, 91),
                NullableLevel = _random.Next(2) == 0 ? null : (byte?)_random.Next(1, 10),
                NullableRating = _random.Next(2) == 0 ? null : (float?)Math.Round(_random.NextDouble() * 5, 1),
                NullableViews = _random.Next(2) == 0 ? null : (long?)_random.Next(0, int.MaxValue),
                NullableRank = _random.Next(2) == 0 ? null : (short?)_random.Next(1, 100),
                NullableDiscount = _random.Next(2) == 0
                    ? null
                    : (decimal?)Math.Round((decimal)_random.NextDouble() * 30, 2),
            };
        }
    }

    private static IEnumerable<BigClass> YieldBigClasses(int n)
    {
        for (int i = 0; i < n; i++)
        {
            yield return new BigClass
            {
                Id = i,
                Name = $"Name{i}",
                Price = Math.Round(_random.NextDouble() * 100, 2),
                IsAvailable = _random.Next(2) == 0,
                Symbol = (char)_random.Next(65, 91),
                Level = (byte)_random.Next(1, 10),
                Rating = (float)Math.Round(_random.NextDouble() * 5, 1),
                Views = _random.Next(0, int.MaxValue),
                Rank = (short)_random.Next(1, 100),
                Discount = Math.Round((decimal)_random.NextDouble() * 30, 2),

                NullableId = _random.Next(2) == 0 ? null : _random.Next(),
                NullablePrice = _random.Next(2) == 0 ? null : (double?)Math.Round(_random.NextDouble() * 100, 2),
                NullableIsAvailable = _random.Next(2) == 0 ? null : (bool?)(_random.Next(2) == 0),
                NullableSymbol = _random.Next(2) == 0 ? null : (char?)_random.Next(65, 91),
                NullableLevel = _random.Next(2) == 0 ? null : (byte?)_random.Next(1, 10),
                NullableRating = _random.Next(2) == 0 ? null : (float?)Math.Round(_random.NextDouble() * 5, 1),
                NullableViews = _random.Next(2) == 0 ? null : (long?)_random.Next(0, int.MaxValue),
                NullableRank = _random.Next(2) == 0 ? null : (short?)_random.Next(1, 100),
                NullableDiscount = _random.Next(2) == 0 ? null : (decimal?)Math.Round((decimal)_random.NextDouble() * 30, 2),

                Description = $"Description{i}",
                Url = $"https://example.com/resource/{i}",

                CreatedAt = DateTime.Now.AddDays(-_random.Next(1000)), // Random date within the last ~3 years
                UpdatedAt = _random.Next(2) == 0 ? null : DateTime.Now.AddDays(-_random.Next(1000)),
                Duration = TimeSpan.FromMinutes(_random.Next(60, 1440)), // Random duration between 1 hour and 24 hours
                OptionalDuration = _random.Next(2) == 0 ? null : (TimeSpan?)TimeSpan.FromMinutes(_random.Next(60, 1440)),

                Status = (StatusType)_random.Next(4),
                NullableStatus = _random.Next(2) == 0 ? null : (StatusType?)_random.Next(4),

                UniqueId = Guid.NewGuid(),
                NullableUniqueId = _random.Next(2) == 0 ? null : (Guid?)Guid.NewGuid(),

                IsActive = _random.Next(2) == 0,
                IsVerified = _random.Next(2) == 0,
                IsPremium = _random.Next(2) == 0
            };
        }
    }
}
