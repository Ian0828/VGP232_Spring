using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2
{
    public class CharacterCollection : List<Character>
    {
        void SortBy(string propertyName)
        {
            if (propertyName == "HP")
            {
                this.Sort(CompareHP);
            }
        }

        private int CompareHP(Character x, Character y)
        {
            return x.HP - y.HP;
        }

        int GetMaxHPFromCharacters()
        {
            int maxHP = 0;
            for(int i = 0; i < this.Count; ++i)
            {
                if (this[i].HP > maxHP)
                {
                    maxHP = this[i].HP;
                }
            }
        }
    }
}
