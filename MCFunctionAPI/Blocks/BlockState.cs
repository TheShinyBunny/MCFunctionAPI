using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class BlockState : IBlockState
    {

        private List<State> states;
        private Block block;

        public BlockState()
        {
            states = new List<State>();
        }

        public BlockState(Block of)
        {
            this.block = of;
        }

        public BlockState PutInt(string name, int value)
        {
            states.Add(new IntState(name, value));
            return this;
        }

        public BlockState PutBoolean(string name, bool value)
        {
            states.Add(new BoolState(name, value));
            return this;
        }

        public BlockState PutString(string name, string value)
        {
            states.Add(new StringState(name, value));
            return this;
        }


        public State this[string key]
        {
            get
            {
                foreach (var s in states)
                {
                    if (s.Name.EqualsIgnoreCase(key))
                    {
                        return s;
                    }
                }
                return null;
            }
            set
            {
                value.Name = key;
                states.Add(value);
            }
        }

        public static BlockState Parse(string s)
        {
            string[] entries = s.Split(',');
            var kv = from e in entries let idx = e.IndexOf('=') where idx != -1 select new { Key = e.Substring(0, idx), Value = e.Substring(idx + 1) };
            BlockState state = new BlockState();
            foreach (var entry in kv)
            {
                if (int.TryParse(entry.Value,out int iv))
                {
                    state.PutInt(entry.Key, iv);
                }
                else if (bool.TryParse(entry.Value, out bool bv))
                {
                    state.PutBoolean(entry.Key, bv);
                }
                else
                {
                    state.PutString(entry.Key, entry.Value);
                }
            }
            return state;
        }

        public bool IsEmpty()
        {
            return states.Count == 0;
        }

        public static implicit operator BlockState(string s)
        {
            return Parse(s);
        }

        public static implicit operator Block(BlockState state)
        {
            if (state.block != null)
            {
                state.block.state = state;
            }
            return state.block;
        }

        public static State Of(string name, int value)
        {
            return new IntState(name, value);
        }

        public static State Of(string name, bool value)
        {
            return new BoolState(name, value);
        }

        public static State Of(string name, string value)
        {
            return new StringState(name, value);
        }

        public static BlockState operator +(BlockState state, State add)
        {
            state.states.Add(add);
            return state;
        }

        public static BlockState operator +(BlockState state, IEnumerable<State> ie)
        {
            state.states.AddRange(ie);
            return state;
        }

        public override string ToString()
        {
            return $"[{string.Join(",", from s in states select $"{s.Name}={s.value}")}]";
        }
    }

    public interface IBlockState
    {
        BlockState PutInt(string name, int value);

        BlockState PutBoolean(string name, bool value);

        BlockState PutString(string name, string value);
    }

    public abstract class State
    {
        public string Name { get; internal set; }
        public object value { get; }

        public State(string name, object value)
        {
            this.Name = name;
            this.value = value;
        }

        public State(object value)
        {
            this.value = value;
        }

        public static implicit operator State(int i)
        {
            return new IntState(i);
        }

        public static implicit operator State(bool b)
        {
            return new BoolState(b);
        }

        public static implicit operator State(string en)
        {
            return new StringState(en);
        }
    }

    public class IntState : State
    {
        public IntState(int value) : base(value)
        {
            
        }

        public IntState(string name, int value) : base(name,value)
        {

        }

        public static implicit operator IntState(int n)
        {
            return new IntState(n);
        }
    }

    public class BoolState : State
    {
        public BoolState(bool value) : base(value)
        {

        }

        public BoolState(string name, bool value) : base(name, value)
        {

        }

        public static implicit operator BoolState(bool b)
        {
            return new BoolState(b);
        }
    }

    public class StringState : State
    {
        public StringState(string value) : base(value)
        {

        }

        public StringState(string name, string value) : base(name, value)
        {

        }

        public static implicit operator StringState(string e)
        {
            return new StringState(e);
        }

    }

}
