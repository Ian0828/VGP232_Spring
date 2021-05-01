using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Week3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string binaryFileName = "mySkill.dat";
            string xmlFileName = "mySkill.xml";
            Skill mySkill = new Skill() { Name = "Thunder Strike", Cost = 5, Modifier = 1 };

            FileStream fs = new FileStream(xmlFileName, FileMode.Create);
            //XmlSerializer xmlWriter = new XmlSerializer(typeof(Skill));
            
            

        }

        private static void LoadSkill(string fileName)
        {
            // 1 - open a file: Read
            FileStream fs = new FileStream("mySkill.dat", FileMode.Open);
            // 2 - create a BinaryFormatter
            BinaryFormatter bf = new BinaryFormatter();
            // 3 - create a new object
            // 4 - Deserialize
            Skill loadedSkill = (Skill)bf.Deserialize(fs);

            Console.WriteLine("Binary file loaded!");
            Console.WriteLine(loadedSkill);
        }

        private static void SaveSkill(string fileName, Skill mySkill)
        {
            // 1 - opening a file
            FileStream fs = new FileStream("mySkill.dat", FileMode.Create);
            // 2 - create a BinaryFormatter
            BinaryFormatter bf = new BinaryFormatter();
            // 3 - Serialize your file
            bf.Serialize(fs, mySkill);
            fs.Close();
            Console.WriteLine("Binary file saved!");
        }
    }
}
