using MCFunctionAPI.Blocks;
using MCFunctionAPI.Entity;
using MCFunctionAPI.Scoreboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class MyFunctions : FunctionContainer
    {

        private ObjectiveBoolean MyBool;

        public void Setup()
        {
            ObjectiveBoolean.Setup();
            MyBool = "booleantest";
            MyBool.Create();
        }

        public void TurnOn()
        {
            MyBool.Value = true;   
        }

        public void TurnOff()
        {
            MyBool.Value = false;
        }

        public void Check()
        {
            execute.If(MyBool).Run.Say("Its working!");
            execute.Unless(MyBool).Run.Say("It was false!");
        }

        public void Remove()
        {
            MyBool.Remove();
        }

    }
}
