using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace reader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input your ATDD path below:");
            string path = Console.ReadLine();
            Dictionary<string, string> _given = new Dictionary<string, string>();
            Dictionary<string, string> _when = new Dictionary<string, string>();
            Dictionary<string, string> _then = new Dictionary<string, string>();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Definition,Statement");
            var Directories = Directory.GetDirectories(path);
            foreach (var dir in Directories)
            {
                
                try
                {
                    string[] f;
                    string scope = "";
                    f = Directory.GetFiles(dir, "*.feature",SearchOption.AllDirectories);
                    foreach (var file in f)
                    {
                        string[] lines = File.ReadAllLines(file);
                        foreach (var l in lines)
                        {
                            
                            if (Regex.IsMatch(l, "(^Given|^And)(.*)") && (!String.IsNullOrEmpty(l)))
                            {
                                if (!l.Contains("And"))
                                    scope = "given";
                                if ((!_given.ContainsKey(l.Replace("Given", "").Replace("And", ""))))
                                    _given.Add(l.Replace("Given", "").Replace("And", ""), "");
                            }
                            else if(Regex.IsMatch(l, "(^When|^And)(.*)") && (!String.IsNullOrEmpty(l)))
                            {
                                if(!l.Contains("And"))
                                    scope = "when";
                                if ((!_when.ContainsKey(l.Replace("When", "").Replace("And", ""))))
                                    _when.Add(l.Replace("When", "").Replace("And", ""), "");
                            }
                            else if (Regex.IsMatch(l, "(^Then|^And)(.*)") && (!String.IsNullOrEmpty(l)))
                            {
                                if (!l.Contains("And"))
                                    scope = "then";
                                if ((!_then.ContainsKey(l.Replace("Then", "").Replace("And", ""))))
                                    _then.Add(l.Replace("Then", "").Replace("And",""), "");
                            }
                            Console.WriteLine(file);
                        }
                    }
                }
                catch { }
                
                
            }
            foreach(var g in _given)
            {
                sb.AppendLine("Given," + g.Key);
            }
            foreach (var w in _when)
            {
                sb.AppendLine("When," + w.Key);
            }
            foreach (var t in _then)
            {
                sb.AppendLine("Then," + t.Key);
            }
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Gharkin.csv",sb.ToString());
            Console.ReadLine();
        }
    }
}
