using MCFunctionAPI.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class Block : IBlockState, ITaggable
    {

        private ResourceLocation id;
        public BlockState state;
        public NBT nbt;

        public ResourceLocation Id => id;

        public Block(ResourceLocation id) : this(id, new BlockState(), new NBT())
        {}

        public Block(ResourceLocation id, BlockState state) : this(id,state, new NBT())
        {}

        public Block(ResourceLocation id, NBT nbt) : this(id, new BlockState(), nbt)
        {}

        public Block(ResourceLocation id, BlockState state, NBT nbt)
        {
            this.id = id;
            this.state = state;
            this.nbt = nbt;
        }

        public BlockState WithState()
        {
            return state ?? new BlockState(this);
        }

        public BlockState PutInt(string name, int value)
        {
            return WithState().PutInt(name, value);
        }

        public BlockState PutBoolean(string name, bool value)
        {
            return WithState().PutBoolean(name, value);
        }

        public BlockState PutString(string name, string value)
        {
            return WithState().PutString(name, value);
        }

        public static implicit operator Block(string s)
        {
            return Parse(s);
        }

        public static Block Parse(string s)
        {
            int sqBrace = s.IndexOf('[');
            BlockState state = new BlockState();
            if (sqBrace != -1)
            {
                state = BlockState.Parse(s.Substring(sqBrace + 1, s.IndexOf("]") - sqBrace - 1));
            }
            ResourceLocation id = s.Substring(0, sqBrace == -1 ? s.Length : sqBrace);
            int curly = s.IndexOf('{');
            NBT nbt = new NBT();
            if (curly != -1)
            {
                nbt = s.Substring(curly);
            }
            return new Block(id, state, nbt);
        }

        public override string ToString()
        {
            return $"{id}{(state.IsEmpty() ? "" : state.ToString())}{(nbt.IsEmpty() ? "" : nbt)}";
        }
    }
}
