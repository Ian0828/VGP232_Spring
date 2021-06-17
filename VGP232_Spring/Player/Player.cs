using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player
{
    public enum PlayerPosition
    {
        PG,
        SG,
        SF,
        PF,
        C
    }
    public class Player
    {
        public string Name { get; set; }
        public int Overall { get; set; }
        public PlayerPosition Position { get; set; }
        public int Shooting { get; set; }
        public int Passing { get; set; }
        public int Speed { get; set; }
        public int Vertical { get; set; }
        public int Dribble { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        // try parse
        public static bool TryParse(string rawData, out Player player)
        {
            string[] values = rawData.Split(',');
            player = new Player();
            if (values.Length == 10)
            {
                try
                {
                    player.Name = values[0].ToString();
                    player.Overall = int.Parse(values[1]);
                    player.Position = Enum.Parse<PlayerPosition>(values[1]);
                    player.Shooting = int.Parse(values[3]);
                    player.Passing = int.Parse(values[4]);
                    player.Speed = int.Parse(values[5]);
                    player.Vertical = int.Parse(values[6]);
                    player.Dribble = int.Parse(values[7]);
                    player.Height = int.Parse(values[8]);
                    player.Weight = int.Parse(values[9]);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static int CompareByName(Player left, Player right)
        {
            return left.Name.CompareTo(right.Name);
        }
        public static int CompareByOverall(Player left, Player right)
        {
            return left.Overall.CompareTo(right.Overall);
        }
        public static int CompareByPosition(Player left, Player right)
        {
            return left.Position.CompareTo(right.Position);
        }
        public static int CompareByShooting(Player left, Player right)
        {
            return left.Shooting.CompareTo(right.Shooting);
        }
        public static int CompareByPassing(Player left, Player right)
        {
            return left.Passing.CompareTo(right.Passing);
        }
        public static int CompareBySpeed(Player left, Player right)
        {
            return left.Speed.CompareTo(right.Speed);
        }
        public static int CompareByVertical(Player left, Player right)
        {
            return left.Vertical.CompareTo(right.Vertical);
        }
        public static int CompareByDribble(Player left, Player right)
        {
            return left.Dribble.CompareTo(right.Dribble);
        }
        public static int CompareByHeight(Player left, Player right)
        {
            return left.Height.CompareTo(right.Height);
        }
        public static int CompareByWeight(Player left, Player right)
        {
            return left.Weight.CompareTo(right.Weight);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, Shooting:{3}, Passing:{4}, Speed:{5}, Vertical:{6}, Dribble:{7}, Height:{8}cm, Weight:{9}kg", Name, Overall, Position, Shooting, Passing, Speed, Vertical, Dribble, Height, Weight);
        }
    }
}
