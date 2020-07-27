
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Navigation_solution_
{
    public class Reader
    {
        public static List<Node<Entity>> ReadFile(string path)
        {
            var rootNodes = new List<Node<Entity>>();
            var childItems = new List<Entity>();


            if (!File.Exists(path))
            {
                Console.WriteLine("The file does not exist!");
            }
            using (StreamReader sr = new StreamReader(path))
            {
                var data = sr.ReadToEnd();

                var lines = data.Split("\n");
                foreach (var item in lines.Skip(1))
                {
                    var columns = item.Split(";");


                    var id = Convert.ToInt32(columns[0]);
                    var menuName = columns[1];
                    var parentID = ToNullableInt(columns[2]);
                    var isHidden = Convert.ToBoolean(columns[3]);
                    var linkURL = columns[4];


                    var entity = new Entity()
                    {
                        Id = id,
                        MenuName = menuName,
                        ParentID = parentID,
                        IsHidden = isHidden,
                        LinkURL = linkURL
                    };

                    if (parentID == null)
                    {
                       Console.WriteLine("Found root:" + entity.Id + " - " + entity.MenuName);
                        rootNodes.Add(new Node<Entity>()
                        {
                            Data = entity
                        });
                    }
                    else
                    {
                        childItems.Add(entity);
                    }


                    foreach (var rootNode in rootNodes)
                    {
                        foreach (var childItem in childItems.OrderBy(a => a.ParentID).ThenBy(b => b.MenuName))
                        {
                            var newNode = new Node<Entity>()
                            {
                                Data = childItem
                            };

                            Insert(rootNode, newNode);
                        }
                    }


                    Console.WriteLine(new string('-', 40));




                    foreach (var rootNode in rootNodes)
                    {
                        var indent = 0;
                        var previous = rootNode;
                        foreach (var node in rootNode.LevelOrder())
                        {
                            if (node.Data.IsHidden) continue;

                            if (previous.Data.ParentID != node.Data.ParentID)
                                indent++;


                            for (var i = 0; i <= indent; i++)
                                Console.Write("\t".Length + ".");

                            Console.WriteLine(indent + " - " + node.Data.MenuName);
                            previous = node;
                        }
                    }
                }
            }
            return rootNodes;
        }

        public static int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }


        public static void Insert(Node<Entity> rootNode, Node<Entity> targetNode)
        {
            foreach (var current in rootNode.LevelOrder())
            {
                if (current.Data.Id == targetNode.Data.ParentID)
                {
                    current.Children.Add(targetNode);
                    return;
                }
            }
        }
    }
}
