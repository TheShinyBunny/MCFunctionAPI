using MCFunctionAPI.Blocks;
using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public abstract class DataContainer
    {

        public abstract ResultCommand GetData(string path);

        public abstract ResultCommand GetData(string path, double scale);

        public abstract void MergeData(NBT nbt);

        public abstract void RemoveData(string path);

        public DataModifier ModifyData(string path)
        {
            return new DataModifier(this, path);
        }

        public abstract string ToDataCommand();

        public class DataModifier
        {
            public readonly DataContainer container;
            public readonly string path;

            public DataModifier(DataContainer dataContainer, string path)
            {
                this.container = dataContainer;
                this.path = path;
            }

            public DataValueSource Append()
            {
                return new DataValueSource(this, "append");
            }

            public DataValueSource Prepend()
            {
                return new DataValueSource(this, "prepend");
            }

            public DataValueSource Set()
            {
                return new DataValueSource(this, "set");
            }

            public DataValueSource Merge()
            {
                return new DataValueSource(this, "merge");
            }

            public DataValueSource InsertBefore(int index)
            {
                return new DataValueSource(this, "insert before " + index);
            }
            

            public DataValueSource InsertAfter(int index)
            {
                return new DataValueSource(this, "insert after " + index);
            }
        }

        public class DataValueSource
        {
            private DataModifier modifier;
            private string mode;

            public DataValueSource(DataModifier modifier,string mode)
            {
                this.modifier = modifier;
                this.mode = mode;
            }

            public void Value(string value)
            {
                FunctionWriter.Write($"data modify {modifier.container.ToDataCommand()} {modifier.path} {mode} value {value}");
            }

            public void Value(NBT value)
            {
                Value(value.ToString());
            }

            public void From(DataContainer container, string path)
            {
                FunctionWriter.Write($"data modify {modifier.container.ToDataCommand()} {modifier.path} {mode} from {container.ToDataCommand()} {path}");
            }
            
        }
    }
}
