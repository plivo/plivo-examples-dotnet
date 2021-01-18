using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace text_to_speech {
  public class Program: NancyModule {
    public Program() {
      Get["/speech"] = x => {
        Plivo.XML.Response resp = new Plivo.XML.Response();

        // Add Speak XML Tag
        resp.AddSpeak("This is English", new Dictionary < string, string > () {
          {
            "language",
            "en-GB"
          }, {
            "voice",
            "MAN"
          }
        });

        // Add Speak XML Tag
        resp.AddSpeak("Ce texte généré aléatoirement peut-être utilisé dans vos maquettes", new Dictionary < string, string > () {
          {
            "language",
            "fr-FR"
          }, {
            "voice",
            "WOMAN"
          }
        });

        // Add Speak XML Tag
        resp.AddSpeak("Это случайно сгенерированный текст может быть использован в макете", new Dictionary < string, string > () {
          {
            "language",
            "ru-RU"
          }, {
            "voice",
            "WOMAN"
          }
        });

        Debug.WriteLine(resp.ToString());

        var output = resp.ToString();
        var res = (Nancy.Response) output;
        res.ContentType = "text/xml";
        return res;
      };
    }
  }
}
/*
Sample Output
<Response>
    <Speak language="en-GB" voice="MAN">This is English</Speak>
    <Speak language="fr-FR" voice="WOMAN">
        Ce texte généré aléatoirement peut-être utilisé dans vos maquettes
    </Speak>
    <Speak language="ru-RU" voice="WOMAN">
        Это случайно сгенерированный текст может быть использован в макете
    </Speak>
</Response>
*/