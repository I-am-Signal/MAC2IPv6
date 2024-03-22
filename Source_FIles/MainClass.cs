/*
 *  MAC2IPv6
 *  Command line conversion script to convert a MAC address to an IPv6 address
 *  Written by Dylan Carder
 *  March 22nd, 2024
 *  Idea suggested by Jake B at Kennesaw State University
 *  https://discord.com/channels/730029345954988082/1140806104402640917/1220599398204047420
 *  MainClass.cs
*/

namespace ConvertMACToIPv6
{
    public class MainClass
    {
        /// <summary>
        /// Displays the IPv6 address to the console if the address is in the correct format
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static void CheckThenConvert(string? Address)
        {
            // correct format
            if (Converter.CheckMACAddrFormat(Address))
            {
                Console.WriteLine(" IPv6 Address: " + Converter.MACAddrToIPv6(Address + ""));
            }

            // incorrect format
            else
            {
                Console.WriteLine(" MAC Address is not in the correct format.");
                Console.WriteLine(" MAC Address formats:");
                Console.WriteLine("  1) XX:XX:XX:XX:XX:XX");
                Console.WriteLine("  2) XXXXXXXXXXXX");
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("[MAC Address to IPv6 Address]");
            Console.Write(" MAC Address: ");

            // using the command line initialization
            if (args != null && args.Length == 1)
            {
                Console.WriteLine(args[0]);
                CheckThenConvert(args[0]);
                return;
            }

            // using the command line menu
            CheckThenConvert(Console.ReadLine());
        }
    }
}