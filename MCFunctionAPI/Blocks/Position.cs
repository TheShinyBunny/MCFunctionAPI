using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class Position : DataContainer
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

        public static Position Here
        {
            get
            {
                return "~ ~ ~";
            }
        }

        public Position Add(double x, double y, double z)
        {
            return new Position(X + x, Y + y, Z + z);
        }

        public Position Subtract(double x, double y, double z)
        {
            return new Position(X - x, Y - y, Z - z);
        }

        public Position Rotative()
        {
            return new Position(X.Rotated(), Y.Rotated(), Z.Rotated());
        }

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

        public static Position Above(int blocks)
        {
            return Here.Add(0, blocks, 0);
        }

        public static Position Above()
        {
            return Above(1);
        }

        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }

        public override ResultCommand GetData(string path)
        {
            return new ResultCommand($"data get block {this} {path}");
        }

        public override ResultCommand GetData(string path, double scale)
        {
            return new ResultCommand($"data get block {this} {path} {scale}");
        }

        public override void MergeData(NBT nbt)
        {
            FunctionWriter.Write($"data merge block {this} {nbt}");
        }

        public override void RemoveData(string path)
        {
            FunctionWriter.Write($"data remove block {this} {path}");
        }

        public override string ToDataCommand()
        {
            return "block " + this;
        }
    }
}
