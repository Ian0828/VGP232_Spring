using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLib
{
    public interface IPeristence
    {
        bool Load(string fileName);
        bool Save(string fileName);
    }
}
