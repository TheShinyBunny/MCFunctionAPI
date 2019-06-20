using MCFunctionAPI.Entity;
using System;
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

        public ScoreEventHandler(string objective, string criteria, string intRange)
        {
            Objective = objective;
            Criteria = criteria;
            TargetValue = intRange;
        }

        public ScoreEventHandler(string objective, string criteria, int exact)
        {
            Objective = objective;
            Criteria = criteria;
            TargetValue = exact;
        }

        public ScoreEventHandler(string objective, string criteria)
        {
            Objective = objective;
            Criteria = criteria;
            TargetValue = "1..";
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
}
