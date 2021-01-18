using System;
using System.Collections.Generic;
using Plivo;

namespace Long_Sms {
  class Program {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      // Send a long message
      var response = api.Message.Create(
      src: "1111111111", // Sender's phone number with country code
      dst: new List < String > {
        "2222222222"
      },
      // Receiver's phone number wiht country code
      text: "This randomly generated text can be used in your layout (webdesign , websites, books, posters ... ) for free. This text is entirely free of law. Feel free to link to this site by using the image below or by making a simple text link", // Your SMS text message - English
      // Send uicode text
      // Your SMS text message - Japanese
      // {"text", "このランダムに生成されたテキストは、自由のためのあなたのレイアウト（ウェブデザイン、ウェブサイト、書籍、ポスター...）で使用することができます。このテキストは、法律の完全に無料です。下の画像を使用して、または単純なテキストリンクを作ることで、このサイトへのリンクフリーです"}
      // Your SMS text message - French
      // {"text", "Ce texte généré aléatoirement peut-être utilisé dans vos maquettes (webdesign, sites internet,livres, affiches...) gratuitement. Ce texte est entièrement libre de droit. N'hésitez pas à faire un lien sur ce site en utilisant l'image ci-dessous ou en faisant un simple lien texte}
      );

      // Prints the message details
      Console.Write(response;

      // Get the details of the sent message
      string uuid = response.MessageUuid[0];

      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      var response = api.Message.Get(
      messageUuid: "your_message_uuid");

      Console.WriteLine("Number of units : {0}", response.Units);
    }
  }
}

// Sample output
/*
(202, {
   u'message': u'message(s) queued', 
   u'message_uuid': [u'dcfc1510-9260-11e4-b1a4-22000ac693b1'], 
   u'api_id': u'dce8fb42-9260-11e4-b932-22000ac50fac'
   }
)

For English
Number of units : 2

For Japanese
Number of units : 3

For French
Number of units : 5
*/