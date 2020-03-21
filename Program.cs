namespace Countdown
{
    using System;
    using System.Diagnostics;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var numbers = new[] { 75, 6, 1, 8, 5, 4 };
            var target = 471;

            var solver = new Solver();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var solutions = solver.Solve(numbers, target);

            var solutionCount = 0;
            foreach (var solution in solutions)
            {
                Console.WriteLine($"{solution.Expression.ToInfixNotation()}");
                solutionCount++;
            }

            Console.WriteLine();
            Console.WriteLine($"{solutionCount} solutions found");
            Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
