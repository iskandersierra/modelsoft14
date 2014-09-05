using System;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace ModelSoft.Framework.Reflection
{
    public class ExpressionPrinter : ExpressionVisitor
    {
        int _indentLevel;
        string Indentation
        {
            get
            {
                return Enumerable.Range(1, _indentLevel).Aggregate("", (a, i) => a + "  ");
            }
        }

        readonly StringBuilder _content = new StringBuilder();

        public override string ToString()
        {
            return _content.ToString();
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            _content.Append(Indentation).Append("Binary ").Append(node.NodeType).AppendLine();
            using (Indent())
            {
                _content.Append(Indentation).AppendLine("Left");
                using (Indent())
                {
                    Visit(node.Left);
                }
                _content.Append(Indentation).AppendLine("Right");
                using (Indent())
                    Visit(node.Right);
                _content.Append(Indentation).AppendLine("Conversion");
                using (Indent())
                    Visit(node.Conversion);
            }
            return node;
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            _content.Append(Indentation).AppendLine("Block");
            using (Indent())
            {
                if (node.Variables != null && node.Variables.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Variables");
                    using (Indent())
                        foreach (var item in node.Variables)
                            Visit(item);
                }
                if (node.Expressions != null && node.Expressions.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Expressions");
                    using (Indent())
                        foreach (var item in node.Expressions)
                            Visit(item);
                }
                if (node.Result != null)
                {
                    _content.Append(Indentation).AppendLine("Result");
                    using (Indent())
                        Visit(node.Result);
                }
            }
            return node;
        }

        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            _content.Append(Indentation).AppendLine("Catch");
            using (Indent())
            {
                if (node.Variable != null)
                {
                    _content.Append(Indentation).AppendLine("Variable");
                    using (Indent())
                        Visit(node.Variable);
                }
                if (node.Filter != null)
                {
                    _content.Append(Indentation).AppendLine("Filter");
                    using (Indent())
                        Visit(node.Filter);
                }
                if (node.Body != null)
                {
                    _content.Append(Indentation).AppendLine("Body");
                    using (Indent())
                        Visit(node.Body);
                }
            }
            return node;
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            _content.Append(Indentation).AppendLine("Conditional");
            using (Indent())
            {
                _content.Append(Indentation).AppendLine("Test");
                using (Indent())
                    Visit(node.Test);
                _content.Append(Indentation).AppendLine("IfTrue");
                using (Indent())
                    Visit(node.IfTrue);
                _content.Append(Indentation).AppendLine("IfFalse");
                using (Indent())
                    Visit(node.IfFalse);
            }
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _content.Append(Indentation).Append("Constant ").Append(node.Value == null ? "null" : node.Value.ToString()).AppendFormat(" [{0}]", node.Type.Name).AppendLine();
            return node;
        }

        protected override Expression VisitDynamic(DynamicExpression node)
        {
            _content.Append(Indentation).AppendLine("Dynamic");
            return node;
        }

        protected override Expression VisitDefault(DefaultExpression node)
        {
            _content.Append(Indentation).AppendFormat("Default({0})", node.Type.Name).AppendLine();
            return node;
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            _content.Append(Indentation).AppendLine("Indexer");
            using (Indent())
            {
                if (node.Object != null)
                {
                    _content.Append(Indentation).AppendLine("Object");
                    using (Indent())
                        Visit(node.Object);
                }
                if (node.Arguments != null && node.Arguments.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Arguments");
                    using (Indent())
                        foreach (var item in node.Arguments)
                            Visit(item);
                }
            }
            return node;
        }

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            _content.Append(Indentation).AppendLine("Invocation");
            using (Indent())
            {
                _content.Append(Indentation).AppendLine("Expression");
                using (Indent())
                    Visit(node.Expression);
                if (node.Arguments.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Arguments");
                    using (Indent())
                        foreach (var item in node.Arguments)
                            Visit(item);
                }
            }
            return node;
        }

        protected override Expression VisitLabel(LabelExpression node)
        {
            _content.Append(Indentation).Append("Label ").AppendFormat("{0} : {1}", node.Target.Name, node.Target.Type.Name).AppendLine();
            using (Indent())
            {
                if (node.DefaultValue != null)
                {
                    _content.Append(Indentation).AppendLine("DefaultValue");
                    using (Indent())
                        Visit(node.DefaultValue);
                }
            }
            return node;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            _content.Append(Indentation).AppendFormat("Lambda \"{0}\"", node.Name).AppendLine();
            using (Indent())
            {
                if (node.Parameters.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Parameters");
                    using (Indent())
                        foreach (var item in node.Parameters)
                            Visit(item);
                }
                _content.Append(Indentation).AppendLine("Body");
                using (Indent())
                    Visit(node.Body);
            }
            return node;
        }

        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            _content.Append(Indentation).AppendLine("DebugInfo");
            return node;
        }

        protected override ElementInit VisitElementInit(ElementInit node)
        {
            _content.Append(Indentation).AppendLine("ElementInit");
            _content.Append(Indentation).AppendFormat("ElementInit \"{0}\"", node.AddMethod);
            using (Indent())
            {
                if (node.Arguments.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Arguments");
                    using (Indent())
                        foreach (var item in node.Arguments)
                            Visit(item);
                }
            }
            return node;
        }

        protected override Expression VisitGoto(GotoExpression node)
        {
            _content.Append(Indentation).AppendLine("Goto");
            return node;
        }

        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            _content.Append(Indentation).AppendLine("LabelTarget");
            return node;
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            _content.Append(Indentation).AppendLine("ListInit");
            return node;
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            _content.Append(Indentation).AppendLine("Loop");
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
// ReSharper disable once PossibleNullReferenceException
            _content.Append(Indentation).AppendFormat(@"Member ""{0}.{1}"" {2}", node.Member.DeclaringType.Name, node.Member.Name, node.Member).AppendLine();
            using (Indent())
            {
                if (node.Expression != null)
                {
                    _content.Append(Indentation).AppendLine("Expression");
                    using (Indent())
                        Visit(node.Expression);
                }
            }
            return node;
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
// ReSharper disable once PossibleNullReferenceException
            _content.Append(Indentation).AppendFormat(@"MemberAssignment [{0}] on ""{1}.{2}"" {3}", node.BindingType, node.Member.DeclaringType.Name, node.Member.Name, node.Member).AppendLine();
            using (Indent())
            {
                _content.Append(Indentation).AppendLine("Expression");
                using (Indent())
                    Visit(node.Expression);
            }
            return node;
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            _content.Append(Indentation).AppendLine("MemberInit");
            using (Indent())
            {
                _content.Append(Indentation).AppendLine("NewExpression");
                using (Indent())
                    Visit(node.NewExpression);
                if (node.Bindings.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Bindings");
                    using (Indent())
                        foreach (var item in node.Bindings)
                        {
                        }
                }
            }
            return node;
        }

        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            _content.Append(Indentation).AppendLine("MemberListBinding");
