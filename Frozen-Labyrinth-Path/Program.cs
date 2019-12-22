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
            while (true)
            {
                try
                {
                    var origin = File.ReadAllText("data.json");
                    var json = JsonConvert.DeserializeObject<Dictionary<string, Map>>(origin);
                    verifyJson(json);

                    Console.WriteLine("Your position: ");
                    var start = Console.ReadLine();
                    validateMap(start, json);

                    Console.WriteLine("Where you want to go: ");
                    var finish = Console.ReadLine();

                    var foundMap = findPath(start, finish, json);

                    if (!string.IsNullOrEmpty(foundMap))
                    {
                        printPath(foundMap, json);
                    }
                    else
                    {
                        Console.WriteLine("Path not found!");
                        continue;
                    }

                    Console.WriteLine("Hit enter to search path again or type Q and hit enter to quit:");
                    var response = Console.ReadLine();
                    if (!string.IsNullOrEmpty(response))
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }
        }

        public static void validateMap(string map, Dictionary<string, Map> json)
        {
            if (!json.TryGetValue(map, out var value))
            {
                Console.WriteLine("Maps:");
                foreach (var pair in json)
                {
                    Console.WriteLine(pair.Key);
                }
                throw new Exception();
            }
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

        public static string findPath(string start, string finish, Dictionary<string, Map> json)
        {
            var queue = new Queue<string>();
            queue.Enqueue(start);
            json[start].Visited = true;

            while (queue.Count > 0)
            {
                var map = queue.Dequeue();
                if (map.Contains(finish))
                {
                    return map;
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

            return null;
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

            Console.WriteLine("\n Shortest path:");
            Console.WriteLine(path + "\n");
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
