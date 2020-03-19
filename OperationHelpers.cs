namespace Countdown
{
    using System;

    public static class OperationHelpers
    {
        public static int Evaluate(Operation operation, int left, int right)
        {
            return operation switch
            {
                Operation.Addition => (left + right),
                Operation.Subtraction => (left - right), // TODO throw if negative
                Operation.Multiplication => (left * right),
                Operation.Division => (left / right), // TODO throw if fractional
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static string GetSymbol(Operation operation)
        {
            return operation switch
            {
                Operation.Addition => "+",
                Operation.Subtraction => "-",
                Operation.Multiplication => "*",
                Operation.Division => "/",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}