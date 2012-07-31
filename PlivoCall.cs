using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace PlivoCall
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI(auth_id, auth_token);

            // Place a call
            IRestResponse<Call> resp = plivo.make_call(new dict {
                { "to", "11111111111" },
                { "from", "22222222222" },
                { "answer_url", "http://some.domain/answer/" },
                { "answer_method", "GET" }
            });
            if (resp.Data != null)
            {
                PropertyInfo[] proplist = resp.Data.GetType().GetProperties();
                foreach (PropertyInfo property in proplist)
                    Console.WriteLine("{0}: {1}", property.Name, property.GetValue(resp.Data, null));
            }
            else
                Console.WriteLine(resp.ErrorMessage);

            // Get all CDRs
            string cdr_id = "";
            IRestResponse<CDRList> resp2 = plivo.get_cdrs(new dict {
                { "", "" }
            });
            if (resp2.Data != null)
            {
                if (resp2.Data.meta.total_count > 0)
                {
                    cdr_id = resp2.Data.objects[0].call_uuid;
                    foreach (CDR cdr in resp2.Data.objects)
                        foreach (PropertyInfo property in cdr.GetType().GetProperties())
                            Console.WriteLine("{0}: {1}", property.Name, property.GetValue(cdr, null));
                }
            }
            else
                Console.WriteLine(resp2.ErrorMessage);
            Console.WriteLine("==================================================");

            // Get details of a particular cdr

            IRestResponse<CDR> resp3 = plivo.get_cdr(new dict {
                { "record_id", cdr_id },
            });
            if (resp3.Data != null)
            {
                foreach (PropertyInfo property in resp3.Data.GetType().GetProperties())
                    Console.WriteLine("{0}: {1}", property.Name, property.GetValue(resp3.Data, null));
            }
            else
                Console.WriteLine(resp3.ErrorMessage);
            Console.WriteLine("==================================================");

            // Get live calls 
            string call_id = "";
            IRestResponse<LiveCallList> resp4 = plivo.get_live_calls();
            if (resp4.Data != null)
            {
                if (resp4.Data.calls.Count > 0)
                {
                    call_id = resp4.Data.calls[0];
                    foreach (string call in resp4.Data.calls)
                        Console.WriteLine(call);
                }
            }
            else
                Console.WriteLine(resp4.ErrorMessage);
            Console.WriteLine("==================================================");

            // Get details of a particular live call
            IRestResponse<LiveCall> resp5 = plivo.get_live_call(new dict {
                { "call_uuid", call_id }
            });
            if (resp5.Data != null)
            {
                foreach (PropertyInfo property in resp5.Data.GetType().GetProperties())
                    Console.WriteLine("{0}: {1}", property.Name, property.GetValue(resp5.Data, null));
            }
            else
                Console.WriteLine(resp5.ErrorMessage);
        }
    }
}