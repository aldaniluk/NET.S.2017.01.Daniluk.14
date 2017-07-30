using System;
using System.Collections;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Class subscribes a node for the BST.
    /// </summary>
    /// <typeparam name="T">Type for substitution.</typeparam>
    public class Node<T>
    {
        #region fields
        private Node<T> left;
        private Node<T> right;
        #endregion

        #region properties
        /// <summary>
        /// Data of the node.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Left node.
        /// </summary>
        public Node<T> Left
        {
            get => left;
            internal set => left = value;
        }

        /// <summary>
        /// Right node.
        /// </summary>
        public Node<T> Right
        {
            get => right;
            internal set => right = value;
        }
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

        #region public methods
        /// <summary>
        /// Returns string representation of the node.
        /// </summary>
        /// <returns>String representation of the node.</returns>
        public override string ToString() => Value.ToString();
        #endregion
    }
}
