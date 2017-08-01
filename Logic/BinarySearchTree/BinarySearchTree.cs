using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    /// <summary>
    /// Class for the representation of the BST.
    /// </summary>
    /// <typeparam name="T">Type for substitution.</typeparam>
    public class BinarySearchTree<T> : IEnumerable<Node<T>> 
    {
        #region fields
        private Node<T> root;
        private int count;
        private readonly Comparison<T> comparison;
        #endregion

        #region properties
        /// <summary>
        /// Root of the BST.
        /// </summary>
        public Node<T> Root
        {
            get => root;
            private set => root = value;
        }

        /// <summary>
        /// Quantity of elements in BST.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Returns true, if BST is immutable, and false otherwise.
        /// </summary>
        public bool IsReadOnly => false;
        #endregion

        #region ctors
        /// <summary>
        /// Ctor.
        /// </summary>
        public BinarySearchTree()
        {
            this.comparison = DefaultCompare();
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="comparison">A way of comparing elements.</param>
        public BinarySearchTree(Comparison<T> comparison)
        {
            this.comparison = ReferenceEquals(comparison, null) ? DefaultCompare() : comparison;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="comparer">A way of comparing elements.</param>
        public BinarySearchTree(IComparer<T> comparer) : this(comparer.Compare) { }


        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="value">Value to insert to the BST.</param>
        public BinarySearchTree(T value)
        {
            if (ReferenceEquals(value, null)) throw new ArgumentNullException($"{nameof(value)} is null.");
            this.comparison = DefaultCompare();
            Root = new Node<T>(value);
            count++;
        }

        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="value">Value to insert to the BST.</param>
        /// <param name="comparison">A way of comparing elements.</param>
        public BinarySearchTree(T value, Comparison<T> comparison) 
        {
            if (ReferenceEquals(value, null)) throw new ArgumentNullException($"{nameof(value)} is null.");
            this.comparison = ReferenceEquals(comparison, null) ? DefaultCompare() : comparison;
            Root = new Node<T>(value);
            count++;
        }

        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="value">Value to insert to the BST.</param>
        /// <param name="comparer">A way of comparing elements.</param>
        public BinarySearchTree(T value, IComparer<T> comparer) : this(value, comparer.Compare) { }


        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="values">Array of elements to insert to the BST.</param>
        public BinarySearchTree(T[] values)
        {
            if (ReferenceEquals(values, null)) throw new ArgumentNullException($"{nameof(values)} is null.");
            if (values.Length == 0) throw new ArgumentException($"{nameof(values)} is empty.");
            this.comparison = DefaultCompare();
            foreach (var v in values)
            {
                Add(v);
            }
        }

        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="values">Array of elements to insert to the BST.</param>
        /// <param name="comparison">A way of comparing elements.</param>
        public BinarySearchTree(T[] values, Comparison<T> comparison)
        {
            if (ReferenceEquals(values, null)) throw new ArgumentNullException($"{nameof(values)} is null.");
            if (values.Length == 0) throw new ArgumentException($"{nameof(values)} is empty.");
            this.comparison = ReferenceEquals(comparison, null) ? DefaultCompare() : comparison;
            foreach (var v in values)
            {
                Add(v);
            }
        }

        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="values">Array of elements to insert to the BST.</param>
        /// <param name="comparer">A way of comparing elements.</param>
        public BinarySearchTree(T[] values, IComparer<T> comparer) : this(values, comparer.Compare) { }
        #endregion

        #region public methods
        /// <summary>
        /// Inserts an element in the BST.
        /// </summary>
        /// <param name="item">Element to insert.</param>
        public void Add(T item)
        {
            if (ReferenceEquals(item, null)) throw new ArgumentNullException($"{nameof(item)} is null.");
            if (Contains(item)) return;   

            Node<T> node = Root;
            Node<T> parent = null;

            while (node != null)
            {
                if (comparison.Invoke(item, node.Value) < 0)
                {
                    parent = node;
                    node = node.Left;
                }
                else if (comparison.Invoke(item, node.Value) > 0)
                {
                    parent = node;
                    node = node.Right;
                }
            }
            if (ReferenceEquals(Root, null)) Root = new Node<T>(item);
            else
            {
                if (comparison.Invoke(parent.Value, item) > 0)
                    parent.Left = new Node<T>(item);
                else
                    parent.Right = new Node<T>(item);
            }
            count++;
        }

        /// <summary>
        /// Clears the BST.
        /// </summary>
        public void Clear()
        {
            Root = null;
            count = 0;
        }

        /// <summary>
        /// Searches an element in the BST.
        /// </summary>
        /// <param name="item">Element to search.</param>
        /// <returns>True, if element presents, and false otherwise.</returns>
        public bool Contains(T item)
        {
            if (ReferenceEquals(item, null)) throw new ArgumentNullException($"{nameof(item)} is null.");

            Node<T> node = Root;

            while (node != null)
            {
                if (comparison.Invoke(node.Value, item) == 0)
                    return true;
                else if (comparison.Invoke(node.Value, item) > 0)
                    node = node.Left;
                else 
                    node = node.Right;
            }

            return false;
        }

        /// <summary>
        /// Removes an element from the BST.
        /// </summary>
        /// <param name="item">Element to remove.</param>
        /// <returns>True, if element removed, and false otherwise.</returns>
        public bool Remove(T item)
        {
            if (ReferenceEquals(item, null)) throw new ArgumentNullException($"{nameof(item)} is null.");
            if (ReferenceEquals(Root, null)) return false;
            if (!Contains(item)) return false;

            Node<T> find = Find(item);
            Node<T> findparent = FindParent(item);

            count--;

            if (find.Left == null && find.Right == null)
            {
                return RemoveRightLeftNull(find, findparent);
            }
            else if (find.Right == null) //1
            {
                return RemoveRightNull(find, findparent);
            }
            else if (find.Right.Left == null) //2
            {
                return RemoveLeftNull(find, findparent);
            }
            else //3
            {
                return RemoveRightLeftNotNull(find, findparent);
            }
        }

        /// <summary>
        /// Finds node with given value.
        /// </summary>
        /// <param name="item">Value to search item with it.</param>
        /// <returns>Node with given value.</returns>
        public Node<T> Find(T item)
        {
            if (ReferenceEquals(item, null)) throw new ArgumentNullException($"{nameof(item)} is null.");

            Node<T> node = Root;

            while (node != null)
            {
                if (comparison.Invoke(node.Value, item) == 0)
                    return node;
                else if (comparison.Invoke(node.Value, item) > 0)
                    node = node.Left;
                else
                    node = node.Right;
            }

            return null;
        }

        /// <summary>
        /// Finds parent of the element with given value.
        /// </summary>
        /// <param name="item">Value to search parent of item with it.</param>
        /// <returns>Parent of the element with given value.</returns>
        public Node<T> FindParent(T item)
        {
            if (ReferenceEquals(item, null)) throw new ArgumentNullException($"{nameof(item)} is null.");

            if (ReferenceEquals(Root, null)) return null;
            if (comparison.Invoke(Root.Value, item) == 0) return null;
            if (!Contains(item)) return null;

            Node<T> node = Root;

            while (node != null)
            {
                if (node.Left != null)
                {
                    if (comparison.Invoke(node.Left.Value, item) == 0) return node;
                }
                if (node.Right != null)
                {
                    if (comparison.Invoke(node.Right.Value, item) == 0) return node;
                }
                if (comparison.Invoke(node.Value, item) > 0)
                    node = node.Left;
                else if (comparison.Invoke(node.Value, item) < 0)
                    node = node.Right;
            }

            return null;
        }

        /// <summary>
        /// Preorder traversal of the BST.
        /// </summary>
        /// <returns>Traversal of the BST.</returns>
        public IEnumerable<Node<T>> PreorderTraversal()
        {
            Node<T> current = Root;
            Stack<Node<T>> s = new Stack<Node<T>>();

            while (true)
            {
                while (current != null)
                {
                    yield return current;
                    s.Push(current);
                    current = current.Left;
                }
                if (s.Count == 0) break;
                current = s.Pop();
                current = current.Right;
            }
        }

        /// <summary>
        /// Inorder traversal of the BST.
        /// </summary>
        /// <returns>Traversal of the BST.</returns>
        public IEnumerable<Node<T>> InorderTraversal() 
        {
            Node<T> current = Root;
            Stack<Node<T>> s = new Stack<Node<T>>();

            while (true)
            {
                if (current != null)
                {
                    s.Push(current);
                    current = current.Left;
                }
                else
                {
                    if (s.Count == 0) break;
                    current = s.Pop();
                    yield return current;
                    current = current.Right;
                }
            }
        }

        /// <summary>
        /// Postorder traversal of the BST.
        /// </summary>
        /// <returns>Traversal of the BST.</returns>
        public IEnumerable<Node<T>> PostorderTraversal()
        {
            Node<T> lastVisited = Root;
            
            Stack<Node<T>> stack = new Stack<Node<T>>();
            stack.Push(lastVisited);

            while (stack.Count != 0)
            {
                Node<T> next = stack.Peek();

                bool finishedSubtreesR = next.Right != null ? (comparison.Invoke(next.Right.Value, lastVisited.Value) == 0) : false;
                bool finishedSubtreesL = next.Left != null ? (comparison.Invoke(next.Left.Value, lastVisited.Value) == 0) : false;
                bool isLeaf = (next.Left == null && next.Right == null);

                if (finishedSubtreesR || finishedSubtreesL || isLeaf)
                {
                    stack.Pop();
                    yield return next;
                    lastVisited = next;
                }
                else
                {
                    if (next.Right != null) stack.Push(next.Right);
                    if (next.Left != null) stack.Push(next.Left);
                }
            }
        }

        public IEnumerator<Node<T>> GetEnumerator() => PreorderTraversal().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => PreorderTraversal().GetEnumerator();

        /// <summary>
        /// Returns string representation of the BST.
        /// </summary>
        /// <returns>String representation of the BST.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var i in PreorderTraversal())
            {
                sb.Append(i.ToString() + ' ');
            }
            return sb.ToString();
        }
        #endregion

        #region private methods
        private bool RemoveRightLeftNull(Node<T> find, Node<T> findparent)
        {
            if (findparent == null)
            {
                Root = null;
                return true;
            }
            if (findparent.Left != null)
            {
                if (comparison.Invoke(find.Value, findparent.Left.Value) == 0)
                    findparent.Left = null;
            }
            if (findparent.Right != null)
            {
                if (comparison.Invoke(find.Value, findparent.Right.Value) == 0)
                    findparent.Right = null;
            }
            return true;
        }

        private bool RemoveRightNull(Node<T> find, Node<T> findparent)
        {
            if (findparent == null)
            {
                Root = find.Left;
                return true;
            }
            if (comparison.Invoke(find.Value, findparent.Value) < 0)
                findparent.Left = find.Left;
            else
                findparent.Right = find.Left;
            return true;
        }

        private bool RemoveLeftNull(Node<T> find, Node<T> findparent)
        {
            find.Right.Left = find.Left;
            if (findparent == null)
            {
                Root = find.Right;
                return true;
            }
            find.Right.Left = find.Left;
            if (comparison.Invoke(find.Value, findparent.Value) < 0)
                findparent.Left = find.Right;
            else
                findparent.Right = find.Right;
            return true;
        }

        private bool RemoveRightLeftNotNull(Node<T> find, Node<T> findparent)
        {
            Node<T> leftmost = find.Right.Left;
            Node<T> leftmostparent = find.Right;
            while (leftmost.Left != null)
            {
                leftmostparent = leftmost;
                leftmost = leftmost.Left;
            }
            leftmostparent.Left = leftmost.Right;
            leftmost.Left = find.Left;
            leftmost.Right = find.Right;
            if (findparent == null)
            {
                Root = leftmost;
                return true;
            }
            if (comparison.Invoke(find.Value, findparent.Value) < 0)
                findparent.Left = leftmost;
            else
                findparent.Right = leftmost;
            return true;
        }

        private Comparison<T> DefaultCompare() 
        {
            IComparer<T> myComparer = Comparer<T>.Default;
            try
            {
                myComparer.Compare(default(T), default(T));
            }
            catch (Exception)
            {
                throw new ArgumentException($"In type {typeof(T)} there isn't default comparer!");
            }
            return Comparer<T>.Default.Compare;
        }
        #endregion

    }
}
