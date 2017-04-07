using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CleanArchitecture
{
	public class ExpressionsTree
	{
		void Main()
		{ 
			var instance = new SomeClass() { Name = "SomeClass" };
			Expression<Func<SomeClass, bool>> resultBool = ins => (ins.Name.Length > 2 || ins.Name.Length < 10) && ins.Name.Length != 0 && !ins.Name.Equals("");
			var binEx = resultBool.Body as BinaryExpression;

			Console.WriteLine(binEx.Left);
			Console.WriteLine(binEx.NodeType == ExpressionType.AndAlso);
			Console.WriteLine(binEx.Right);


			Expression<Func<SomeClass, string>> resultString = ins => ins.Name;
			var memEx = resultString.Body as MemberExpression;
			Console.WriteLine((memEx.Member as PropertyInfo).GetValue(instance));
			Console.WriteLine(memEx.NodeType == ExpressionType.MemberAccess);


			Expression<Func<int, int, int>> expression = (a, b) => a + a * b + 3;

			var visitor = new ConsoleVisitor();
			visitor.Visit(expression.Body);
		}
	}

	class SomeClass
	{
		public string Name { get; set; }
	}


	public class ConsoleVisitor : ExpressionVisitor
	{
		protected override Expression VisitBinary(BinaryExpression node)
		{
			Console.Write("(");

			this.Visit(node.Left);

			Console.Write(" {0} ", node.NodeType);

			this.Visit(node.Right);

			Console.Write(")");

			return node;
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			Console.Write("parameter({0})", node.Name);
			return base.VisitParameter(node);
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			Console.Write("constant({0})", node.Value);
			return base.VisitConstant(node);
		}
	}

}
