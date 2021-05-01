using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment2a
{
    public class WeaponCollection : List<Weapon>, IPeristence
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
            int lowestBA = 99999;

            for(int i = 0; i < this.Count; i++)
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

            for (int i =0; i < this.Count; i++)
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
        public bool Load(string filename)
        {
            if (!File.Exists(filename))
            {
                return false;
            }
            else
            {
                using (StreamReader reader = new StreamReader(filename))
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
        public bool Save(string filename)
        {
            
                FileStream fs;
                fs = File.Open(filename, FileMode.Create);
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
