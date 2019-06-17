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
To use this API, download the project's .dll from the /releases/ directory. 
In VisualStudio, Add taht dll file as a reference in a new C# console application project.

## Create the Datapack class
Create a class that extends the MCFunctionAPI.Datapack class. You can extend the main Program class too.

Implement the abstract methods:

* `GetName()` // The name of your datapack. Used as the name of the main directory (the one that contains the pack.mcmeta and data folder)
* `GetDescription()` // A description for your datapack. Will be used inside pack.mcmeta.

To create the datapack, initialize the class that extends Datapack.

Example:

```
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
