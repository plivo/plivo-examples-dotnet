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
            resp.AddRecord(new Dictionary<string, string>() {
                { "transcriptUrl", "http=>//some.server/action/" },
                { "transcriptMethod", "GET" },
                { "transcriptType", "auto" },
            });
            //Console.WriteLine(resp.ToString());
        }
    }
}