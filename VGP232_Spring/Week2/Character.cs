using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2
{
    public class Character : IDamageable
    {
        public int HP { get; set; }
        public int MP { get; set; }
        public string Name { get; set; }
        public Character(string name, int hp, int mp)
        {
            Name = name;
            HP = hp;
            MP = mp;
        }

        public void TakeDamage(int damage)
        {

        }
    }
}
