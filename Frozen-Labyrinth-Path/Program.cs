using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Frozen_Labyrinth_Path
{
    class Program
    {
        static void Main(string[] args)
        {
            var origin = File.ReadAllText("data.json");
            var json = JsonConvert.DeserializeObject<Dictionary<string, Map>>(origin);
            verifyJson(json);

            Console.WriteLine("Your position: ");
            var start = Console.ReadLine();

            Console.WriteLine("Where you want to go: ");
            var finish = Console.ReadLine();

            var isFound = findPath(start, finish, json);

            if (isFound)
            {
                printPath(finish, json);
            }

            Console.WriteLine(isFound);
            Console.ReadLine();
        }

        public static void verifyJson(Dictionary<string, Map> json)
        {
            foreach (var map in json)
            {
                var name = map.Key;
                if (map.Value.TL != String.Empty)
                {
                    var valid = json[map.Value.TL].TL == name || json[map.Value.TL].TR == name || json[map.Value.TL].BR == name
                        || json[map.Value.TL].BL == name;
                    if (!valid)
                    {
                        throw new Exception($"Gate to map \"{name}\" was not found in \"{map.Value.TL}\" map");
                    }
                }
                if (map.Value.TR != String.Empty)
                {
                    var valid = json[map.Value.TR].TL == name || json[map.Value.TR].TR == name || json[map.Value.TR].BR == name
                        || json[map.Value.TR].BL == name;
                    if (!valid)
                    {
                        throw new Exception($"Gate to map \"{name}\" was not found in \"{map.Value.TR}\" map");
                    }
                }
                if (map.Value.BR != String.Empty)
                {
                    var valid = json[map.Value.BR].TL == name || json[map.Value.BR].TR == name || json[map.Value.BR].BR == name
                        || json[map.Value.BR].BL == name;
                    if (!valid)
                    {
                        throw new Exception($"Gate to map \"{name}\" was not found in \"{map.Value.BR}\" map");
                    }
                }
                if (map.Value.BL != String.Empty)
                {
                    var valid = json[map.Value.BL].TL == name || json[map.Value.BL].TR == name || json[map.Value.BL].BR == name
                        || json[map.Value.BL].BL == name;
                    if (!valid)
                    {
                        throw new Exception($"Gate to map \"{name}\" was not found in \"{map.Value.BL}\" map");
                    }
                }
            }
        }

        public static bool findPath(string start, string finish, Dictionary<string, Map> json)
        {
            var queue = new Queue<string>();
            queue.Enqueue(start);
            json[start].Visited = true;

            while (queue.Count > 0)
            {
                var map = queue.Dequeue();
                if (map == finish)
                {
                    return true;
                }
                else
                {
                    var nodes = new Dictionary<string, string>();
                    nodes.Add("TL", json[map].TL);
                    nodes.Add("TR", json[map].TR);
                    nodes.Add("BR", json[map].BR);
                    nodes.Add("BL", json[map].BL);
                    foreach (var node in nodes)
                    {
                        if (node.Value != "" && !json[node.Value].Visited)
                        {
                            queue.Enqueue(node.Value);
                            json[node.Value].Visited = true;
                            json[node.Value].Parent = map;
                            json[node.Value].Via = node.Key;
                        }
                    }
                }
            }

            return false;
        }

        public static void printPath(string finish, Dictionary<string, Map> json)
        {
            var path = finish;

            var curr = finish;

            while (curr != null)
            {
                path = $"{json[curr]?.Parent}--[{json[curr].Via}]->{path}";
                curr = json[curr].Parent;
            }

            Console.WriteLine(path);
        }
    }

    public class Map
    {
        public string TL { get; set; }
        public string TR { get; set; }
        public string BR { get; set; }
        public string BL { get; set; }
        public bool Visited { get; set; }
        public string Parent { get; set; }
        public string Via { get; set; }
    }
}
