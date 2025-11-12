using System;
using System.Collections.Generic;

namespace PROG7312_POE.Models
{
    public class DataStructures
    {
        // bst
        public class BSTNode
        {
            public Report Data;
            public BSTNode Left;
            public BSTNode Right;

            public BSTNode(Report report)
            {
                Data = report;
                Left = Right = null;
            }
        }

        public class BST
        {
            public BSTNode Root;

            public void Insert(Report report)
            {
                Root = InsertRec(Root, report);
            }

            private BSTNode InsertRec(BSTNode root, Report report)
            {
                if (root == null) return new BSTNode(report);

                if (string.Compare(report.StreetAddress, root.Data.StreetAddress) < 0)
                    root.Left = InsertRec(root.Left, report);
                else
                    root.Right = InsertRec(root.Right, report);

                return root;
            }

            public void InOrderTraversal(BSTNode node, List<Report> result)
            {
                if (node != null)
                {
                    InOrderTraversal(node.Left, result);
                    result.Add(node.Data);
                    InOrderTraversal(node.Right, result);
                }
            }
        }

        // avl
        public class AVLNode
        {
            public Report Data;
            public AVLNode Left;
            public AVLNode Right;
            public int Height;

            public AVLNode(Report report)
            {
                Data = report;
                Height = 1;
            }
        }

        public class AVLTree
        {
            public AVLNode Root;

            public void Insert(Report report)
            {
                Root = InsertRec(Root, report);
            }

            private AVLNode InsertRec(AVLNode node, Report report)
            {
                if (node == null) return new AVLNode(report);

                if (report.ReportId < node.Data.ReportId)
                    node.Left = InsertRec(node.Left, report);
                else
                    node.Right = InsertRec(node.Right, report);

                node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

                int balance = GetBalance(node);

                if (balance > 1 && report.ReportId < node.Left.Data.ReportId)
                    return RightRotate(node);

                if (balance < -1 && report.ReportId > node.Right.Data.ReportId)
                    return LeftRotate(node);

                if (balance > 1 && report.ReportId > node.Left.Data.ReportId)
                {
                    node.Left = LeftRotate(node.Left);
                    return RightRotate(node);
                }

                if (balance < -1 && report.ReportId < node.Right.Data.ReportId)
                {
                    node.Right = RightRotate(node.Right);
                    return LeftRotate(node);
                }

                return node;
            }

            private int GetHeight(AVLNode node) => node?.Height ?? 0;
            private int GetBalance(AVLNode node) => node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);

            private AVLNode RightRotate(AVLNode y)
            {
                AVLNode x = y.Left;
                AVLNode T2 = x.Right;

                x.Right = y;
                y.Left = T2;

                y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
                x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

                return x;
            }

            private AVLNode LeftRotate(AVLNode x)
            {
                AVLNode y = x.Right;
                AVLNode T2 = y.Left;

                y.Left = x;
                x.Right = T2;

                x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
                y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

                return y;
            }

            public void InOrderTraversal(AVLNode node, List<Report> result)
            {
                if (node != null)
                {
                    InOrderTraversal(node.Left, result);
                    result.Add(node.Data);
                    InOrderTraversal(node.Right, result);
                }
            }
        }

        // heap
        public class MinHeap
        {
            private List<Report> heap = new List<Report>();
            public int Count => heap.Count;

            public void Insert(Report report)
            {
                heap.Add(report);
                HeapifyUp(heap.Count - 1);
            }

            private void HeapifyUp(int index)
            {
                while (index > 0)
                {
                    int parent = (index - 1) / 2;
                    if (heap[index].ReportId >= heap[parent].ReportId) break;
                    Swap(index, parent);
                    index = parent;
                }
            }

            private void Swap(int i, int j)
            {
                var temp = heap[i];
                heap[i] = heap[j];
                heap[j] = temp;
            }

            public List<Report> ToList() => new List<Report>(heap);
        }

        // gpragh
        public class Graph
        {
            private Dictionary<int, List<int>> adjList = new Dictionary<int, List<int>>();

            public void AddVertex(int id)
            {
                if (!adjList.ContainsKey(id))
                    adjList[id] = new List<int>();
            }

            public void AddEdge(int fromId, int toId)
            {
                AddVertex(fromId);
                AddVertex(toId);
                adjList[fromId].Add(toId);
                adjList[toId].Add(fromId);
            }
        }
    }
}
