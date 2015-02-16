using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace phone_ivr
{
    public class Program : NancyModule
    {
        public Program()
        {
            //  This file will be played when a caller presses 2.
            String PLIVO_SONG = "https://s3.amazonaws.com/plivocloud/music.mp3";
            // This is the message that Plivo reads when the caller dials in
            String IVR_MESSAGE1 = "Welcome to the Plivo IVR Demo App. Press 1 to listen to a pre recorded text in different languages. Press 2 to listen to a song.";

            String IVR_MESSAGE2 = "Press 1 for English. Press 2 for French. Press 3 for Russian";
            // This is the message that Plivo reads when the caller does nothing at all
            String NO_INPUT_MESSAGE = "Sorry, I didn't catch that. Please hangup and try again later.";
            // This is the message that Plivo reads when the caller inputs a wrong number.
            String WRONG_INPUT_MESSAGE = "Sorry, it's a wrong input.";

            Get["/response/ivr"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();
                String getdigits_action_url = "http://dotnettest.apphb.com/response/ivr";

                // Add GetDigits XML Tag
                GetDigits gd = new GetDigits("", new Dictionary<string, string>()
                {
                    {"action", getdigits_action_url},
                    {"method", "POST"},
                    {"timeout","7"},
                    {"numDigits","1"},
                    {"retries","1"}
                });

                // Add Speak XML Tag
                gd.AddSpeak(IVR_MESSAGE1, new Dictionary<string, string>() { });
                resp.Add(gd);
                // Add Speak XML Tag
                resp.AddSpeak(NO_INPUT_MESSAGE, new Dictionary<string, string>() { });

                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };

            Post["/response/ivr"] = x =>
            {
                String digit = Request.Form["Digits"];
                Debug.WriteLine("Digit pressed : {0}", digit);

                Plivo.XML.Response resp = new Plivo.XML.Response();

                if (digit == "1")
                {
                    String getdigits_action_url = "http://dotnettest.apphb.com/response/tree";

                    // Add GetDigits XML Tag
                    GetDigits gd = new GetDigits("", new Dictionary<string, string>()
                {
                    {"action", getdigits_action_url}, // The URL to which the digits are sent. 
                    {"method", "GET"}, // Submit to action URL using GET or POST.
                    {"timeout","7"}, // Time in seconds to wait to receive the first digit.
                    {"numDigits","1"}, // Maximum number of digits to be processed in the current operation. 
                    {"retries","1"} // Indicates the number of retries the user is allowed to input the digits
                });

                    // Add Speak XML Tag
                    gd.AddSpeak(IVR_MESSAGE2, new Dictionary<string, string>() { });
                    resp.Add(gd);
                    // Add Speak XML Tag
                    resp.AddSpeak(NO_INPUT_MESSAGE, new Dictionary<string, string>() { });
                }
                else if (digit == "2")
                {
                    // Add Play XML Tag
                    resp.AddPlay(PLIVO_SONG, new Dictionary<string, string>() { });
                }
                else
                {
                    // Add Speak XML Tag
                    resp.AddSpeak(WRONG_INPUT_MESSAGE, new Dictionary<string, string>() { });
                }

                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };

            Get["/response/tree"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();
                String digit = Request.Query["Digits"];

                // Add Speak XMLTag
                if (digit == "1")
                {
                    resp.AddSpeak("This message is being read out in English", new Dictionary<string, string>()
                    {
                        {"language","en-GB"}
                    });
                }
                else if (digit == "2")
                {
                    resp.AddSpeak("Ce message est lu en français", new Dictionary<string, string>()
                    {
                        {"language","fr-FR"}
                    });
                }
                else if (digit == "3")
                {
                    resp.AddSpeak("Это сообщение было прочитано в России", new Dictionary<string, string>()
                    {
                        {"language","ru-RU"}
                    });
                }
                else
                {
                    resp.AddSpeak(WRONG_INPUT_MESSAGE, new Dictionary<string, string>() { });
                }

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
Sample output
<Response>
    <GetDigits action="http://dotnettest.apphb.com/response/ivr" method="POST" timeout="7" numDigits="1" retries="1">
        <Speak>
            Welcome to the Plivo IVR Demo App. Press 1 to listen to a pre recorded text in different languages. Press 2 to listen to a song.
        </Speak>
    </GetDigits>
    <Speak>
        Sorry, I didn't catch that. Please hangup and try again later.
    </Speak>
</Response>

If 1 is pressed, another menu is read out. Following is the generated Speak XML.
<Response>
    <GetDigits action="http://morning-ocean-4669.herokuapp.com/response/tree/" method="POST" numDigits="1" retries="1" timeout="7">
        <Speak>Press 1 for English. Press 2 for French. Press 3 for Russian</Speak>
    </GetDigits>
    <Speak>Sorry, I didn't catch that. Please hangup and try again later.</Speak>
</Response>

If 1 is pressed, the English text is read out. Following is the generated Speak XML.
<Response>
   <Speak language="en-GB">This message is being read out in English</Speak>
</Response>

If 2 is pressed, the French text is read out. Following is the generated Speak XML.
<Response>
    <Speak language="fr-FR">Ce message est lu en français</Speak>
</Response>

If 3 is pressed, the Russian text is read out. Following is the generated Speak XML.
<Response>
    <Speak language="ru-RU">Это сообщение было прочитано в России</Speak>
</Response>

If 2 is pressed, a music is played. Following is the generated Play XML.
<Response>
   <Play>https://s3.amazonaws.com/plivocloud/music.mp3</Play>
</Response>

*/