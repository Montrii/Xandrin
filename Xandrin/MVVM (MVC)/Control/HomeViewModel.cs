using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xandrin.MVVM__MVC_.Control
{
    class HomeViewModel
    {


        private MainViewModel _m;
        public HomeViewModel(MainViewModel m)
        {
            _m = m;
        }

        public MainViewModel M { get => _m; set => _m = value; }
    }
}
