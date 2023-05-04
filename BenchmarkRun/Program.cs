// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using TestDataFormatter;

Console.WriteLine("Hello, World!");

BenchmarkRunner.Run<Benchmark>();