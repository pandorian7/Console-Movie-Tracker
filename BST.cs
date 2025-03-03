using System;
using System.Collections.Generic;

namespace DSA
{
    public class TreeNode<T>
    {
        public T Key;
        public TreeNode<T>? Left;
        public TreeNode<T>? Right;


        public TreeNode(T key)
        {
            Key = key;
            Left = null;
            Right = null;
        }
    }
    public class BST<T>
    {
        public int Count { get; private set; }
        private TreeNode<T>? Root { get; set; }
        private Func<T, IComparable> Key { get; }

        public BST(Func<T, IComparable> keySelector)
        {
            Key = keySelector;
            Root = null;
        }

        

        private TreeNode<T> InsertRecursively(T item, TreeNode<T>? root)
        {
            if (root == null)
            {
                Count++;
                return new TreeNode<T>(item);
            }

            if (Key(item).CompareTo(Key(root.Key)) < 0)
            {
                root.Left = InsertRecursively(item, root.Left);
            }
            else if (Key(item).CompareTo(Key(root.Key)) > 0)
            {
                root.Right = InsertRecursively(item, root.Right);
            }

            return root;
        }
        public void Insert(T key)
        {
            Root = InsertRecursively(key, Root);
        }

        // public void InsertIteratively(T key)
        // {
        //     TreeNode<T>? current = Root;
        //     TreeNode<T>? parent = null;

        //     if (Root == null)
        //     {
        //         Root = new TreeNode<T>(key);
        //         return;
        //     }

        //     while (current != null)
        //     {
        //         if (key.CompareTo(current.Key) > 0)
        //         {
        //             parent = current;
        //             current = current.Right;
        //         }

        //         else if (key.CompareTo(current.Key) < 0)
        //         {
        //             parent = current;
        //             current = current.Left;
        //         }

        //         else { return; } //duplicate key
        //     }
        //     if (key.CompareTo(parent!.Key) < 0) parent.Left = new TreeNode<T>(key);
        //     else parent.Right = new TreeNode<T>(key);
        // }

            private void PrintInOrder(TreeNode<T>? root)
        {
            if (root != null)
            {
                PrintInOrder(root.Left);
                Console.WriteLine(root.Key);
                PrintInOrder(root.Right);
            }
        }

        private void PrintPreOrder(TreeNode<T>? root)
        {
            if (root != null)
            {
                Console.WriteLine(root.Key);
                PrintPreOrder(root.Left);
                PrintPreOrder(root.Right);
            }
        }

        private void PrintPostOrder(TreeNode<T>? root)
        {
            if (root != null)
            {
                PrintPostOrder(root.Left);
                PrintPostOrder(root.Right);
                Console.WriteLine(root.Key);
            }
        }

        

        // public void Delete(T key)
        // {
        //     Root = DeleteRecursively(Root, key);
        // }

        // private TreeNode<T>? DeleteRecursively(TreeNode<T>? root, T key)
        // {
        //     if (root == null)
        //     {
        //         return root;
        //     }

        //     if (key.CompareTo(root.Key) > 0)
        //     {
        //         root.Left = DeleteRecursively(root.Left, key);
        //     }

        //     else if (key.CompareTo(root.Key) > 0)
        //     {
        //         root.Right = DeleteRecursively(root.Right, key);
        //     }

        //     else
        //     {
        //         if(root?.Right == null)
        //         {
        //             return root?.Left;
        //         }
        //         else if (root.Left == null)
        //         {
        //             return root?.Right;
        //         }

        //         // root.Key = FindMin(root.Right);
        //         root.Right = DeleteRecursively(root.Right, root.Key);
        //     }

        //     return root;

        // }

        //public int FindMin(TreeNode? root)
        //{
        //    while(root?.Left != null)
        //    {
        //        root = root.Left;
        //    }
        //    return root.Key;
        //}

        //private int FindMax(TreeNode? root)
        //{
        //    while (root.Right != null)
        //    {
        //        root = root.Right;
        //    }
        //    return root.Key;
        //}

        public void Print()
        {
            PrintInOrder(Root);
            // Console.WriteLine(FindMin(Root));
        }
    }
}