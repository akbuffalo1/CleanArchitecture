using System;
namespace CleanArchitecture
{
	public class SimpleStack<T>
	{
		private class Node<V>
		{
			public Node<V> Next;
			public V Item;
		}

		private Node<T> head;

		public SimpleStack()
		{
			head = new Node<T>();
		}

		public void Push(T item)
		{
			Node<T> node = new Node<T>();
			node.Item = item;
			node.Next = head.Next;
			head.Next = node;
		}

		public T Pop()
		{
			Node<T> node = head.Next;
			if (node == null)
				return default(T);
			head.Next = node.Next;
			return node.Item;
		}
	}
}