// ReSharper disable once PossibleNullReferenceException
            _content.Append(Indentation).AppendFormat(@"MemberListBinding [{0}] on ""{1}.{2}"" {3}", node.BindingType, node.Member.DeclaringType.Name, node.Member.Name, node.Member).AppendLine();
            using (Indent())
            {
                _content.Append(Indentation).AppendLine("Initializers");
                using (Indent())
                    foreach (var item in node.Initializers)
                        VisitElementInit(item);
            }
            return node;
        }

        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
// ReSharper disable once PossibleNullReferenceException
            _content.Append(Indentation).AppendFormat(@"MemberMemberBinding [{0}] on ""{1}.{2}"" {3}", node.BindingType, node.Member.DeclaringType.Name, node.Member.Name, node.Member).AppendLine();
            using (Indent())
            {
                _content.Append(Indentation).AppendLine("Bindings");
                using (Indent())
                    foreach (var item in node.Bindings)
                    {
                    }
            }
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
// ReSharper disable once PossibleNullReferenceException
            _content.Append(Indentation).AppendFormat(@"MethodCall : {0}""{1}.{2}"" {3}", node.Method.IsStatic ? "static " : "", node.Method.DeclaringType.Name, node.Method.Name, node.Method).AppendLine();
            using (Indent())
            {
                if (node.Object != null)
                {
                    _content.Append(Indentation).AppendLine("Object");
                    using (Indent())
                        Visit(node.Object);
                }
                _content.Append(Indentation).AppendLine("Arguments");
                using (Indent())
                    foreach (var item in node.Arguments)
                        Visit(item);
            }
            return node;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            _content.Append(Indentation).AppendFormat("New \"{0}\" with {1}", node.Type.Name, "constructor " + node.Constructor).AppendLine();
            using (Indent())
            {
                if (node.Arguments.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Arguments");
                    using (Indent())
                        foreach (var item in node.Arguments)
                            Visit(item);
                }
                if (node.Members.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Members");
                    using (Indent())
                        foreach (var item in node.Members)
                            _content.Append(Indentation).Append(item).AppendLine();
                }
            }
            return node;
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            _content.Append(Indentation).AppendFormat("NewArray of {0}", node.Type.FullName).AppendLine();
            using (Indent())
            {
                if (node.Expressions.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Expressions");
                    using (Indent())
                        foreach (var item in node.Expressions)
                            Visit(item);
                }
            }
            return node;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            _content.Append(Indentation).AppendFormat("Parameter \"{0}\" : {1}", node.Name, node.Type.FullName).AppendLine();
            return node;
        }

        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            _content.Append(Indentation).AppendFormat("RuntimeVariables : {0}", node.Type.FullName).AppendLine();
            using (Indent())
            {
                if (node.Variables != null && node.Variables.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Variables");
                    using (Indent())
                        foreach (var item in node.Variables)
                            Visit(item);
                }
            }
            return node;
        }

        protected override Expression VisitSwitch(SwitchExpression node)
        {
            _content.Append(Indentation).AppendFormat("Switch by {0}", node.Comparison);
            using (Indent())
            {
                if (node.SwitchValue != null)
                {
                    _content.Append(Indentation).AppendLine("SwitchValue");
                    using (Indent())
                        Visit(node.SwitchValue);
                }
                if (node.Cases != null && node.Cases.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Cases");
                    using (Indent())
                        foreach (var item in node.Cases)
                            VisitSwitchCase(item);
                }
                if (node.DefaultBody != null)
                {
                    _content.Append(Indentation).AppendLine("DefaultBody");
                    using (Indent())
                        Visit(node.DefaultBody);
                }
            }
            return node;
        }

        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            _content.Append(Indentation).AppendFormat("SwitchCase").AppendLine();
            using (Indent())
            {
                if (node.TestValues != null && node.TestValues.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("TestValues");
                    using (Indent())
                        foreach (var item in node.TestValues)
                            Visit(item);
                }
                if (node.Body != null)
                {
                    _content.Append(Indentation).AppendLine("Body");
                    using (Indent())
                        Visit(node.Body);
                }
            }
            return node;
        }

        protected override Expression VisitTry(TryExpression node)
        {
            _content.Append(Indentation).AppendLine("Try");
            using (Indent())
            {
                if (node.Body != null)
                {
                    _content.Append(Indentation).AppendLine("Body");
                    using (Indent())
                        Visit(node.Body);
                }
                if (node.Fault != null)
                {
                    _content.Append(Indentation).AppendLine("Fault");
                    using (Indent())
                        Visit(node.Fault);
                }
                if (node.Finally != null)
                {
                    _content.Append(Indentation).AppendLine("Finally");
                    using (Indent())
                        Visit(node.Finally);
                }
                if (node.Handlers != null && node.Handlers.Count > 0)
                {
                    _content.Append(Indentation).AppendLine("Handlers");
                    using (Indent())
                        foreach (var item in node.Handlers)
                            VisitCatchBlock(item);
                }
            }
            return node;
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            _content.Append(Indentation).AppendFormat("TypeBinary : {0}", node.TypeOperand.FullName);
            using (Indent())
            {
                _content.Append(Indentation).AppendLine("Expression");
                using (Indent())
                    Visit(node.Expression);
            }
            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            _content.Append(Indentation).AppendFormat("Unary : {0} ({1})", node.NodeType, node.Method).AppendLine();
            using (Indent())
            {
                _content.Append(Indentation).AppendLine("Operand");
                using (Indent())
                    Visit(node.Operand);
            }
            return node;
        }

        protected override Expression VisitExtension(Expression node)
        {
            _content.Append(Indentation).AppendLine("Extension");
            return base.VisitExtension(node);
        }

        private IDisposable Indent()
        {
            return new GenericMonitor(() => _indentLevel++, () => _indentLevel--);
        }
    }

    public static class ExpressionPrinterExtensions
    {
        public static string Print(this Expression expression)
        {
            expression.RequireNotNull("expression");

            var printer = new ExpressionPrinter();
            printer.Visit(expression);
            return printer.ToString();
        }
    }
}
