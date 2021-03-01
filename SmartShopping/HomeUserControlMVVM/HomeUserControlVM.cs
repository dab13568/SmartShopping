using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.HomeUserControlMVVM
{
    public class HomeUserControlVM
    {
        HomeUserControlV View;
        public string currentTime { get; set; }
        public string currentHelloMessage { get; set; }
        public string InfUrl { get; set; }

        public HomeUserControlVM(HomeUserControlV view)
        {
            View = view;
            currentTime = new HomeUserControlM().getCurrentTime().Item1;
            currentHelloMessage = new HomeUserControlM().getCurrentTime().Item2;
            InfUrl= new HomeUserControlM().getCurrentTime().Item3;
        }
    }
}
