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
    public class BinarySearchTree<T> : IEnumerable<T>
    {
        #region fields
        private Node<T> root;
        private readonly Comparison<T> comparison;
        #endregion

        #region properties
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
            this.comparison = (comparison == null) ? DefaultCompare() : comparison;
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
            root = new Node<T>(value);
        }

        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="value">Value to insert to the BST.</param>
        /// <param name="comparison">A way of comparing elements.</param>
        public BinarySearchTree(T value, Comparison<T> comparison)
        {
            if (ReferenceEquals(value, null)) throw new ArgumentNullException($"{nameof(value)} is null.");
            this.comparison = (comparison == null) ? DefaultCompare() : comparison;
            root = new Node<T>(value);
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
            if (values == null) throw new ArgumentNullException($"{nameof(values)} is null.");
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
            if (values == null) throw new ArgumentNullException($"{nameof(values)} is null.");
            if (values.Length == 0) throw new ArgumentException($"{nameof(values)} is empty.");
            this.comparison = (comparison == null) ? DefaultCompare() : comparison;
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

            Node<T> node = root;
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
            if (ReferenceEquals(root, null)) root = new Node<T>(item);
            else
            {
                if (comparison.Invoke(parent.Value, item) > 0)
                    parent.Left = new Node<T>(item);
                else
                    parent.Right = new Node<T>(item);
            }
        }

        /// <summary>
        /// Clears the BST.
        /// </summary>
        public void Clear()
        {
            root = null;
        }

        /// <summary>
        /// Searches an element in the BST.
        /// </summary>
        /// <param name="item">Element to search.</param>
        /// <returns>True, if element presents, and false otherwise.</returns>
        public bool Contains(T item)
        {
            if (ReferenceEquals(item, null)) throw new ArgumentNullException($"{nameof(item)} is null.");

            Node<T> node = root;

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
            if (ReferenceEquals(root, null)) return false;
            if (!Contains(item)) return false;

            Node<T> find = Find(item);
            Node<T> findparent = FindParent(item);

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
        /// Preorder traversal of the BST.
        /// </summary>
        /// <returns>Traversal of the BST.</returns>
        public IEnumerable<T> PreorderTraversal()
        {
            Node<T> current = root;
            Stack<Node<T>> s = new Stack<Node<T>>();

            while (true)
            {
                while (current != null)
                {
                    yield return current.Value;
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
        public IEnumerable<T> InorderTraversal()
        {
            Node<T> current = root;
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
                    yield return current.Value;
                    current = current.Right;
                }
            }
        }

        /// <summary>
        /// Postorder traversal of the BST.
        /// </summary>
        /// <returns>Traversal of the BST.</returns>
        public IEnumerable<T> PostorderTraversal()
        {
            Node<T> lastVisited = root;

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
                    yield return next.Value;
                    lastVisited = next;
                }
                else
                {
                    if (next.Right != null) stack.Push(next.Right);
                    if (next.Left != null) stack.Push(next.Left);
                }
            }
        }

        public IEnumerator<T> GetEnumerator() => PreorderTraversal().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => PreorderTraversal().GetEnumerator();
        #endregion

        #region private methods
        private bool RemoveRightLeftNull(Node<T> find, Node<T> findparent)
        {
            if (findparent == null)
            {
                root = null;
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
                root = find.Left;
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
                root = find.Right;
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
                root = leftmost;
                return true;
            }
            if (comparison.Invoke(find.Value, findparent.Value) < 0)
                findparent.Left = leftmost;
            else
                findparent.Right = leftmost;
            return true;
        }

        private Node<T> Find(T item)
        {
            if (ReferenceEquals(item, null)) throw new ArgumentNullException($"{nameof(item)} is null.");

            Node<T> node = root;

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

        private Node<T> FindParent(T item)
        {
            if (ReferenceEquals(item, null)) throw new ArgumentNullException($"{nameof(item)} is null.");

            if (ReferenceEquals(root, null)) return null;
            if (comparison.Invoke(root.Value, item) == 0) return null;
            if (!Contains(item)) return null;

            Node<T> node = root;

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

        private Comparison<T> DefaultCompare()
        {
            if (typeof(IComparable).IsAssignableFrom(typeof(T)) || typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                return Comparer<T>.Default.Compare;
            else
                throw new ArgumentException($"In type {typeof(T)} there isn't default comparer!");
        }
        #endregion

        #region Node<T>
        /// <summary>
        /// Class subscribes a node for the BST.
        /// </summary>
        /// <typeparam name="T">Type for substitution.</typeparam>
        private class Node<T>
        {
            #region properties
            /// <summary>
            /// Data of the node.
            /// </summary>
            public T Value { get; }

            /// <summary>
            /// Left node.
            /// </summary>
            public Node<T> Left { get; set; }

            /// <summary>
            /// Right node.
            /// </summary>
            public Node<T> Right { get; set; }
            #endregion

            #region ctors
            /// <summary>
            /// Ctor without parameters.
            /// </summary>
            public Node() { }

            /// <summary>
            /// Ctor with parameter.
            /// </summary>
            /// <param name="value">Data to insert into node.</param>
            public Node(T value)
            {
                if (ReferenceEquals(value, null)) throw new ArgumentNullException($"{nameof(value)} is null.");
                Value = value;
            }
            #endregion

            #endregion

        }
    }
}
