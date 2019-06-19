# MCFunctionAPI
The Minecraft Function Application Programming Interface (or MCFunctionAPI for short)
is a system for developing custom Datapacks for Minecraft very easily using advanced C# tools.

Here are some examples, for a full documentation go to the Wiki tab.
```csharp
class Program : Datapack
{

  static void Main(string[] args)
  {
    Program dp = new Program();
    Namespace main = dp.CreateNamespace("main");
    main.AddFunctions(typeof(MyFunctions));
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

[Root]
class MyFunctions : FunctionContainer
{
  
  public static void SayHello()
  {
    Say("Hello World!");
  }
  
}
```
In this example, we created a datapack named `test` that contains a single function with id `main:say_hello`. That function contains a `/say Hello World!` command.
