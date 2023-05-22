using System;
using System.Collections.Generic;
using System.Linq;
using ParkingSystem.Models;
using ParkingSystem.Utils;
// using ParkingSystem.Repository.IParkingRepository;

namespace ParkingSystem.Repository.ParkingRepository
{
    public interface IParkingRepository
    {
        bool TransportParking(int slot, string platNumber, TypeTransportation type, string color);
        bool TransportCheckout(string platNumber);
        List<Transportation> GetTransportParking();
        int CountFilledLots();
        int CountAvailableLots();
        int CountTransportationByPlatNumber(bool odd);
        int CountTransportationByTypeTransportation(TypeTransportation type);
        int CountTranportationByColor(string color);
    }

    public class ParkingRepository : IParkingRepository
    {
        private Dictionary<string, Transportation> parkingLot;
        
        public int TotalLot { get; }
        
        public ParkingRepository(int TotalLot)
        {
            this.TotalLot = TotalLot;
            parkingLot = new Dictionary<string, Transportation>();
        }

        public int GetTransportParkingBySlot(int slot)
        {
            foreach (var vehicle in parkingLot.Values)
            {
                if (vehicle.slot == slot )
                {
                    return vehicle.slot;
                }
            }
            return 0;
        }

        public bool TransportParking(int slot, string platNumber, TypeTransportation type, string color)
        {
            if (parkingLot.Count >= TotalLot)
            {
                return false;
            }
            else
            {
                if (GetTransportParkingBySlot(slot) == 0)
                {
                    Transportation transportation = new Transportation
                    {
                        slot = slot,
                        platNumber = platNumber,
                        type = type,
                        color = color,
                        checkInTime = DateTime.Now
                    };

                    parkingLot.Add(platNumber, transportation);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool TransportCheckout(string platNumber)
        {
            if (parkingLot.ContainsKey(platNumber))
            {
                parkingLot.Remove(platNumber);
                return true;
            }

            return false;
        }

        public List<Transportation> GetTransportParking()
        {
            return new List<Transportation>(parkingLot.Values);
        }

        public int CountFilledLots()
        {
            return parkingLot.Count;
        }

        public int CountAvailableLots()
        {
            return TotalLot - parkingLot.Count;
        }

        public int CountTransportationByPlatNumber(bool odd)
        {
            int count = 0;
            foreach (var transportation in parkingLot.Values)
            {
                string platNumber = transportation.platNumber;
                int lastDigit = int.Parse(platNumber[platNumber.Length - 4].ToString());

                if ((lastDigit % 2 == 0 && !odd) || (lastDigit % 2 != 0 && odd))
                {
                    count++;
                }
            }

            return count;
        }

        public int CountTransportationByTypeTransportation(TypeTransportation type)
        {
            int count = 0;
            foreach (var transportation in parkingLot.Values)
            {
                if (transportation.type == type)
                {
                    count++;
                }
            }

            return count;
        }

        public int CountTranportationByColor(string color)
        {
            int count = 0;
            foreach (var transportation in parkingLot.Values)
            {
                if (transportation.color == color)
                {
                    count++;
                }
            }

            return count;
        }
    }
}