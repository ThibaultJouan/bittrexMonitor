using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bittrex
{
    class Echange
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Result[] result { get; set; }


        public class Result
        {
            public int Id { get; set; }
            public DateTime TimeStamp { get; set; }
            public float Quantity { get; set; }
            public float Price { get; set; }
            public float Total { get; set; }
            public string FillType { get; set; }
            //"BUY" or "SELL"
            public string OrderType { get; set; }
        }
    }
}
