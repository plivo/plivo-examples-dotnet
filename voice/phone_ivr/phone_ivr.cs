using System;
using Plivo.XML;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ivrphonetree.Controllers
{
    public class IvrController : Controller
    {
        //  This file will be played when a caller presses 2.
        String PlivoSong = "https://s3.amazonaws.com/plivocloud/music.mp3";
        // This is the message that Plivo reads when the caller dials in
        String IvrMessage1 = "Welcome to the Plivo IVR Demo App. Press 1 to listen to a pre recorded text in different languages. Press 2 to listen to a song.";
        String IvrMessage2 = "Press 1 for English. Press 2 for French. Press 3 for Russian";
        // This is the message that Plivo reads when the caller does nothing at all
        String NoinputMessage = "Sorry, I didn't catch that. Please hangup and try again later.";
        // This is the message that Plivo reads when the caller inputs a wrong number.
        String WronginputMessage = "Sorry, it's a wrong input.";

        // GET: /<controller>/
        public IActionResult Index()
        {
            var resp = new Response();
            Plivo.XML.GetInput get_input = new
                Plivo.XML.GetInput("",
                    new Dictionary<string, string>()
                    {
                        {"action", "https://www.foo.com/ivr/firstbranch/"},
                        {"method", "POST"},
                        {"digitEndTimeout", "5"},
                        {"inputType", "dtmf"},
                        {"redirect", "true"},
                    });
            resp.Add(get_input);
            get_input.AddSpeak(IvrMessage1,
                new Dictionary<string, string>() { });
            resp.AddSpeak(NoinputMessage,
                new Dictionary<string, string>() { });

            var output = resp.ToString();
            return this.Content(output, "text/xml");
        }
        // First branch of IVR phone tree
        public IActionResult FirstBranch()
        {
            String digit = Request.Query["Digits"];
            Debug.WriteLine("Digit pressed : {0}", digit);

            var resp = new Response();

            if (digit == "1")
            {
                String getinput_action_url = "https://www.foo.com/ivr/secondbranch/";

                // Add GetInput XML Tag
                Plivo.XML.GetInput get_input = new
                Plivo.XML.GetInput("",
                    new Dictionary<string, string>()
                    {
                        {"action", getinput_action_url},
                        {"method", "POST"},
                        {"digitEndTimeout", "5"},
                        {"finishOnKey", "#"},
                        {"inputType", "dtmf"},
                        {"redirect", "true"},
                    });
                resp.Add(get_input);
                get_input.AddSpeak(IvrMessage2,
                    new Dictionary<string, string>() { });
                resp.AddSpeak(NoinputMessage,
                    new Dictionary<string, string>() { });
            }
            else if (digit == "2")
            {
                // Add Play XML Tag
                resp.AddPlay(PlivoSong, new Dictionary<string, string>() { });
            }
            else
            {
                // Add Speak XML Tag
                resp.AddSpeak(WronginputMessage,
                    new Dictionary<string, string>() { });
            }

            Debug.WriteLine(resp.ToString());

            var output = resp.ToString();
            return this.Content(output, "text/xml");
        }
        // Second branch of IVR phone tree
        public IActionResult SecondBranch()
        {
            var resp = new Response();
            String digit = Request.Query["Digits"];
            Debug.WriteLine("Digit pressed : {0}", digit);

            // Add Speak XMLTag
            if (digit == "1")
            {
                resp.AddSpeak("This message is being read out in English",
                   new Dictionary<string, string>()
                   {
                    { "language","en-GB"}
                });
            }
            else if (digit == "2")
            {
                resp.AddSpeak("Ce message est lu en français",
                   new Dictionary<string, string>()
                   {
                    { "language","fr-FR"}
                });
            }
            else if (digit == "3")
            {
                resp.AddSpeak("Это сообщение было прочитано в России",
                   new Dictionary<string, string>()
                   {
                    { "language","ru-RU"}
                });
            }
            else
            {
                resp.AddSpeak(WronginputMessage,
                    new Dictionary<string, string>() { });
            }

            Debug.WriteLine(resp.ToString());

            var output = resp.ToString();
            return this.Content(output, "text/xml");
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