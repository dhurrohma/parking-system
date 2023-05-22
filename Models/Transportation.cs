using System;
using ParkingSystem.Utils;

namespace ParkingSystem.Models
{
    public class Transportation
    {
        public int slot { set; get; }
        public string platNumber { set; get; }
        public TypeTransportation type { set; get; }
        public string color { set; get; }
        public DateTime checkInTime { set; get; }
    }
}