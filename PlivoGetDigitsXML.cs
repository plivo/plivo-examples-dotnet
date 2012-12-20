using System;
using System.Collections.Generic;
using Plivo.XML;

namespace PlivoResponse
{
    class Program
    {
        static void Main(string[] args)
        {
            Response resp = new Response();
            resp.AddGetDigits(new Dictionary<string, string>() {
                { "action", "http://some.server/action/" },
                { "method", "GET" },
                { "digitTimeout", "5" },
				{ "finishOnKey", "#" },
            });
            //Console.WriteLine(resp.ToString());
        }
    }
}