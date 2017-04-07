using System;
using System.Threading;

namespace CleanArchitecture
{
	public class NonBlockingStack<T>
	{
		private class Node<T>
		{
			public Node<T> Next;
			public T Item;
		}

		private Node<T> head;

		public NonBlockingStack()
		{
			head = new Node<T>();
		}

		public void Push(T item)
		{
			Node<T> node = new Node<T>();
			node.Item = item;
			do 
			{
				node.Next = head.Next;
			} while (!CAS(ref head.Next, node.Next, node));
		}

		public T Pop()
		{
			Node<T> node;
			do
			{
				node = head.Next;
				if (node == null)
					return default(T);
			} while (!CAS(ref head.Next, node, node.Next));
			return node.Item;
		}

		private static bool CAS<TYPE>(ref TYPE location, TYPE comparand, TYPE newValue) where TYPE : class
		{
			return comparand == Interlocked.CompareExchange(ref location, newValue, comparand);
		}
	}
}
