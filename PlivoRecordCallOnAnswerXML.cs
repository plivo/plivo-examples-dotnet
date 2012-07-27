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
            resp.AddRecord(new Dictionary<string,string>() {
                { "action", "http://some.domain/recordfile" },
                { "startOnDialAnswer", "true" }
            });

            Dial dial = new Dial(new Dictionary<string, string>() {
                { "callerId", "12222222222" },
            });
            dial.AddNumber("12121212121", new Dictionary<string,string>());
            resp.Add(dial);
            Console.WriteLine(resp.ToString());
        }
    }
}    