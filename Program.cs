using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        public static List<Node> nodes;

        public static List<catalog> catalogs = new List<catalog>();
        static void Main(string[] args)
        {
           Intialnodes();

           myMethod(0);

            foreach (var catalog in catalogs)
            {
                Console.WriteLine($"id : {catalog.id} count : {catalog.count}");
            }

            Console.ReadKey();
        }

        public static void myMethod(int id)
        {
            var root = nodes.Where(x => x.id == id).ToList();
            var childIds = root[0].childIds;
            var childs = nodes.Where(x => childIds.Contains(x.id));
            foreach (var child in childs)
            {
                if (child.isLegal)
                {
                    catalogs.Add(child.catalog);
                }
                else
                {
                    catalogs.Add(new catalog(child.id, 0));
                    myMethod(child.id);
                }
            }
        }

        private static void Intialnodes()
        {
            //Root 
            var root = new Node(0, null, null, false);                
            //two child of root
            var node1 = new Node(1, root.id, null, true);
            var node2 = new Node(2, root.id, null, false);
            root.childIds = new int[] { node1.id, node2.id };
            //one child of node2
            var node3 = new Node(3, node2.id, null, true);
            node2.childIds = new int[] { node3.id };

            nodes = new List<Node>();
            nodes.Add(root);
            nodes.Add(node1);
            nodes.Add(node2);
            nodes.Add(node3);
        }
    }

    class Node
    {

        public int id;
        public int? parentId;
        public int[] childIds;
        public bool isLegal;

        public catalog catalog;

        public Node(int id, int? parentId, int[] childIds, bool isLegal)
        {
            this.id = id;
            this.parentId = parentId;
            this.childIds = childIds;
            this.isLegal = isLegal;

            this.catalog = new catalog(id);
        }
    }

    class catalog
    {
        public int id;
        public int count = new Random().Next(1, 5);

        public catalog(int id)
        {
            this.id = id;
        }

        public catalog(int id, int count)
        {
            this.id = id;
            this.count = count;
        }
    }
}
