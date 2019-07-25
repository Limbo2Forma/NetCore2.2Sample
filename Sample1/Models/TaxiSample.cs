using System;

namespace Sample1.Models {
    public class TaxiSample {
        public long Id { get; set; }
        public string Medallion { get; set; }
        public string Hack_License { get; set; }
        public string Vendor_Id { get; set; }
        public string Rate_Code { get; set; }
        public string Store_And_Fwd_Flag { get; set; }
        public DateTime Pickup_Datetime { get; set; }
        public DateTime? Dropoff_Datetime { get; set; }
        public int? Passenger_Count { get; set; }
        public long? Trip_Time_In_Secs { get; set; }
        public double? Trip_Distance { get; set; }
        public string Pickup_Longitude { get; set; }
        public string Pickup_Latitude { get; set; }
        public string Dropoff_Longitude { get; set; }
        public string Dropoff_Latitude { get; set; }
        public string Payment_Type { get; set; }
        public double? Fare_Amount { get; set; }
        public double? Surcharge { get; set; }
        public double? Mta_Tax { get; set; }
        public double? Tolls_Amount { get; set; }
        public double? Total_Amount { get; set; }
        public double? Tip_Amount { get; set; }
        public int? Tipped { get; set; }
        public int? Tip_Class { get; set; }       
    }
}
