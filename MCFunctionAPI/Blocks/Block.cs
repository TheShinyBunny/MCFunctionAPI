using MCFunctionAPI.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class Block : IBlockState<Block>, ITaggable
    {
        public BlockState state;
        public NBT nbt;

        public ResourceLocation Id { get; }

        public Block(ResourceLocation id) : this(id, new BlockState(), new NBT())
        {}

        public Block(ResourceLocation id, BlockState state) : this(id,state, new NBT())
        {}

        public Block(ResourceLocation id, NBT nbt) : this(id, new BlockState(), nbt)
        {}

        public Block(ResourceLocation id, BlockState state, NBT nbt)
        {
            this.Id = id;
            this.state = state;
            this.nbt = nbt;
        }

        public BlockState WithState()
        {
            return state ?? new BlockState(this);
        }

        public Block PutInt(string name, int value)
        {
            WithState().PutInt(name, value);
            return this;
        }

        public Block PutBoolean(string name, bool value)
        {
            WithState().PutBoolean(name, value);
            return this;
        }

        public Block PutString(string name, string value)
        {
            WithState().PutString(name, value);
            return this;
        }

        public static implicit operator Block(string s)
        {
            return Parse(s);
        }

        public static Block Parse(string s)
        {
            int nbtStart = s.IndexOf('{');
            int nbtEnd = s.LastIndexOf('}');
            NBT nbt = new NBT();
            if (nbtStart != -1)
            {
                nbt = s.SubstringIndexed(nbtStart, nbtEnd+1);
                s = s.SubstringIndexed(0, nbtStart) + (nbtEnd == s.Length ? "" : s.Substring(nbtEnd + 1));
            }
            int sqBrace = s.IndexOf('[');
            BlockState state = new BlockState();
            if (sqBrace != -1)
            {
                state = BlockState.Parse(s.Substring(sqBrace + 1));
            }
            ResourceLocation id = s.Substring(0, sqBrace == -1 ? nbtEnd == -1 ? s.Length : nbtStart : Math.Min(sqBrace,nbtStart));
            
            return new Block(id, state, nbt);
        }

        public override string ToString()
        {
            return $"{Id}{(state.IsEmpty() ? "" : state.ToString())}{(nbt.IsEmpty() ? "" : nbt)}";
        }
    }
}
