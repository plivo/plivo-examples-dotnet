using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

// Set the caller ID using Dial XML

namespace hangup
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["dynamic_caller_id"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();
                Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary<string, string>() { });
                dial.AddNumber("1111111111", new Dictionary<string, string>() { });
                resp.Add(dial);
                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };
        }
    }
}

/*
Sample Output
<Response>
    <Dial>
        <Number>919663489033</Number>
    </Dial>
</Response>
*/

// Set the caller ID using Call API

using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace make_calls
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");
             
            IRestResponse<Call> resp = plivo.make_call(new Dictionary<string, string>() 
            {
                { "from", "1111111111" }, // The phone number to which the call has to be placed
                { "to", "2222222222" }, // The phone number to be used as the caller Id
                { "answer_url", "http://dotnettest.apphb.com/speak" }, // The URL invoked by Plivo when the outbound call is answered
                { "answer_method","GET"} // The method used to invoke the answer_url
            });

            //Prints the response
            Console.Write(resp.Content);

            Console.ReadLine();
        }
    }
}

/*
Sample Output
{
  "api_id": "eaf9a5ec-b295-11e4-b932-22000ac50fac",
  "message": "call fired",
  "request_uuid": "1b034cad-65e7-4a8c-8d93-4040d9d8809a"
}
*/