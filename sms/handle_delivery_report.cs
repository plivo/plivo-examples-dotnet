using Nancy;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Handle_delivery {
  public class Program: NancyModule {
    public Program() {
      Post("/delivery", parameters =>{

        String from_number = Request.Form["From"]; // Sender's phone number
        String to_number = Request.Form["To"]; // Receiver's phone number
        String text = Request.Form["Text"]; // The text which was received
        Console.WriteLine("Message received - From: {0}, To: {1}, Text: {2}", from_number, to_number, text); // Print the message
        return "Delivery status reported";
      }
    }
  }

  // Sample Output
  // From : 2222222222 To : 1111111111 Status : queued MessageUUID : 53e6526a-8a7a-11e4-a77d-22000ae383ea
  // From : 2222222222 To : 1111111111 Status : sent MessageUUID : 53e6526a-8a7a-11e4-a77d-22000ae383ea
  // From : 2222222222 To : 1111111111 Status : delivered MessageUUID : 53e6526a-8a7a-11e4-a77d-22000ae383ea
  // Possible values for message status - "queued", "sent", "failed", "delivered", "undelivered" or "rejected"
  