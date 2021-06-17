using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Player
{
    public interface IPeristence
    {
        bool Load(string fileName);
        bool Save(string fileName);
    }
}
