using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bittrex
{
    class Ordre
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Result result { get; set; }


        public class Result
        {
            public Buy[] buy { get; set; }
            public Sell[] sell { get; set; }
        }

        public class Buy
        {
            public float Quantity { get; set; }
            public float Rate { get; set; }
        }

        public class Sell
        {
            public float Quantity { get; set; }
            public float Rate { get; set; }
        }
    }
}
