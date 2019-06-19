using MCFunctionAPI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class ModelEntityType<T> : EntityType where T : EntityModelBase<T>, new()
    {
        public ModelEntityType(string id) : base(id)
        {
        }

        public T CreateModel(string tag)
        {
            return new T().With(tag,this);
        }
    }
}
