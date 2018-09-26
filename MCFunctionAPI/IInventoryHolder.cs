using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public interface IInventoryHolder
    {

        void ReplaceItem(string slot, Item item);

        void ReplaceItem(string slot, Item item, uint count);

    }
}
