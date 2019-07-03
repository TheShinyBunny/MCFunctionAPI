using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCFunctionAPI.Blocks;
using MCFunctionAPI.Entity;
using MCFunctionAPI.Scoreboard;
using MCFunctionAPI.BossBar;
using static MCFunctionAPI.CommandWrapper;

namespace MCFunctionAPI
{
    /// <summary>
    /// The Execute class is able to build easily any minecraft /execute command.
    /// </summary>
    /// <remarks>
    /// To start creating an /execute command, you can initialize a new Execute() or use an Execute instance of a FunctionContainer, EntitySelector or CommandWrapper.
    /// </remarks>
    /// <example>
    /// <c>new Execute().As(EntitySelector.AllPlayers).Run.Say("Hello!");</c>
    /// </example>
    public class Execute
    {

        private string str = "";
        
        private readonly EntitySelector @as;

        public Execute()
        {

        }

        /// <summary>
        /// Gets the CommandWrapper to use as the entity running this /execute
        /// </summary>
        public Entities Run() {
            FunctionWriter.Execute.Push(this);
            UseOnce = true;
            return new Entities();
        }

        public void RunRaw(string command)
        {
            FunctionWriter.Execute.Push(this);
            UseOnce = true;
            FunctionWriter.Write(command);
        }

        public void RunFunction(Function func)
        {
            Run();
            CommandWrapper.RunFunction(func);
        }

        public void RunAbstractFunction(object instance,Function func)
        {
            Run();
            CommandWrapper.RunAbstractFunction(instance,func);
        }

        public void Run(Action<Entities> action)
        {
            action(Run());
        }

        public bool UseOnce { get; private set; }

        /// <summary>
        /// Initializes an execute builder, starting with '/execute as &lt;<paramref name="as"/>&gt;'.
        /// </summary>
        /// <param name="as">The EntitySelector for /execute as &lt;selector&gt;</param>
        public Execute(EntitySelector @as)
        {
            this.@as = @as;
        }

        /// <summary>
        /// Runs all actions done inside the <paramref name="execution"/> delegate using this Execute builder.
        /// </summary>
        /// <param name="execution">The delegate to take all commands from</param>
        public void RunAll(Action<Entities> execution)
        {
            UseOnce = false;
            FunctionWriter.Execute.Push(this);
            execution(new Entities());
            FunctionWriter.Execute.Pop();
        }
        
        /// <summary>
        /// Makes minecraft run the command like the specified entity(s) executed it.
        /// </summary>
        /// <param name="entity">The entity(s) to use to execute the command</param>
        /// <returns></returns>
        public Execute As(Entities entity)
        {
            return this + $"as {entity}";
        }

        /// <summary>
        /// A combination of <see cref="As(Entities)"/> and <see cref="At()"/>
        /// </summary>
        /// <param name="entity">The entity to be the executor at its location</param>
        /// <returns></returns>
        public Execute AsAt(Entities entity)
        {
            return this + $"as {entity} at @s";
        }

        private Execute Append(string s)
        {
            str += s + " ";
            return this;
        }

        /// <summary>
        /// Makes the command being executed at the location, rotation and dimension of the specified entity(s). Used for commands like /setblocks
        /// </summary>
        /// <param name="entity">The entity(s) to use their location</param>
        /// <returns></returns>
        public Execute At(Entities entity)
        {
            return this + $"at {entity}";
        }

        /// <summary>
        /// Runs the command at the location, rotation and dimension of the currently executing entity(s)
        /// </summary>
        /// <returns></returns>
        public Execute At()
        {
            return this + "at @s";
        }

        /// <summary>
        /// Runs the command at the specified xyz.
        /// </summary>
        /// <param name="pos">The location to execute the command in. Can be relative</param>
        /// <returns></returns>
        public Execute Positioned(Position pos)
        {
            return this + $"positioned {pos}";
        }

        /// <summary>
        /// Runs the command only at the xyz of the specified entity(s)
        /// </summary>
        /// <param name="entity">The entity(s) to use their location</param>
        /// <returns></returns>
        public Execute PositionedAs(Entities entity)
        {
            return this + $"positioned as {entity}";
        }

        /// <summary>
        /// Aligns the command location inside the block, using a combination of x, y and z characters.
        /// </summary>
        /// <param name="a">The alignment to execute the command by</param>
        /// <returns></returns>
        public Execute Align(Alignment a)
        {
            return this + $"align {a}";
        }
        
        /// <summary>
        /// Executes the command as the entity was facing the specified xyz. Used for commands that use the "^" coords.
        /// </summary>
        /// <param name="pos">The location to face as when executing the command</param>
        /// <returns></returns>
        public Execute Facing(Position pos)
        {
            return this + $"facing {pos}";
        }

        /// <summary>
        /// Executes the command as the entity was facing the specified entity at the specified anchor.
        /// </summary>
        /// <param name="entity">The entity to face</param>
        /// <param name="anchor">The entity's anchor (feet/eyes)</param>
        /// <returns></returns>
        public Execute Facing(Entities entity, Anchor anchor)
        {
            return this + $"facing entity {entity} {anchor}";
        }
        
        /// <summary>
        /// Executes the command as the entity's head rotation was <paramref name="rot"/>.
        /// </summary>
        /// <param name="rot"></param>
        /// <returns></returns>
        public Execute Rotated(Rotation rot)
        {
            return this + $"rotated {rot}";
        }

