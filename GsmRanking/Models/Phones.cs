using System;
using System.Collections.Generic;

namespace GsmRanking.Models
{
    public partial class Phones
    {
        public string Model { get; set; }
        public string Kind { get; set; }
        public string Screen { get; set; }
        public int? Batterycapacity { get; set; }
        public bool? Fastcharge { get; set; }
        public bool? Inducharge { get; set; }
        public bool? Ip68 { get; set; }
        public int? Memory { get; set; }
        public bool? Sdcard { get; set; }
        public string Sdcardinfo { get; set; }
        public string Oswork { get; set; }
        public string Cpu { get; set; }
        public bool? Touchscreen { get; set; }
        public bool? Dualsim { get; set; }
        public DateTime? Premierdate { get; set; }
        public string Rearcamera { get; set; }
        public string Frontcamera { get; set; }
        public string Con { get; set; }
        public bool? Lte { get; set; }
        public bool? Hsdpaplus { get; set; }
        public bool? Hsdpa { get; set; }
        public int? Ltedl { get; set; }
        public int? Lteup { get; set; }
        public bool? Gprs { get; set; }
        public bool? Edge { get; set; }
        public bool? Bt { get; set; }
        public string Btinfo { get; set; }
        public bool? A2dp { get; set; }
        public bool? Wifi { get; set; }
        public string Wifinfo { get; set; }
        public bool? Dlna { get; set; }
        public bool? Hswifi { get; set; }
        public bool? Irda { get; set; }
        public bool? Gps { get; set; }
        public bool? Nfc { get; set; }
        public int Producerid { get; set; }
        public int PhoneId { get; set; }

        public virtual Producers Producer { get; set; }
    }
}
