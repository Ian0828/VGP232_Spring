using System;
using System.Collections.Generic;
using System.IO;

namespace Player
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = string.Empty;

            string outputFile = string.Empty;

            bool appendToFile = false;

            bool displayCount = false;

            bool sortEnabled = false;

            string sortColumnName = string.Empty;

            PlayerPool results = new PlayerPool();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h" || args[i] == "--help")
                {
                    Console.WriteLine("-i <path> or --input <path> : loads the input file path specified (required)");
                    Console.WriteLine("-o <path> or --output <path> : saves result in the output file path specified (optional)");

                    Console.WriteLine("-c or --count : displays the number of entries in the input file (optional)");
                    Console.WriteLine("-a or --append : enables append mode when writing to an existing out put file (optional)");
                    Console.WriteLine("-s or --sort <column name> : outputs the results sorted by column name");

                    break;
                }
                else if (args[i] == "-i" || args[i] == "--input")
                {
                    if (args.Length > i + 1)
                    {
                        ++i;
                        inputFile = args[i];

                        if (string.IsNullOrEmpty(inputFile))
                        {
                            Console.WriteLine("Input does NOT exist.");
                        }
                        else if (!File.Exists(inputFile))
                        {
                            Console.WriteLine("File does NOT exist.");
                        }
                        else
                        {
                            results.Load(inputFile);
                        }
                    }
                }
                else if (args[i] == "-s" || args[i] == "--sort")
                {
                    if (args.Length > i + 1)
                    {
                        sortEnabled = true;
                        sortColumnName = args[++i];
                    }
                }
                else if (args[i] == "-c" || args[i] == "--count")
                {
                    displayCount = true;
                }
                else if (args[i] == "-a" || args[i] == "--append")
                {
                    appendToFile = true;
                }
                else if (args[i] == "-o" || args[i] == "--output")
                {
                    if (args.Length > i + 1)
                    {
                        ++i;
                        string filePath = args[i];
                        if (string.IsNullOrEmpty(filePath))
                        {
                            Console.WriteLine("Output file does NOT exist");
                        }
                        else
                        {
                            outputFile = filePath;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The argument Arg[{0}] = [{1}] is invalid", i, args[i]);
                }
            }

            //results.SortBy(sortColumnName);
            if (sortEnabled)
            {
                Console.WriteLine($"Sorting by {sortColumnName}");

                if (sortColumnName.ToLower() == "name")
                {
                    results.Sort(Player.CompareByName);
                }
                else if (sortColumnName.ToLower() == "overall")
                {
                    results.Sort(Player.CompareByOverall);
                }
                else if (sortColumnName.ToLower() == "position")
                {
                    results.Sort(Player.CompareByPosition);
                }
                else if (sortColumnName.ToLower() == "shooting")
                {
                    results.Sort(Player.CompareByShooting);
                }
                else if (sortColumnName.ToLower() == "passing")
                {
                    results.Sort(Player.CompareByPassing);
                }
                else if (sortColumnName.ToLower() == "speed")
                {
                    results.Sort(Player.CompareBySpeed);
                }
                else if (sortColumnName.ToLower() == "vertical")
                {
                    results.Sort(Player.CompareByVertical);
                }
                else if (sortColumnName.ToLower() == "height")
                {
                    results.Sort(Player.CompareByHeight);
                }
                else if (sortColumnName.ToLower() == "Weight")
                {
                    results.Sort(Player.CompareByWeight);
                }
                else
                {
                    Console.WriteLine("Argument is wrong");
                }
            }

            if (displayCount)
            {
                Console.WriteLine("There are {0} entries", results.Count);
            }
            if (results.Count > 0)
            {
                if (!string.IsNullOrEmpty(outputFile))
                {
                    results.Save(outputFile);
                    //FileStream fs;
                    //
                    //// Check if the append flag is set, and if so, then open the file in append mode; otherwise, create the file to write.
                    //if (appendToFile && File.Exists((outputFile)))
                    //{
                    //    fs = File.Open(outputFile, FileMode.Append);
                    //}
                    //else
                    //{
                    //    fs = File.Open(outputFile, FileMode.Create);
                    //}
                    //
                    // //opens a stream writer with the file handle to write to the output file.
                    //using (StreamWriter writer = new StreamWriter(fs))
                    //{
                    //    // Hint: use writer.WriteLine
                    //    // TODO: write the header of the output "Name,Type,Rarity,BaseAttack"
                    //    writer.WriteLine("Name, Overall, Position, Shooting, Passing, Speed, Vertical, Dribble, Height, Weight");
                    //    // TODO: use the writer to output the results.
                    //    int j = 0;
                    //    foreach (var line in results)
                    //    {
                    //        writer.WriteLine(results[j]);
                    //        j++;
                    //    }
                    //    // TODO: print out the file has been saved.
                    //    Console.WriteLine("The file has been saved");
                    //}
                }
                else
                {
                    for (int i = 0; i < results.Count; i++)
                    {
                        Console.WriteLine(results[i]);
                    }
                }
            }

            Console.WriteLine("Done!");
        }
    }
}
