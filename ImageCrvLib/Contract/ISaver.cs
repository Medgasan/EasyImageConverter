using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCrvLib.Contract
{
    public interface ISaver
    {
        string Description { get; }
        List<string> Extension { get; }
        void Convert();
    }
}
