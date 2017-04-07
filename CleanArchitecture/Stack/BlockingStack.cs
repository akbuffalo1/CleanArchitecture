using System;
namespace CleanArchitecture
{
	public class BlockingStack<T>
	{
		private class Node<T>
		{
			public Node<T> Next;
			public T Item;
		}

		private Node<T> head;

		public BlockingStack()
		{
			head = new Node<T>();
		}

		public void Push(T item)
		{
			Node<T> node = new Node<T>();
			node.Item = item;
			// TODO: Blocking here!
			lock (head)
			{
				node.Next = head.Next;
				head.Next = node;
			}	
		}

		public T Pop()
		{
			Node<T> node = head.Next;
			// TODO: Blocking here!
			lock (head)
			{
				if (node == null)
					return default(T);
				head.Next = node.Next;
			}
			return node.Item;
		}
	}
}
