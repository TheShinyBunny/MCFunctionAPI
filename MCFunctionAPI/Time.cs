using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class Time
    {
        private int time;

        public Time(int time)
        {
            this.time = time;
        }

        public Time()
        {
        }

        public static implicit operator Time(int time)
        {
            FunctionWriter.Write("time set " + time);
            return new Time(time);
        }

        public static Time operator +(Time t, int ticks)
        {
            FunctionWriter.Write("time add " + ticks);
            return t;
        }

        public ResultCommand GetDay()
        {
            return new ResultCommand("time query day");
        }

        public ResultCommand GetDayTime()
        {
            return new ResultCommand("time query daytime");
        }

        public ResultCommand GetGameTime()
        {
            return new ResultCommand("time query gametime");
        }

        public override string ToString()
        {
            return time.ToString();
        }

    }
}