        /// <summary>
        /// Executes the command as the entity's head rotation was like <paramref name="entity"/>'s head rotation.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Execute RotatedAs(Entities entity)
        {
            return this + $"rotated as {entity}";
        }

        /// <summary>
        /// Anchores the execution position to either the feet or the eye level of the executing entity
        /// </summary>
        /// <param name="anchor">The <see cref="Anchor"/> to use</param>
        /// <returns></returns>
        public Execute Anchored(Anchor anchor)
        {
            return this + $"anchored {anchor}";
        }

        /// <summary>
        /// Executes the current command in the specified dimension, at the same XYZ location
        /// </summary>
        /// <param name="dim"></param>
        /// <returns></returns>
        public Execute In(Dimension dim)
        {
            return this + $"in {dim}";
        }

        public Execute If(Entities entity)
        {
            return this + $"if entity {entity}";
        }

        public Execute If(ObjectiveBoolean b)
        {
            return this + $"if score {ObjectiveBoolean.DEFAULT_PLAYER} {b} = true {ObjectiveBoolean.BOOL_VALUES}";
        }

        public Execute If(Score score)
        {
            return this + $"if score {score.GetTarget()} {score.GetObjective()} matches 1..";
        }

        public Execute If(Score score, IntRange matches)
        {
            return this + $"if score {score.GetTarget()} {score.GetObjective()} matches {matches}";
        }

        public Execute If(Position pos, Block block)
        {
            return this + $"if block {pos} {block}";
        }

        public Execute If(Position begin, Position end, Position dest, bool masked)
        {
            return this + $"if blocks {begin} {end} {dest} {(masked ? "masked" : "all")}";
        }

        public Execute If(string target, Objective targetObj, ScoreOperator @operator, string src, Objective srcObj)
        {
            return this + $"if score {target} {targetObj} {@operator} {src} {srcObj}";
        }

        public Execute If(string target, Objective targetObj, IntRange matches)
        {
            return this + $"if score {target} {targetObj} matches {matches}";
        }

        public Execute IfData(DataContainer container, string path)
        {
            return this + $"if data {container.ToDataCommand()} {path}";
        }

        public Execute Unless(Entities entity)
        {
            return this + $"unless entity {entity}";
        }

        public Execute Unless(Score score)
        {
            return this + $"unless score {score.GetTarget()} {score.GetObjective()} matches 1..";
        }

        public Execute Unless(Score score, IntRange matches)
        {
            return this + $"unless score {score.GetTarget()} {score.GetObjective()} matches {matches}";
        }

        public Execute Unless(Position pos, Block block)
        {
            return this + $"unless block {pos} {block}";
        }

        public Execute Unless(Position begin, Position end, Position dest, bool masked)
        {
            return this + $"unless blocks {begin} {end} {dest} {(masked ? "masked" : "all")}";
        }

        public Execute Unless(string target, Objective targetObj, ScoreOperator @operator, string src, Objective srcObj)
        {
            return this + $"unless score {target} {targetObj} {@operator} {src} {srcObj}";
        }

        public Execute Unless(string target, Objective targetObj, IntRange matches)
        {
            return this + $"unless score {target} {targetObj} matches {matches}";
        }

        public Execute Unless(ObjectiveBoolean b)
        {
            return this + $"unless score {ObjectiveBoolean.DEFAULT_PLAYER} {b} = true {ObjectiveBoolean.BOOL_VALUES}";
        }

        public Execute UnlessData(DataContainer container, string path)
        {
            return this + $"unless data {container.ToDataCommand()} {path}";
        }

        public Execute Store(Storage @in, string name, Objective objective)
        {
            return this + $"store {@in} score {name} {objective}";
        }

        public Execute Store(Storage @in, BossBar.BossBar boss, bool max)
        {
            return this + $"store {@in} bossbar {boss} {(max ? "max" : "value")}";
        }

        public Execute Store(Storage @in, DataContainer container, string path, DataType type, double scale)
        {
            return this + $"store {@in} {container.ToDataCommand()} {path} {type} {scale}";
        }

        public static Execute operator +(Execute e, string s)
        {
            return e.Append(s);
        }

        /// <summary>
        /// Returns a string representation of the execute command built in this Execute instance
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (@as != null)
            {
                return "execute as " + @as + " " + str;
            }
            return "execute " + str;
        }
    }

    public class DataType : EnumBase
    {
        public static DataType Integer = new DataType("int");
        public static DataType Short = new DataType("short");
        public static DataType Byte = new DataType("byte");
        public static DataType Long = new DataType("long");
        public static DataType Float = new DataType("float");
        public static DataType Double = new DataType("double");

        private static IDictionary<string, DataType> Registry;

        public static IEnumerable<DataType> All
        {
            get
            {
                return Registry.Values;
            }
        }

        public DataType(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, DataType>();
            Registry.Add(id, this);
        }

        public static implicit operator DataType(string s)
        {
            return Get(s, Registry);
        }

        public static implicit operator string(DataType type)
        {
            return type.Id;
        }
    }

    public class Storage
    {

        public static Storage Result = new Storage("result");
        public static Storage Success = new Storage("success");

        private string id;

        public Storage(string id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return id;
        }
    }
    
}
