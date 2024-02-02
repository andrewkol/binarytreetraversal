using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp26
{
    class TreeNode<T> where T : IComparable
    {
        private T value;
        private TreeNode<T> left;
        private TreeNode<T> right;
        public TreeNode(T Value)
        {
            value = Value;
        }
        public T Value { get { return this.value; } set { this.value = value; } }
        public TreeNode<T> Left { get { return left; } set { left = value; } }
        public TreeNode<T> Right { get { return right; } set { right = value; } }
    }
}
