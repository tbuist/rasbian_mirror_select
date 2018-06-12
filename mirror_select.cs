using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace mirror_select
{
    class mirror_select
    {
        static void Main(string[] args)
        {
            var latency = new List<Tuple<string, long>>();

            var filename = "mirrors.txt";
            var path = Path.Combine(Directory.GetCurrentDirectory(), filename);
            if (!File.Exists(path))
            {
                Console.WriteLine($"Couldn't find the specified file - {path}");
                Console.ReadKey();
                return;
            }

            var lines = File.ReadLines(path);
            foreach (var line in lines)
            {
                var time = ResponseTime(line.Trim());
                if (time > 0)
                {
                    latency.Add(Tuple.Create(line, time));
                    Console.WriteLine($"Testing {line}...{time} milliseconds");
                }
            }

            latency.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            latency.Reverse();
            Console.WriteLine("The fastest mirrors are:\n");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"\t{i}. {latency[i].Item1} - {latency[i].Item2} ms");
            }
            Console.ReadKey();
        }

        static long ResponseTime(string url)
        {
            var ping = new System.Net.NetworkInformation.Ping();
            List<long> times = new List<long> { };

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    var result = ping.Send(url);

                    if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
                        return -1;
                    else
                        times.Add(result.RoundtripTime);
                }
                catch
                {
                    return -1;
                }
            }

            return times.Min();
        }
    }
}
