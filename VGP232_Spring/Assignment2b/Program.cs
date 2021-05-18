using System;
using System.Collections.Generic;
using System.IO;

// TODO: Fill in your name and student number.
// Assignment 2b
// NAME: Yung-Hsiang Ma
// STUDENT NUMBER: 1940028

//GRADE: 100/100

namespace Assignment2b
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Variables and flags

            // The path to the input file to load.
            string inputFile = string.Empty;

            // The path of the output file to save.
            string outputFile = string.Empty;

            // The flag to determine if we overwrite the output file or append to it.
            bool appendToFile = false;

            // The flag to determine if we need to display the number of entries
            bool displayCount = false;

            // The flag to determine if we need to sort the results via name.
            bool sortEnabled = false;

            // The column name to be used to determine which sort comparison function to use.
            string sortColumnName = string.Empty;

            // The results to be output to a file or to the console
            WeaponCollection results = new WeaponCollection();

            for (int i = 0; i < args.Length; i++)
            {
                // h or --help for help to output the instructions on how to use it
                if (args[i] == "-h" || args[i] == "--help")
                {
                    Console.WriteLine("-i <path> or --input <path> : loads the input file path specified (required)");
                    Console.WriteLine("-o <path> or --output <path> : saves result in the output file path specified (optional)");

                    // TODO: include help info for count
                    //"-c or --count : displays the number of entries in the input file (optional).";
                    Console.WriteLine("-c or --count : displays the number of entries in the input file (optional)");
                    // TODO: include help info for append
                    //"-a or --append : enables append mode when writing to an existing output file (optional)";
                    Console.WriteLine("-a or --append : enables append mode when writing to an existing out put file (optional)");
                    // TODO: include help info for sort
                    //"-s or --sort <column name> : outputs the results sorted by column name";
                    Console.WriteLine("-s or --sort <column name> : outputs the results sorted by column name");

                    break;
                }
                else if (args[i] == "-i" || args[i] == "--input")
                {
                    // Check to make sure there's a second argument for the file name.
                    if (args.Length > i + 1)
                    {
                        // stores the file name in the next argument to inputFile
                        ++i;
                        inputFile = args[i];

                        if (string.IsNullOrEmpty(inputFile))
                        {
                            // TODO: print no input file specified.
                            Console.WriteLine("Input does NOT exist.");
                        }
                        else if (!File.Exists(inputFile))
                        {
                            // TODO: print the file specified does not exist.
                            Console.WriteLine("File does NOT exist.");
                        }
                        else
                        {
                            // This function returns a List<Weapon> once the data is parsed.
                            results.Load(inputFile);
                        }
                    }
                }
                else if (args[i] == "-s" || args[i] == "--sort")
                {
                    // TODO: set the sortEnabled flag and see if the next argument is set for the column name
                    // TODO: set the sortColumnName string used for determining if there's another sort function.
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
                    // TODO: set the appendToFile flag
                    appendToFile = true;
                }
                else if (args[i] == "-o" || args[i] == "--output")
                {
                    // validation to make sure we do have an argument after the flag
                    if (args.Length > i + 1)
                    {
                        // increment the index.
                        ++i;
                        string filePath = args[i];
                        if (string.IsNullOrEmpty(filePath))
                        {
                            // TODO: print No output file specified.
                            Console.WriteLine("Output file does NOT exist");
                        }
                        else
                        {
                            // TODO: set the output file to the outputFile
                            outputFile = filePath;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The argument Arg[{0}] = [{1}] is invalid", i, args[i]);
                }
            }

            //ERROR: -3. Why are you checking again the columnName? Your SortBy should do this.
            //results.SortBy(columnName)
            if (sortEnabled)
            {
                // TODO: add implementation to determine the column name to trigger a different sort. (Hint: column names are the 4 properties of the weapon class)
                // print: Sorting by <column name> e.g. BaseAttack
                // Sorts the list based off of the Weapon name.
                Console.WriteLine($"Sorting by {sortColumnName}");

                if (sortColumnName.ToLower() == "name")
                {
                    results.Sort(Weapon.CompareByName);
                }
                else if (sortColumnName.ToLower() == "type")
                {
                    results.Sort(Weapon.CompareByType);
                }
                else if (sortColumnName.ToLower() == "rarity")
                {
                    results.Sort(Weapon.CompareByRarity);
                }
                else if (sortColumnName.ToLower() == "baseattack")
                {
                    results.Sort(Weapon.CompareByBaseAttack);
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

                    //// Check if the append flag is set, and if so, then open the file in append mode; otherwise, create the file to write.
                    //if (appendToFile && File.Exists((outputFile)))
                    //{
                    //    fs = File.Open(outputFile, FileMode.Append);
                    //}
                    //else
                    //{
                    //    fs = File.Open(outputFile, FileMode.Create);
                    //}

                    // opens a stream writer with the file handle to write to the output file.
                    //using (StreamWriter writer = new StreamWriter(fs))
                    //{
                    //    // Hint: use writer.WriteLine
                    //    // TODO: write the header of the output "Name,Type,Rarity,BaseAttack"
                    //    writer.WriteLine("Name, Type, Rarity, BaseAttack");
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
                    // prints out each entry in the weapon list results.
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