using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class Position : IDataContainer
    {

        public RotativeCoord X { get; set; }
        public RotativeCoord Y { get; set; }
        public RotativeCoord Z { get; set; }

        public Position(RotativeCoord x, RotativeCoord y, RotativeCoord z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static readonly Position Here = "~ ~ ~";

        public static implicit operator Position(string s)
        {
            return Of(s);
        }

        public static Position Of(string s)
        {
            string[] slices = s.Split(' ');
            return new Position(slices[0], slices[1], slices[2]);
        }

        public static Position Of(RotativeCoord x, RotativeCoord y, RotativeCoord z)
        {
            return new Position(x, y, z);
        }

        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }

        public void GetDate(string path)
        {
            FunctionWriter.Write($"data get block {this} {path}");
        }

        public void GetData(string path, double scale)
        {
            FunctionWriter.Write($"data get block {this} {path} {scale}");
        }

        public void MergeData(NBT nbt)
        {
            FunctionWriter.Write($"data merge block {this} {nbt}");
        }

        public void RemoveData(string path)
        {
            FunctionWriter.Write($"data remove block {this} {path}");
        }
    }
}
