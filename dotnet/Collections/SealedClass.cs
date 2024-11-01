using BenchmarkDotNet.Attributes;

namespace PerfExperimentation;

public class SealedClassPerf
{
    private readonly RegularClassUnderTest _regularClass = new();
    private readonly RegularClassVirtualMethodUnderTest _regularClassVirtualMethod = new();
    private readonly SealedClassUnderTest _sealedClass = new();
    private readonly SealedInheritanceUnderTest _sealedInheritanceClass = new();
    private readonly RegularInheritanceUnderTest _regularInheritanceClass = new();

    [Params(100)]
    public int N;

    [Benchmark(Baseline = true)]
    public void RegularClass()
    {
        int sum = 0;
        for (int i = 0; i < N; i++)
        {
            sum += _regularClass.RestByTwo(i);
        }

        if (sum != N / 2)
        {
            throw new Exception($"Expected {N / 2} got {sum}");
        }
    }

    [Benchmark]
    public void RegularClassVirtualMethod()
    {
        int sum = 0;
        for (int i = 0; i < N; i++)
        {
            sum += _regularClassVirtualMethod.RestByTwo(i);
        }

        if (sum != N / 2)
        {
            throw new Exception($"Expected {N / 2} got {sum}");
        }
    }

    [Benchmark]
    public void SealedClass()
    {
        int sum = 0;
        for (int i = 0; i < N; i++)
        {
            sum += _sealedClass.RestByTwo(i);
        }

        if (sum != N / 2)
        {
            throw new Exception($"Expected {N / 2} got {sum}");
        }
    }

    [Benchmark]
    public void SealedInheritance()
    {
        int sum = 0;
        for (int i = 0; i < N; i++)
        {
            sum += _sealedInheritanceClass.RestByTwo(i);
        }

        if (sum != N / 2)
        {
            throw new Exception($"Expected {N / 2} got {sum}");
        }
    }

    [Benchmark]
    public void RegularInheritance()
    {
        int sum = 0;
        for (int i = 0; i < N; i++)
        {
            sum += _regularInheritanceClass.RestByTwo(i);
        }

        if (sum != N / 2)
        {
            throw new Exception($"Expected {N / 2} got {sum}");
        }
    }

    public class RegularClassUnderTest
    {
        public int RestByTwo(int i) => i % 2;
    }

    public class RegularClassVirtualMethodUnderTest
    {
        public virtual int RestByTwo(int i) => i % 2;
    }

    public sealed class SealedClassUnderTest
    {
        public int RestByTwo(int i) => i % 2;
    }

    public abstract class AbstractClass {
        public abstract int RestByTwo(int i);
    }

    public sealed class SealedInheritanceUnderTest : AbstractClass
    {
        public override int RestByTwo(int i) => i % 2;
    }

    public class RegularInheritanceUnderTest : AbstractClass
    {
        public override int RestByTwo(int i) => i % 2;
    }
}

