using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Advancements
{
    public class AdvancementList
    {

        private Entities owner;
        public GrantRevoker Until { get; private set; }
        public GrantRevoker From { get; private set; }
        public GrantRevoker Through { get; private set; }

        public AdvancementList(Entities owner)
        {
            this.owner = owner;
            this.Until = new GrantRevoker(owner, "until");
            this.From = new GrantRevoker(owner, "from");
            this.Through = new GrantRevoker(owner, "through");
        }

        public OnlyListItem this[ResourceLocation id]
        {
            get
            {
                return new OnlyListItem(id,owner);
            }
        }

        public void RevokeAll()
        {
            FunctionWriter.Write($"advancement revoke {owner} everything");
        }

        public void GrantAll()
        {
            FunctionWriter.Write($"advancement grant {owner} everything");
        }
    }

    public class GrantRevoker
    {
        
        private Entities owner;
        private string method;

        public GrantRevoker(Entities owner, string method)
        {
            this.owner = owner;
            this.method = method;
        }

        public ListItem this[ResourceLocation id]
        {
            get
            {
                return new ListItem(id, owner, method);
            }
        }

    }

    public class ListItem
    {
        protected ResourceLocation id;
        protected Entities owner;
        private string method;

        public ListItem(ResourceLocation id, Entities owner, string method)
        {
            this.id = id;
            this.owner = owner;
            this.method = method;
        }

        public void Grant()
        {
            FunctionWriter.Write($"advancement grant {owner} {method} {id}");
        }

        public void Revoke()
        {
            FunctionWriter.Write($"advancement revoke {owner} {method} {id}");
        }
    }

    public class OnlyListItem : ListItem
    {
        public OnlyListItem(ResourceLocation id, Entities owner) : base(id, owner, "only")
        {
        }

        public void Grant(string criterion)
        {
            FunctionWriter.Write($"advancement grant {owner} only {id} {criterion}");
        }

        public void Revoke(string criterion)
        {
            FunctionWriter.Write($"advancement revoke {owner} only {id} {criterion}");
        }
    }
}
