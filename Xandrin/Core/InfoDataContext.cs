using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xandrin.Core
{
    public class InfoDataContext
    {


        private List<String> onMissionInfo = new List<String>();


        public InfoDataContext()
        {

        }

        public List<string> OnMissionInfo { get => onMissionInfo; set => onMissionInfo = value; }
    }
}
