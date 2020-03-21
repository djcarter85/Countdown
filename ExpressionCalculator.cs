namespace Countdown
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ExpressionCalculator
    {
        private readonly Dictionary<IReadOnlyList<int>, IReadOnlyList<Expression>> cache =
            new Dictionary<IReadOnlyList<int>, IReadOnlyList<Expression>>(new CollectionComparer());

        public IReadOnlyList<Expression> GetAllPossibleExpressions(IReadOnlyList<int> numbers)
        {
            if (!numbers.Any())
            {
                throw new InvalidOperationException();
            }

            if (this.cache.TryGetValue(numbers, out var cachedExpressions))
            {
                return cachedExpressions;
            }

            var expressions = this.CalculateAllPossibleExpressions(numbers).ToArray();

            this.cache.Add(numbers, expressions);

            return expressions;
        }

        private IEnumerable<Expression> CalculateAllPossibleExpressions(IReadOnlyList<int> numbers)
        {
            if (numbers.Count == 1)
            {
                yield return Expression.Number(numbers.Single());
                yield break;
            }

            foreach (var operation in Operation.All)
            {
                foreach (var numberSplit in GetSplits(numbers))
                {
                    var leftExpressions = this.GetAllPossibleExpressions(numberSplit.Left).ToArray();
                    var rightExpressions = this.GetAllPossibleExpressions(numberSplit.Right).ToArray();

                    foreach (var leftExpression in leftExpressions)
                    {
                        foreach (var rightExpression in rightExpressions)
                        {
                            if (ExpressionShouldBeIncluded(operation, leftExpression, rightExpression))
                            {
                                yield return Expression.Operation(operation, leftExpression, rightExpression);
                            }
                        }
                    }
                }
            }
        }

        private static bool ExpressionShouldBeIncluded(Operation operation, Expression leftExpression, Expression rightExpression)
        {
            if (!operation.IsCommutative)
            {
                return true;
            }

            // Only include one variant of a commutative operation
            return Expression.Comparer.Compare(leftExpression, rightExpression) <= 0;
        }

        private static IEnumerable<NumberSplit> GetSplits(IReadOnlyList<int> numbers)
        {
            var count = numbers.Count;

            for (var leftCount = 1; leftCount <= count - 1; leftCount++)
            {
                yield return new NumberSplit(
                    left: numbers.Take(leftCount).ToArray(),
                    right: numbers.Skip(leftCount).ToArray());
            }
        }

        private class NumberSplit
        {
            public NumberSplit(IReadOnlyList<int> left, IReadOnlyList<int> right)
            {
                this.Left = left;
                this.Right = right;
            }

            public IReadOnlyList<int> Left { get; }

            public IReadOnlyList<int> Right { get; }
        }

        private class CollectionComparer : IEqualityComparer<IReadOnlyList<int>>
        {
            public bool Equals(IReadOnlyList<int> x, IReadOnlyList<int> y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(IReadOnlyList<int> obj)
            {
                return obj.Sum(); // needed?
            }
        }
    }
}