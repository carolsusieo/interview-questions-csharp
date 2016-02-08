using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Globalization;
namespace interviewCSharp
{
	public class Node<T>
	{
		// Private member-variables
		private T data;
		private NodeList<T> neighbors = null;

		public Node() {}
		public Node(T data) : this(data, null) {}
		public Node(T data, NodeList<T> neighbors)
		{
			this.data = data;
			this.neighbors = neighbors;
		}

		public T Value
		{
			get
			{
				return data;
			}
			set
			{
				data = value;
			}
		}

		protected NodeList<T> Neighbors
		{
			get
			{
				return neighbors;
			}
			set
			{
				neighbors = value;
			}
		}
	}
	public class NodeList<T> : Collection<Node<T>>
	{
		public NodeList() : base() { }

		public NodeList(int initialSize)
		{
			// Add the specified number of items
			for (int i = 0; i < initialSize; i++)
				base.Items.Add(default(Node<T>));
		}

		public Node<T> FindByValue(T value)
		{
			// search the list for the value
			foreach (Node<T> node in Items)
				if (node.Value.Equals(value))
					return node;

			// if we reached here, we didn't find a matching node
			return null;
		}
	}
	public class BinaryTreeNode<T> : Node<T>
	{
		public BinaryTreeNode() : base() {}
		public BinaryTreeNode(T data) : base(data, null) {}
		public BinaryTreeNode(T data, BinaryTreeNode<T> left, BinaryTreeNode<T> right)
		{
			base.Value = data;
			NodeList<T> children = new NodeList<T>(2);
			children[0] = left;
			children[1] = right;

			base.Neighbors = children;
		}

		public BinaryTreeNode<T> Left
		{
			get
			{
				if (base.Neighbors == null)
					return null;
				else
					return (BinaryTreeNode<T>) base.Neighbors[0];
			}
			set
			{
				if (base.Neighbors == null)
					base.Neighbors = new NodeList<T>(2);

				base.Neighbors[0] = value;
			}
		}
		public BinaryTreeNode<T> Right
		{
			get
			{
				if (base.Neighbors == null)
					return null;
				else
					return (BinaryTreeNode<T>) base.Neighbors[1];
			}
			set
			{
				if (base.Neighbors == null)
					base.Neighbors = new NodeList<T>(2);

				base.Neighbors[1] = value;
			}
		}
	}

		public class BinaryTree<TT>
		{
			private BinaryTreeNode<TT> root;
			private int count;
			private Comparer comparer;
			public BinaryTree()
			{
				root = null;
				count = 0;
				comparer = new Comparer(new CultureInfo( "en-US", false ) );
			}

			public virtual void Clear()
			{
				root = null;
				count = 0;
			}

			public BinaryTreeNode<TT> Root
			{
				get
				{
					return root;
				}
				set
				{
					root = value;
					count++;
				}
			}
		// duplicates allowed
		public virtual void Add(TT data)
		{
			// create a new Node instance
			BinaryTreeNode<TT> n = new BinaryTreeNode<TT>(data);
			int result;

			// now, insert n into the tree
			// trace down the tree until we hit a NULL
			BinaryTreeNode<TT> current = root, parent = null;
			while (current != null)
			{
				result = comparer.Compare(current.Value, data);
				//if (result == 0) {
				//	// they are equal - attempting to enter a duplicate - do nothing
				//	return;
				//}
				//else 
				if (result >= 0)
				{
					// current.Value > data, must add n to current's left subtree
					parent = current;
					current = current.Left;
				}
				else if (result < 0)
				{
					// current.Value < data, must add n to current's right subtree
					parent = current;
					current = current.Right;
				}
			}

			// We're ready to add the node!
			count++;
			if (parent == null)
				// the tree was empty, make n the root
				root = n;
			else
			{
				result = comparer.Compare(parent.Value, data);
				if (result >= 0)
					// parent.Value > data, therefore n must be added to the left subtree
					parent.Left = n;
				else
					// parent.Value < data, therefore n must be added to the right subtree
					parent.Right = n;
			}
		}
		public bool Remove(TT data)
		{
			// first make sure there exist some items in this tree
			if (root == null)
				return false;       // no items to remove

			// Now, try to find data in the tree
			BinaryTreeNode<TT> current = root, parent = null;
			int result = comparer.Compare(current.Value, data);
			while (result != 0)
			{
				if (result >= 0)
				{
					// current.Value > data, if data exists it's in the left subtree
					parent = current;
					current = current.Left;
				}
				else if (result < 0)
				{
					// current.Value < data, if data exists it's in the right subtree
					parent = current;
					current = current.Right;
				}

				// If current == null, then we didn't find the item to remove
				if (current == null)
					return false;
				else
					result = comparer.Compare(current.Value, data);
			}

			// At this point, we've found the node to remove
			count--;

			// We now need to "rethread" the tree
			// CASE 1: If current has no right child, then current's left child becomes
			//         the node pointed to by the parent
			if (current.Right == null)
			{
				if (parent == null)
					root = current.Left;
				else
				{
					result = comparer.Compare(parent.Value, current.Value);
					if (result >= 0)
						// parent.Value > current.Value, so make current's left child a left child of parent
						parent.Left = current.Left;
					else if (result < 0)
						// parent.Value < current.Value, so make current's left child a right child of parent
						parent.Right = current.Left;
				}                
			}
			// CASE 2: If current's right child has no left child, then current's right child
			//         replaces current in the tree
			else if (current.Right.Left == null)
			{
				current.Right.Left = current.Left;

				if (parent == null)
					root = current.Right;
				else
				{
					result = comparer.Compare(parent.Value, current.Value);
					if (result >= 0)
						// parent.Value > current.Value, so make current's right child a left child of parent
						parent.Left = current.Right;
					else if (result < 0)
						// parent.Value < current.Value, so make current's right child a right child of parent
						parent.Right = current.Right;
				}
			}
			// CASE 3: If current's right child has a left child, replace current with current's
			//          right child's left-most descendent
			else
			{
				// We first need to find the right node's left-most child
				BinaryTreeNode<TT> leftmost = current.Right.Left, lmParent = current.Right;
				while (leftmost.Left != null)
				{
					lmParent = leftmost;
					leftmost = leftmost.Left;
				}

				// the parent's left subtree becomes the leftmost's right subtree
				lmParent.Left = leftmost.Right;

				// assign leftmost's left and right to current's left and right children
				leftmost.Left = current.Left;
				leftmost.Right = current.Right;

				if (parent == null)
					root = leftmost;
				else
				{
					result = comparer.Compare(parent.Value, current.Value);
					if (result >= 0)
						// parent.Value > current.Value, so make leftmost a left child of parent
						parent.Left = leftmost;
					else if (result < 0)
						// parent.Value < current.Value, so make leftmost a right child of parent
						parent.Right = leftmost;
				}
			}

			return true;
		}



				public void PreorderTraversal(BinaryTreeNode<TT> current)
			{
				if (current != null)
				{
					// Output the value of the current node
					Console.Write(current.Value);
					Console.Write (" ");

					// Recursively print the left and right children
					PreorderTraversal(current.Left);
					PreorderTraversal(current.Right);
				}
			}		
			public void InorderTraversal(BinaryTreeNode<TT> current)
			{
				if (current != null)
				{
					// Visit the left child...
					InorderTraversal(current.Left);

					// Output the value of the current node
					Console.Write(current.Value);
					Console.Write (" ");

					// Visit the right child...
					InorderTraversal(current.Right);
				}
			}	 
	}

}

