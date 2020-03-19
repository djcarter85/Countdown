namespace Countdown
{
    using System;
    using System.Collections.Generic;

    public abstract class EquationItem
    {
        private EquationItem()
        {
        }

        public static EquationItem Value(int value) => new ValueEquationItem(value);

        public static EquationItem Operation(Operation operation) => new OperationEquationItem(operation);

        public abstract bool CanModifyStack(Stack<Expression> stack);

        public abstract void ModifyStack(Stack<Expression> stack);

        public abstract string Representation();

        private class ValueEquationItem : EquationItem
        {
            private readonly int value;

            public ValueEquationItem(int value)
            {
                this.value = value;
            }

            public override bool CanModifyStack(Stack<Expression> stack) => true;

            public override void ModifyStack(Stack<Expression> stack)
            {
                stack.Push(Expression.Number(this.value));
            }

            public override string Representation()
            {
                return this.value.ToString();
            }
        }

        private class OperationEquationItem : EquationItem
        {
            private readonly Operation operation;

            public OperationEquationItem(Operation operation)
            {
                this.operation = operation;
            }

            public override bool CanModifyStack(Stack<Expression> stack) => stack.Count >= 2;

            public override void ModifyStack(Stack<Expression> stack)
            {
                if (!this.CanModifyStack(stack))
                {
                    throw new InvalidOperationException();
                }

                var right = stack.Pop();
                var left = stack.Pop();

                stack.Push(Expression.Operation(this.operation, left, right));
            }

            public override string Representation()
            {
                return OperationHelpers.GetSymbol(this.operation);
            }
        }
    }
}