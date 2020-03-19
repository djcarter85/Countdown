namespace Countdown
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PostfixStack
    {
        private readonly IEnumerable<EquationItem> items;

        private PostfixStack(IEnumerable<EquationItem> items)
        {
            this.items = items;
        }

        public static PostfixStack Empty() => new PostfixStack(Enumerable.Empty<EquationItem>());

        public PostfixStack AddValue(int value) =>
            new PostfixStack(this.items.Append(EquationItem.Value(value)));

        public PostfixStack AddOperation(Operation operation) =>
            new PostfixStack(this.items.Append(EquationItem.Operation(operation)));

        public Expression ToExpression()
        {
            var stack = this.ToExpressionStack();

            if (stack.Count != 1)
            {
                throw new InvalidOperationException();
            }

            return stack.Single();
        }

        public Stack<Expression> ToExpressionStack()
        {
            var stack = new Stack<Expression>();

            foreach (var item in this.items)
            {
                if (!item.CanModifyStack(stack))
                {
                    throw new InvalidOperationException();
                }

                item.ModifyStack(stack);
            }

            return stack;
        }

        public string Representation()
        {
            return string.Join(" ", this.items.Select(i => i.Representation()));
        }
    }
}