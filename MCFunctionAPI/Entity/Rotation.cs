using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class Rotation
    {

        public float Yaw { get; set; }
        private bool relativeYaw;
        public float Pitch { get; set; }
        private bool relativePitch;

        public Rotation(float yaw, bool relativeYaw, float pitch, bool relativePitch)
        {
            this.Yaw = yaw;
            this.relativeYaw = relativeYaw;
            this.Pitch = pitch;
            this.relativePitch = relativePitch;
        }

        public static Rotation Absolute(float yaw, float pitch)
        {
            return new Rotation(yaw, false, pitch, false);
        }

        public static Rotation Relative(float yaw, float pitch)
        {
            return new Rotation(yaw, true, pitch, true);
        }

        public static implicit operator Rotation(string s)
        {
            string y = s.Split(' ')[0];
            string p = s.Split(' ')[1];
            bool b1 = false;
            bool b2 = false;
            if (y[0] == '~')
            {
                b1 = true;
                y = y.Substring(1);
            }
            if (p[0] == '~')
            {
                b2 = true;
                p = p.Substring(1);
            }
            return new Rotation(float.Parse(y), b1, float.Parse(p), b2);
        }
        
        public override string ToString()
        {
            string y = Yaw + "";
            if (Yaw == 0.0)
            {
                y = "";
            }
            string p = Pitch + "";
            if (Pitch == 0.0)
            {
                p = "";
            }
            return (relativeYaw ? "~" : "") + y + " " + (relativePitch ? "~" : "") + p;
        }

    }
}
