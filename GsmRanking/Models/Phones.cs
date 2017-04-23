using System;
using System.Collections.Generic;

namespace GsmRanking.Models
{
    public partial class Phones
    {
        public Phones()
        {
            Comments = new HashSet<Comments>();
        }

        public string Model { get; set; }
        public string Kind { get; set; }
        public string Screen { get; set; }
        public int? BatteryCapacity { get; set; }
        public bool? FastCharge { get; set; }
        public bool? InduCharge { get; set; }
        public bool? Ip68 { get; set; }
        public int? Memory { get; set; }
        public bool? SdCard { get; set; }
        public string SdCardInfo { get; set; }
        public string OsWork { get; set; }
        public string Cpu { get; set; }
        public bool? TouchScreen { get; set; }
        public bool? DualSim { get; set; }
        public DateTime? PremierDate { get; set; }
        public string RearCamera { get; set; }
        public string FrontCamera { get; set; }
        public string Con { get; set; }
        public bool? Lte { get; set; }
        public bool? Hsdpaplus { get; set; }
        public bool? Hsdpa { get; set; }
        public int? Ltedl { get; set; }
        public int? Lteup { get; set; }
        public bool? Gprs { get; set; }
        public bool? Edge { get; set; }
        public bool? Bt { get; set; }
        public string BtInfo { get; set; }
        public bool? A2dp { get; set; }
        public bool? Wifi { get; set; }
        public string WifiInfo { get; set; }
        public bool? Dlna { get; set; }
        public bool? HotSpotWifi { get; set; }
        public bool? Irda { get; set; }
        public bool? Gps { get; set; }
        public bool? Nfc { get; set; }
        public int IdProducer { get; set; }
        public int IdPhone { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual Producers IdProducerNavigation { get; set; }
    }
}
