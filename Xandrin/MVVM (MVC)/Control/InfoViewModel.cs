using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xandrin.MVVM__MVC_.Control
{
    internal class InfoViewModel
    {
        private MainViewModel _m;
        public InfoViewModel(MainViewModel m)
        {
            _m = m;
        }

        internal MainViewModel M { get => _m; set => _m = value; }
    }
}
