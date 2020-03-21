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

            if (solutionOrNull == null)
            {
                Console.WriteLine("No solution found");
            }
            else
            {
                var solution = solutionOrNull;

                Console.WriteLine($"{solution.Result} = {solution.Expression.ToInfixNotation()}");
            }

            Console.WriteLine();
            Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
