using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Plivo;
using Nancy;

namespace plivo_dotnet_app
{
    public sealed class Program : NancyModule
    {
        public Program()
        {
            Get("/speak/", x =>
            {
                string signature = Request.Headers["X-Plivo-Signature-V3"].ToString();
                string nonce = Request.Headers["X-Plivo-Signature-V3_Nonce"].ToString();
                string auth_token = "your_auth_token";
                string method = Request.Method;
                string url = Request.Url;
                bool valid;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                valid = Plivo.Utilities.XPlivoSignatureV3.VerifySignature(url, nonce, signature, auth_token, method);
                Debug.WriteLine("Valid : " + valid);

                Plivo.XML.Response resp = new Plivo.XML.Response();
                resp.AddSpeak("Hello, Welcome to Plivo", parameters);
                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response) output;
                res.ContentType = "text/xml";
                return res;
            });

            Post<Response>("/speak/", x =>
            {
                string signature = Request.Headers["X-Plivo-Signature-V3"].ToString();
                string nonce = Request.Headers["X-Plivo-Signature-V3_Nonce"].ToString();
                string auth_token = "your_auth_token";
                string method = Request.Method;
                string url = Request.Url;
                bool valid;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters = Request.Form;
                valid = Plivo.Utilities.XPlivoSignatureV3.VerifySignature(url, nonce, signature, auth_token, method, parameters);
                Debug.WriteLine("Valid : " + valid);

                Plivo.XML.Response resp = new Plivo.XML.Response();
                resp.AddSpeak("Hello, Welcome to Plivo", parameters);
                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response) output;
                res.ContentType = "text/xml";
                return res;
            });
        }
        
        static void Main(string[] args)
        {
            var p = new Program();
        }
    }
}