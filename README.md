# MCFunctionAPI
The Minecraft Function Application Programming Interface (or MCFunctionAPI for short)
is a system for developing custom Datapacks for Minecraft very easily using advanced C# tools.

## Why C#?
Attempts have been made to create this API in Java and JavaScript, but C# has many unique features no other language has them all.

C# is a very fast and simple language, and very similar to Java - 
so if you know Java from Minecraft modding or something, learning C# is gonna be very easy!

Along the process of developing this API, I learnt many awesome features C# has that Java doesn't, 
and it made the API a lot more simple and convenient to use than in Java.

# How do I use this thing?!?!
To use this API, download the project's .dll from the `releases` tab. 
In VisualStudio, Add taht dll file as a reference in a new C# console application project.

## Create the Datapack class
Create a class that extends the MCFunctionAPI.Datapack class. You can extend the main Program class too.

Implement the abstract methods:

* `GetName()` // The name of your datapack. Used as the name of the main directory (the one that contains the pack.mcmeta and data folder)
* `GetDescription()` // A description for your datapack. Will be used inside pack.mcmeta.

To create the datapack, initialize the class that extends `Datapack`.

Example:

```csharp
class Program : Datapack
{

  static void Main(string[] args)
  {
    Program dp = new Program();
    // add functions, tags, loot tables, etc. here
  }
  
  public override string GetDescription()
  {
    return "Hello World";
  }

  public override string GetName()
  {
    return "test";
  }
}
```

## Add functions
To add functions, first create a new namespace for your datapack:
```csharp
Program dp = new Program();

// create the namespace:
Namespace main = dp.CreateNamespace("main");
```

Then, create a new class that extends `FunctionContainer`.
A `FunctionContainer` holds multiple functions represented as `public static void FunctionName()`, and creates them all in a folder with the name of the class, in lower case with underscores.

To make a FunctionContainer add functions in the root function directory, add a [Root] attribute to the class.

```csharp
[Root]
class MyFunctions : FunctionContainer
{
  
  public static void SayHello()
  {
    Say("Hello World!");
  }
  
}
```
In the example, we added a single function that will be named `say_hello`, that just executes a `/say Hello World!` command.

# The command API
Most commands are statically available through the `FunctionContainer` class. Other commands, are implicitly created from various operations (will be explained later). 


## EntitySelector
Commands that require an entity, will be available statically, and will target @s (the entity the function is executed as). To target another selector that is not @s, you can use the amazing, very complicated `EntitySelector` API!

An entity selector can be built in 4 ways:
```csharp
EntitySelector selector = EntitySelector.AllEntities.Is(EntityType.Creeper).InDistance("5..10").LimitTo(5).Score("myObjective", 5);

EntitySelector selector2 = new EntitySelector(Target.AllEntities);
selector2.Is(EntityType.Creeper);
selector2.Distance = "5..10";
selector2.Limit = 5;
selectors2.Scores.Where("myObjective", 5);

EntitySelector selector3 = new EntitySelector(Target.AllEntities)
{
  Type = "creeper"
  Distance = "5..10",
  Limit = 5,
  Scores = "myObjective=5"
};

EntitySelector selector4 = "@e[type=creeper,distance=5..10,limit=5,scores={myObjective=5}]";
```

Choose whatever is the easiest one and most readable for you! all of the above examples <s>should</s> will produce the same selector!
