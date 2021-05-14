using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Serialization;

namespace Assignment2b
{
    public class WeaponCollection : List<Weapon>, IPeristence, IXmlSerializable, IJsonSerializable, ICsvSerializable
    {
        public int GetHighestBaseAttack()
        {
            int highestBA = 0;

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].BaseAttack >= highestBA)
                {
                    highestBA = this[i].BaseAttack;
                }
            }

            return highestBA;
        }
        public int GetLowestBaseAttack()
        {
            //ERROR -1: To get the lowest Damage at start, you can set this variable to the 
            //maximum value supported by an integer
            //int lowestBA = int.MaxValue

            int lowestBA = int.MaxValue;

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].BaseAttack <= lowestBA)
                {
                    lowestBA = this[i].BaseAttack;
                }
            }
            return lowestBA;
        }
        public List<Weapon> GetAllWeaponOfType(WeaponType type)
        {
            List<Weapon> nWeapon = new List<Weapon>();

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Type == type)
                {
                    nWeapon.Add(this[i]);
                }
            }
            return nWeapon;
        }
        public List<Weapon> GetAllWeaponOfRarity(int stars)
        {
            List<Weapon> nWeapon = new List<Weapon>();

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Rarity == stars)
                {
                    nWeapon.Add(this[i]);
                }
            }
            return nWeapon;
        }
        public void SortBy(string columnName)
        {
            WeaponCollection results = new WeaponCollection();

            if (columnName.ToLower() == "name")
            {
                results.Sort(Weapon.CompareByName);
            }
            else if (columnName.ToLower() == "type")
            {
                results.Sort(Weapon.CompareByType);
            }
            else if (columnName.ToLower() == "image")
            {
                results.Sort(Weapon.CompareByRarity);
            }
            else if (columnName.ToLower() == "rarity")
            {
                results.Sort(Weapon.CompareByRarity);
            }
            else if (columnName.ToLower() == "baseattack")
            {
                results.Sort(Weapon.CompareByBaseAttack);
            }
            else if (columnName.ToLower() == "secondaryStat")
            {
                results.Sort(Weapon.CompareByRarity);
            }
            else if (columnName.ToLower() == "passive")
            {
                results.Sort(Weapon.CompareByRarity);
            }
            else
            {
                Console.WriteLine("Argument is wrong");
            }
        }

        //ERROR: -2. You are always adding in your load. You should first clear your list and then add.
        //A good function would be:
        //public bool Load(string filename)
        //{
        //    // Use the streamreader to add weapons to the previously created list
        //    using (StreamReader reader = new StreamReader(filename))
        //    {
        //        // Clear the list before loading data
        //        this.Clear();

        //        // Skip the first line because header does not need to be parsed.
        //        // Name, Type, Image, Rarity, BaseAttack
        //        string header = reader.ReadLine();

        //        // The rest of the lines looks like the following:
        //        // Skyrider Sword,Sword,3,38

        //        while (reader.Peek() > 0)
        //        {
        //            // Create a null weapon and a string that contais the line's content
        //            string line = reader.ReadLine();
        //            Weapon newWeapon = null;

        //            // Check if parsing is possible
        //            if (Weapon.TryParse(line, out newWeapon))
        //            {
        //                // Add the Weapon to the list
        //                this.Add(newWeapon);
        //            }
        //        }
        //    }

        //    // Return true when the weapons are succesfully loaded
        //    return true;
        //}
        public bool Load(string fileName)
        {
            this.Clear();
            string extension = Path.GetExtension(fileName);

            if (extension == ".xml")
            {
                return LoadXML(fileName);
            }
            else if (extension == ".json")
            {
                return LoadJSON(fileName);
            }
            else if (extension == ".csv")
            {
                return LoadCSV(fileName);
            }

            return false;
        }
        public bool Save(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            if (extension == ".xml")
            {
                return SaveAsXML(fileName);
            }
            else if (extension == ".json")
            {
                return SaveAsJSON(fileName);
            }
            else if (extension == ".csv")
            {
                return SaveAsCSV(fileName);
            }

            return false;
        }
        public bool LoadXML(string path)
        {
            this.Clear();
            XmlSerializer xml = new XmlSerializer(typeof(WeaponCollection));
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    this.AddRange((WeaponCollection)xml.Deserialize(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
        public bool SaveAsXML(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(WeaponCollection));

            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    xml.Serialize(writer, this);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
        public bool LoadJSON(string path)
        {
            this.Clear();
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    this.AddRange(JsonSerializer.Deserialize<WeaponCollection>(reader.ReadToEnd()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
        public bool SaveAsJSON(string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(JsonSerializer.Serialize<WeaponCollection>(this));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
        public bool LoadCSV(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            else
            {
                using (StreamReader reader = new StreamReader(path))
                {

                    string header = reader.ReadLine();
                    while (reader.Peek() > 0)
                    {
                        string line = reader.ReadLine();

                        if (Weapon.TryParse(line, out Weapon weapon))
                        {
                            this.Add(weapon);
                        }

                    }
                }
                return true;
            }
        }
        public bool SaveAsCSV(string path)
        {
            FileStream fs;
            fs = File.Open(path, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine("Name, Type, Image, Rarity, BaseAttack, SecondaryStat, Passive");

                foreach (var line in this)
                {
                    writer.WriteLine(line);
                }
                Console.WriteLine("The file has been saved");
            }
            return true;
        }
    }
}
