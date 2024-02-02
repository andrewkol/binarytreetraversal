using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp26
{
    class MyTree<T> where T : IComparable
    {
        TreeNode<T> root;
        TreeNode<T> parent;
        public T Root
        {
            get { return root.Value; }
        }
        public TreeNode<T> RootNode
        {
            get { return root; }
        }
        public void Add(T value)
        {
            TreeNode<T> node = new TreeNode<T>(value);
            if (root == null) 
                root = node;
            else 
                Add(root, node);
        }
        private void Add(TreeNode<T> subroot, TreeNode<T> node)
        {
            if (subroot.Value.CompareTo(node.Value) <= 0)
            {
                if (subroot.Right == null)
                    subroot.Right = node;
                else
                    Add(subroot.Right, node);
            }
            if (subroot.Value.CompareTo(node.Value) > 0)
            {
                if (subroot.Left == null)
                    subroot.Left = node;
                else
                    Add(subroot.Left, node);
            }
        }

        public TreeNode<T> Find(T value)
        {
            return Find(value, root);
        }
        private TreeNode<T> Find(T value, TreeNode<T> subroot)
        {
            if (subroot == null)
                return null;
            if (value.CompareTo(subroot.Value) == 0)
                return subroot;
            if (value.CompareTo(subroot.Value) < 0)
                return Find(value, subroot.Left);
            if (value.CompareTo(subroot.Value) > 0)
                return Find(value, subroot.Right);
            return null;
        }
        public TreeNode<T> FindRoot(T value)
        {
            return FindRoot(value, root);
        }
        private TreeNode<T> FindRoot(T value, TreeNode<T> subroot)
        {
            if (subroot == null)
                return null;
            if (value.CompareTo(subroot.Value) == 0)
                return subroot;
            if (value.CompareTo(subroot.Value) < 0)
            {
                if (value.CompareTo(subroot.Left.Value) == 0)
                    return subroot;
                return FindRoot(value, subroot.Left);
            }
            if (value.CompareTo(subroot.Value) > 0)
            {
                if (value.CompareTo(subroot.Right.Value) == 0)
                    return subroot;
                return FindRoot(value, subroot.Right);
            }
            return null;
        }
        public void Remove(T value)
        {
            Remove(value, root);
        }
        private bool Remove(T value, TreeNode<T> currentNode)
        {
            if (currentNode == null)
                return false;
            if (value.CompareTo(currentNode.Value) < 0)
            {
                parent = currentNode;
                Remove(value, currentNode.Left);
            }
            if (value.CompareTo(currentNode.Value) > 0)
            {
                parent = currentNode;
                Remove(value, currentNode.Right);
            }
            if (value.CompareTo(currentNode.Value) == 0)
            {
                if (currentNode.Right == null)
                {
                    if (currentNode == root)
                        root = currentNode.Left;
                    else
                    {
                        if (parent.Value.CompareTo(currentNode.Value) > 0)
                            parent.Left = currentNode.Left;
                        else
                            parent.Right = currentNode.Left;
                    }
                }
                else if (currentNode.Right.Left == null)
                {
                    currentNode.Right.Left = currentNode.Left;
                    if (currentNode == root)
                        root = currentNode.Right;
                    else
                    {
                        if (parent.Value.CompareTo(currentNode.Value) > 0)
                            parent.Left = currentNode.Right;
                        else
                            parent.Right = currentNode.Right;
                    }
                }
                else
                {
                    TreeNode<T> min = currentNode.Right.Left, prev = currentNode.Right;
                    min = FindMin(min.Left);
                    prev.Left = min.Right;
                    min.Left = currentNode.Left;
                    min.Right = currentNode.Right;
                    if (currentNode == root)
                        root = min;
                    else
                    {
                        if (parent.Value.CompareTo(currentNode.Value) > 0)
                            parent.Left = min;
                        else
                            parent.Right = min;
                    }
                }
            }
            return true;
        }
        public void RemoveTree(T value)
        {
            root = RemoveTree(root, value);
        }
        private TreeNode<T> RemoveTree(TreeNode<T> subroot, T value)
        {
            if (subroot == null)
                return subroot;
            if (value.CompareTo(subroot.Value) < 0)
                subroot.Left = RemoveTree(subroot.Left, value);
            else if (value.CompareTo(subroot.Value) > 0)
                subroot.Right = RemoveTree(subroot.Right, value);
            else if(subroot.Left != null && subroot.Right != null)
            {
                subroot.Value = FindMin(subroot.Right).Value;
                subroot.Right = RemoveTree(subroot.Right, subroot.Value);
            }
            else
            {
                if (subroot.Left != null)
                    subroot = subroot.Left;
                else if (subroot.Right != null)
                    subroot = subroot.Right;
                else
                    subroot = null;
            }
            return subroot;
        }
        private TreeNode<T> FindMin(TreeNode<T> subroot)
        {
            if (subroot.Left == null)
                return subroot;
            return FindMin(subroot.Left);
        }
        public int GetDeep()
        {
            return GetDeep(root);
        }
        private int GetDeep(TreeNode<T> subroot)
        {
            if (subroot == null)
                return 0;
            return 1 + Math.Max(GetDeep(subroot.Left), GetDeep(subroot.Right));
        }

        public int GetLeafs()
        {
            return GetLeafs(root);
        }
        private int GetLeafs(TreeNode<T> subroot)
        {
            if (subroot == null)
                return 0;
            if (subroot.Left == null && subroot.Right == null)
                return 1;
            return GetLeafs(subroot.Left) + GetLeafs(subroot.Right);
        }

        public int GetNodes()
        {
            return GetNodes(root);
        }
        private int GetNodes(TreeNode<T> subroot)
        {
            if (subroot == null)
                return 0;
            return 1 + GetNodes(subroot.Left) + GetNodes(subroot.Right);
        }

        public string LNR()
        {
            return LNR(root);
        }
        private string LNR(TreeNode<T> subroot)
        {
            if (subroot == null)
                return "";
            return LNR(subroot.Left) + " " + subroot.Value.ToString() + " " + LNR(subroot.Right);
        }

        public string NLR()
        {
            return NLR(root);
        }
        private string NLR(TreeNode<T> subroot)
        {
            if (subroot == null)
                return "";
            return subroot.Value.ToString() + " " + NLR(subroot.Left) + " " + NLR(subroot.Right);
        }

        public string LRN()
        {
            return LRN(root);
        }
        private string LRN(TreeNode<T> subroot)
        {
            if (subroot == null)
                return "";
            return LRN(subroot.Left) + " " + LRN(subroot.Right) + " " + subroot.Value.ToString();
        }

    }
}
