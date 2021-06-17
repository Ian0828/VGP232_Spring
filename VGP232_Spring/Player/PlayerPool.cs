using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Player
{
    public class PlayerPool : List<Player>, IPeristence, IXmlSerializable, IJsonSerializable, ICsvSerializable
    {
        public int GetHighestOverall()
        {
            int highestOverall = 0;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Overall >= highestOverall)
                {
                    highestOverall = this[i].Overall;
                }
            }
            return highestOverall;
        }
        public int GetLowestOverall()
        { 
            int lowestOverall = int.MaxValue;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Overall <= lowestOverall)
                {
                    lowestOverall = this[i].Overall;
                }
            }
            return lowestOverall;
        }
        public List<Player> GetAllPlayerOfPosition(PlayerPosition pos)
        {
            List<Player> nPlayer = new List<Player>();

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Position == pos)
                {
                    nPlayer.Add(this[i]);
                }
            }
            return nPlayer;
        }


        public void SortBy(string columnName)
        {
            if (columnName.ToLower() == "name")
            {
                this.Sort(Player.CompareByName);
            }
            else if (columnName.ToLower() == "overall")
            {
                this.Sort(Player.CompareByOverall);
            }
            else if (columnName.ToLower() == "position")
            {
                this.Sort(Player.CompareByPosition);
            }
            else if (columnName.ToLower() == "shooting")
            {
                this.Sort(Player.CompareByShooting);
            }
            else if (columnName.ToLower() == "passing")
            {
                this.Sort(Player.CompareByPassing);
            }
            else if (columnName.ToLower() == "speed")
            {
                this.Sort(Player.CompareBySpeed);
            }
            else if (columnName.ToLower() == "vertical")
            {
                this.Sort(Player.CompareByVertical);
            }
            else if (columnName.ToLower() == "height")
            {
                this.Sort(Player.CompareByHeight);
            }
            else if (columnName.ToLower() == "Weight")
            {
                this.Sort(Player.CompareByWeight);
            }
            else
            {
                Console.WriteLine("Argument is wrong");
            }
        }
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
            XmlSerializer xml = new XmlSerializer(typeof(PlayerPool));
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    this.AddRange((PlayerPool)xml.Deserialize(reader));
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
            XmlSerializer xml = new XmlSerializer(typeof(PlayerPool));

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
                    this.AddRange(JsonSerializer.Deserialize<PlayerPool>(reader.ReadToEnd()));
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
                    writer.Write(JsonSerializer.Serialize<PlayerPool>(this));
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

                        if (Player.TryParse(line, out Player player))
                        {
                            this.Add(player);
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
                writer.WriteLine("Name, Overall, Position, Shooting, Passing, Speed, Vertical, Dribble, Height, Weight");

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
