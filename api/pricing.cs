using System;
using System.Collections.Generic;
using Plivo;

namespace pricing {
  class Program {
    static void Main(string[] args) {

      // Get pricing of a country
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
      var response = api.Pricing.Get(
      countryIso: "GB");

      Console.WriteLine(response);
    }
  }
}

/*
Sample Output
{
  "api_id": "bdd130e4-b5ce-11e4-af95-22000ac54c79",
  "country": "United Kingdom",
  "country_code": 44,
  "country_iso": "GB",
  "message": {
    "inbound": {
      "rate": "0.00000"
    },
    "outbound": {
      "rate": "0.03680"
    },
    "outbound_networks_list": [
      {
        "group_name": "United Kingdom - All",
        "rate": "0.03680"
      }
    ]
  },
  "phone_numbers": {
    "local": {
      "rate": "0.80000"
    },
    "tollfree": {
      "rate": "1.40000"
    }
  },
  "voice": {
    "inbound": {
      "ip": {
        "rate": "0.00300"
      },
      "local": {
        "rate": "0.00500"
      },
      "tollfree": {
        "rate": "0.05000"
      }
    },
    "outbound": {
      "ip": {
        "rate": "0.00300"
      },
      "local": {
        "rate": "0.01020"
      },
      "rates": [
        {
          "prefix": [
            "44",
            "44203",
            "44207",
            "44208"
          ],
          "rate": "0.01020"
        },
        {
          "prefix": [
            "443",
            "44551107",
            "4455114",
            "445516",
            "44555500",
            "4455551",
            "4455553",
            "4455554",
            "4455555",
            "44558866",
            "4455888",
            "4456"
          ],
          "rate": "0.01700"
        },
        {
          "prefix": [
            "4470000",
            "4470004",
            "4470005",
            "4470006",
            "4470007",
            "4470020",
            "4470022",
            "4470023",
            "447866",
            "447867",
            "447905",
            "447906",
            "447907",
            "447908",
            "447909",
            "447910",
            "447912",
            "447913",
            "447987",
            "447988",
            "447989",
            "447990",
            "447999"
          ],
          "rate": "0.02650"
        },
        {
          "prefix": [
            "44843",
            "44844",
            "44845"
          ],
          "rate": "0.16520"
        },
        {
          "prefix": [
            "44870"
          ],
          "rate": "0.22350"
        },
        {
          "prefix": [
            "44871",
            "44872",
            "44873"
          ],
          "rate": "0.32010"
        },
        {
          "prefix": [
            "4478360",
            "4478361",
            "4478369"
          ],
          "rate": "0.40880"
        },
        {
          "prefix": [
            "447"
          ],
          "rate": "0.42870"
        },
        {
          "prefix": [
            "4470"
          ],
          "rate": "0.44030"
        }
      ],
      "tollfree": {
        "rate": null
      }
    }
  }
}
*/