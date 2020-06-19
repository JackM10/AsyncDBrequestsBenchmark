Benchmark dot net results on my machine for 100k rows in each table:

// * Summary *

BenchmarkDotNet=v0.12.1, OS=Windows 8.1 (6.3.9600.0)
Intel Core i7-4771 CPU 3.50GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3410079 Hz, Resolution=293.2483 ns, Timer=TSC
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


|                            Method |    Mean |    Error |   StdDev |
|---------------------------------- |--------:|---------:|---------:|
|             SynchronioslyEachCall | 1.917 s | 0.0209 s | 0.0196 s |
|                     AsyncEachCall | 2.626 s | 0.0332 s | 0.0311 s |
|      AsyncIndependantCallsWaitAll | 1.820 s | 0.0332 s | 0.0310 s |
| AsyncIndependantCallsWithAwaiters | 1.811 s | 0.0287 s | 0.0268 s |

// * Hints *
Outliers
  MyBench.AsyncEachCall: Default                -> 1 outlier  was  detected (2.52 s)
  MyBench.AsyncIndependantCallsWaitAll: Default -> 1 outlier  was  detected (1.74 s)


For 1m of entities in each table numbers are:

|                            Method |    Mean |   Error |  StdDev |
|---------------------------------- |--------:|--------:|--------:|
|             SynchronioslyEachCall | 19.40 s | 0.275 s | 0.230 s |
|                     AsyncEachCall | 26.88 s | 0.205 s | 0.192 s |
|      AsyncIndependantCallsWaitAll | 18.18 s | 0.235 s | 0.219 s |
| AsyncIndependantCallsWithAwaiters | 18.35 s | 0.217 s | 0.203 s |

