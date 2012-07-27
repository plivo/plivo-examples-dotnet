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
            resp.AddSpeak("Hi, Plivo calling", new Dictionary<string, string>() {
                { "language", "en-US" },
                { "voice", "WOMAN" }
            });
            resp.AddPlay("http://examples.com/play/Trumpet.mp3", new Dictionary<string, string>() {
                { "loop", "2" }
            });
            resp.AddWait(new Dictionary<string, string>() {
                { "length", "3" }
            });
            Console.WriteLine(resp.ToString());
        }
    }
}