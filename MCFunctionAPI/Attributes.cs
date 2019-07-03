using MCFunctionAPI.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MCFunctionAPI.CommandWrapper;

namespace MCFunctionAPI
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Tick : Attribute
    {

    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class Load : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class)]
    public class Root : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false)]
    public class Desc : Attribute
    {
        public string Value { get; }
        public string Caller { get; }

        public Desc(string d)
        {
            Value = d;
        }

        /// <summary>
        /// Creates a description that includes a caller function
        /// </summary>
        /// <param name="d"></param>
        /// <param name="caller">The caller function's path</param>
        public Desc(string d, string caller) : this(d)
        {
            Caller = caller;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ScoreEventHandler : Attribute
    {
        public string Objective { get; set; }
        public string Criteria { get; set; }
        public IntRange TargetValue { get; set; }

        public ScoreEventHandler(string objective, string criteria, string intRange) : this(objective, criteria)
        {
            TargetValue = intRange;
        }

        public ScoreEventHandler(string objective, string criteria, int exact) : this(objective, criteria)
        {
            TargetValue = exact;
        }

        public ScoreEventHandler(string objective, string criteria)
        {
            Objective = objective;
            Criteria = criteria;
            TargetValue = "1..";
        }

        public ScoreEventHandler(string objective, string criteria, IntRange targetValue) : this(objective, criteria)
        {
            TargetValue = targetValue;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class NestedFolder : Attribute
    {
        public Type SuperType { get; }

        public NestedFolder(Type super)
        {
            SuperType = super;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class Criteria : Attribute
    {
        public string Name { get; }

        public Criteria(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// An attribute to use on a function to make it /give the returned item to the executing player.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GiveItem : Attribute
    {
        
        /// <summary>
        /// Creates a new GiveItem attribute with an optional container.
        /// </summary>
        /// <param name="container">The block ID to use as a container item to fill with the returned items. Will result as an item like ctrl+clicking a chest in creative.</param>
        public GiveItem(string container = null)
        {
            Container = container;
        }

        public string Container { get; }
    }

    /// <summary>
    /// Expands a function to a folder, with every value in the provided array.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class Expand : Attribute
    {

        public string[] Items { get; }
        public BuiltinEnums BuiltinItems { get; }

        public string[] All
        {
            get
            {
                return Items ?? EnumBase.GetRegistry(BuiltinItems).Keys.Cast<string>().ToArray();
            }
        }

        public Expand(params string[] items)
        {
            Items = items;
        }

        public Expand(BuiltinEnums builtin)
        {
            BuiltinItems = builtin;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ScoreTrigger : Attribute
    {
        public string Objective { get; }

        public IntRange Range { get; }

        public ScoreTrigger(string objective = null, string range = "1..")
        {
            Objective = objective;
            Range = range;
        }
    }

}
