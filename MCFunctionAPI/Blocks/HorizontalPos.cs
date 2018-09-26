using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class HorizontalPos
    {
        public RelativeCoord X { get; set; }
        public RelativeCoord Z { get; set; }

        public override string ToString()
        {
            return X + " " + Z;
        }

    }
}
