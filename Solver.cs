namespace Countdown
{
    using System.Collections.Generic;
    using System.Linq;

    public class Solver
    {
        private readonly ExpressionCalculator expressionCalculator = new ExpressionCalculator();

        public IEnumerable<ValidExpression> Solve(IReadOnlyList<int> numbers, int target)
        {
            var allValidExpressions = this.GetAllValidExpressions(numbers);

            return allValidExpressions.Where(ve => ve.Result == target);
        }

        public IEnumerable<ValidExpression> GetAllValidExpressions(IReadOnlyList<int> numbers)
        {
            foreach (var orderedSubset in GetAllOrderedSubsets(numbers))
            {
                var expressions = this.expressionCalculator.GetAllPossibleExpressions(orderedSubset);

                foreach (var expression in expressions)
                {
                    var result = expression.Evaluate();

                    if (result.IsSuccess)
                    {
                        yield return new ValidExpression(expression, result.Value);
                    }
                }
            }
        }

        public static IEnumerable<IReadOnlyList<int>> GetAllOrderedSubsets(IReadOnlyList<int> numbers)
        {
            for (var orderedSubsetLength = 1; orderedSubsetLength <= numbers.Count; orderedSubsetLength++)
            {
                var orderedSubsets = GetAllOrderedSubsetsOfLength(numbers, orderedSubsetLength);

                foreach (var orderedSubset in orderedSubsets)
                {
                    yield return orderedSubset;
                }
            }
        }

        private static IEnumerable<IReadOnlyList<int>> GetAllOrderedSubsetsOfLength(IReadOnlyList<int> numbers, int length)
        {
            if (length == 0)
            {
                yield return new List<int>();
            }
            else
            {
                foreach (var head in numbers)
                {
                    var tail = numbers.Without(head).ToArray();

                    foreach (var orderedSubsetOfTail in GetAllOrderedSubsetsOfLength(tail, length - 1))
                    {
                        yield return orderedSubsetOfTail.Prepend(head).ToArray();
                    }
                }
            }
        }
    }
}