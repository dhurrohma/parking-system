using System;
using ParkingSystem.Repository.ParkingRepository;
using ParkingSystem.Utils;

namespace ParkingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== Parking Lot =====");
            Console.Write("Masukkan jumlah total lot parkir : ");
            string inputLot = Console.ReadLine();
            int totalLot = int.Parse(inputLot);
            IParkingRepository parkingRepository = new ParkingRepository(TotalLot: totalLot);

            while (true)
            {
                Console.WriteLine("===== Parking System =====");
                Console.WriteLine("1. Parkir kendaraan");
                Console.WriteLine("2. Kendaraan keluar");
                Console.WriteLine("3. Laporan jumlah lot yang terisi");
                Console.WriteLine("4. Laporan jumlah lot yang tersedia");
                Console.WriteLine("5. Laporan jumlah kendaraan berdasarkan nomor plat ganjil");
                Console.WriteLine("6. Laporan jumlah kendaraan berdasarkan nomor plat genap");
                Console.WriteLine("7. Laporan jumlah kendaraan berdasarkan jenis kendaraan");
                Console.WriteLine("8. Laporan jumlah kendaraan berdasarkan warna kendaraan");
                Console.WriteLine("9. Daftar kendaraan yang sedang diparkir");
                Console.WriteLine("10. Keluar");

                Console.Write("Masukkan pilihan menu : ");
                string menu = Console.ReadLine();

                switch (menu)
                {
                    case "1":
                        Console.WriteLine("===== Vehicle Parking =====");
                        Console.Write("Masukkan nomor slot : ");
                        string inputSlot = Console.ReadLine();
                        int slotNumber = int.Parse(inputSlot);

                        if (slotNumber > totalLot)
                        {
                            Console.WriteLine("Slot tidak tersedia");
                        }
                        else
                        {
                            Console.Write("Masukkan plat nomor kendaraan : ");
                            string platNumber = Console.ReadLine();

                            Console.WriteLine("Jenis kendaraan: ");
                            Console.WriteLine("1. Mobil kecil");
                            Console.WriteLine("2. Motor");

                            Console.Write("Masukkan pilihan jenis kendaraan : ");
                            string inputType = Console.ReadLine();
                            TypeTransportation type;

                            Console.Write("Masukkan warna kendaraan : ");
                            string color1 = Console.ReadLine();

                            if (Enum.TryParse<TypeTransportation>(inputType, out type))
                            {
                                bool successParking = parkingRepository.TransportParking(slotNumber, platNumber, type, color1);
                                if (successParking)
                                {
                                    Console.WriteLine("Kendaraan berhasil diparkir");
                                }
                                else
                                {
                                    Console.WriteLine("Parkiran telah penuh. Kendaraan tidak dapat diparkir");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Input tidak valid");
                            }
                        }
                        break;
                    
                    case "2":
                        Console.WriteLine("===== Vehicle Checkout =====");
                        Console.Write("Masukkan nomor plat kendaraan yang akan keluar : ");
                        string platNumberOut = Console.ReadLine();

                        bool success = parkingRepository.TransportCheckout(platNumberOut);
                        if (success)
                        {
                            Console.WriteLine("Kendaraan berhasil keluar");
                        }
                        else
                        {
                            Console.WriteLine("Kendaraan tidak ditemukan");
                        }
                        break;
                    
                    case "3":
                        Console.WriteLine("===== Report on the number of lots filled =====");
                        int filledLots = parkingRepository.CountFilledLots();
                        Console.WriteLine($"Jumlah lot yang terisi sebanyak {filledLots} slot");
                        break;
                    
                    case "4":
                        Console.WriteLine("===== Report on the number of available lots =====");
                        int availLots = parkingRepository.CountAvailableLots();
                        Console.WriteLine($"Jumlah lot yang tersedia sebanyak {availLots} slot");
                        break;

                    case "5":
                        Console.WriteLine("===== Report the number of vehicles based on odd license plate numbers =====");
                        int oddPlat = parkingRepository.CountTransportationByPlatNumber(true);
                        Console.WriteLine($"Jumlah kendaraan dengan nomor plat ganjil sebanyak {oddPlat} kendaraan");
                        break;

                    case "6":
                        Console.WriteLine("===== Report the number of vehicles based on even license plate numbers =====");
                        int evenPlat = parkingRepository.CountTransportationByPlatNumber(false);
                        Console.WriteLine($"Jumlah kendaraan dengan nomor plat genap sebanyak {evenPlat} kendaraan");
                        break;
                    
                    case "7":
                        Console.WriteLine("===== Report on the number of vehicles based on the type of vehicle =====");
                        Console.WriteLine("Pilihan jenis kendaraan:");
                        Console.WriteLine("1. Mobil Kecil");
                        Console.WriteLine("2. Motor");
                        Console.Write("Masukkan pilihan jenis kendaraan: ");
                        string inputType1 = Console.ReadLine();

                        if (Enum.TryParse<TypeTransportation>(inputType1, out var typeReport))
                        {
                            int type1 = parkingRepository.CountTransportationByTypeTransportation(typeReport);
                            Console.WriteLine($"Jumlah {typeReport} sebanyak {type1}");
                        }
                        else 
                        {
                            Console.WriteLine("Jenis kendaraan tidak valid");
                        }
                        break;
                    
                    case "8":
                        Console.WriteLine("===== Report on the number of vehicles based on color");
                        Console.Write("Masukkan warna kendaraan : ");
                        string inputColor = Console.ReadLine();
                        int color = parkingRepository.CountTranportationByColor(inputColor);
                        Console.WriteLine($"Jumlah kendaraan dengan warna {inputColor} sebanyak {color} kendaraan");
                        break;
                    
                    case "9":
                        Console.WriteLine("===== List of currently parked vehicles =====");
                        Console.WriteLine("Slot, Nomor Plat , Jenis Kendaraan , Warna");
                        var transportParking = parkingRepository.GetTransportParking();
                        foreach (var transportation in transportParking)
                        {
                            Console.WriteLine($"{transportation.slot} , {transportation.platNumber} , {transportation.type} , {transportation.color}");
                        }
                        break;
                    
                    case "10":
                        Console.WriteLine("Terima kasih");
                        return;
                    
                    default:
                        Console.WriteLine("Menu tidak tersedia");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}