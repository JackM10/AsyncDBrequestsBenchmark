Benchmark dot net results on my machine:

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
