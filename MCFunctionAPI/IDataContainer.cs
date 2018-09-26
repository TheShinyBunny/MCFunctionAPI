using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public interface IDataContainer
    {

        void GetDate(string path);

        void GetData(string path, double scale);

        void MergeData(NBT nbt);

        void RemoveData(string path);

    }
}
