using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;

class Program
{
    static void Main(string[] args)
    {
        CPubSubBot bot = new CPubSubBot();

        bot.Connect();

        Console.ReadLine();

        bot.Disconnect();
    }
}

