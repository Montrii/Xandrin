using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Xandrin.GTA
{
    public class SAHandler
    {
        private bool detected = false;


        [DllImport("C:\\Users\\Drago\\source\\repos\\Xandrin\\Xandrin\\gtafunctions.dll")]
        public static extern IntPtr Create();

        [DllImport("C:\\Users\\Drago\\source\\repos\\Xandrin\\Xandrin\\gtafunctions.dll")]
        public static extern bool findGTA(IntPtr a);

        [DllImport("C:\\Users\\Drago\\source\\repos\\Xandrin\\Xandrin\\gtafunctions.dll")]
        public static extern int getGTASAVersion(IntPtr a);


        public SAHandler()
        {

        }

        public bool findGTA()
        {
            return findGTA(Create());
        }

        public int getGTAVersion()
        {
            return getGTASAVersion(Create());
        }

        public bool Detected { get => detected; set => detected = value; }
    }
}
