using System;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace xmlproiect.Models
{
    public class Customer
    {
        [XmlElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlElement("address")]
        [JsonProperty("address")]
        public string Address { get; set; }

        [XmlElement("phone")]
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [XmlElement("email")]
        [JsonProperty("email")]
        public string Email { get; set; }

        [XmlElement("cnp")]
        [JsonProperty("cnp")]
        public string CNP { get; set; }

        [XmlElement("age")]
        [JsonProperty("age")]
        public int Age { get; set; }

        [XmlElement("driving_license")]
        [JsonProperty("driving_license")]
        public string DrivingLicense { get; set; }
    }

    public class Car
    {
        [XmlElement("model")]
        [JsonProperty("model")]
        public string Model { get; set; }

        [XmlElement("year")]
        [JsonProperty("year")]
        public int Year { get; set; }

        [XmlElement("engine_capacity")]
        [JsonProperty("engine_capacity")]
        public double EngineCapacity { get; set; }

        [XmlElement("transmission")]
        [JsonProperty("transmission")]
        public string Transmission { get; set; }

        [XmlElement("options")]
        [JsonProperty("options")]
        public string Options { get; set; }

        [XmlElement("rate")]
        [JsonProperty("rate")]
        public decimal Rate { get; set; }
    }

    public class Payment
    {
        [XmlElement("card_type")]
        [JsonProperty("card_type")]
        public string CardType { get; set; }

        [XmlElement("card_number")]
        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [XmlElement("expiration_date")]
        [JsonProperty("expiration_date")]
        public string ExpirationDate { get; set; }

        [XmlElement("total_cost")]
        [JsonProperty("total_cost")]
        public decimal TotalCost { get; set; }
    }

    public class Reservation
    {
        [XmlAttribute("numar")]
        [JsonProperty("@numar")]
        public int Number { get; set; }

        [XmlElement("customer")]
        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [XmlElement("car")]
        [JsonProperty("car")]
        public Car Car { get; set; }

        [XmlElement("pickup_date")]
        [JsonProperty("pickup_date")]
        public DateTime PickupDate { get; set; }

        [XmlElement("return_date")]
        [JsonProperty("return_date")]
        public DateTime ReturnDate { get; set; }

        [XmlElement("payment")]
        [JsonProperty("payment")]
        public Payment Payment { get; set; }
    }

    [XmlRoot("reservation_info")]
    public class ReservationInfo
    {
        [XmlElement("rezervare")]
        public List<Reservation> Reservations { get; set; }
    }

    
}
