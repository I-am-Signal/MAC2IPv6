/*
 *  MAC2IPv6
 *  Command line conversion script to convert a MAC address to an IPv6 address
 *  Written by Dylan Carder
 *  March 22nd, 2024
 *  Idea suggested by Jake B at Kennesaw State University:
 *  Convert.cs
*/

namespace ConvertMACToIPv6
{
    public static class Converter
    {
        /// <summary>
        /// Returns true if the string provided is in MAC address format
        /// </summary>
        /// <param name="MACAddress"></param>
        /// <returns></returns>
        public static bool CheckMACAddrFormat(string? MACAddress)
        {
            // used to check for delimiters
            char? Delimiter = null;

            // format verification
            if (MACAddress == null || (MACAddress.Length != 12 && MACAddress.Length != 17))
                return false;

            if (MACAddress.Length == 17)
            {
                Delimiter = MACAddress[2];
            }

            else if (MACAddress.Length != 12)
                return false;

            MACAddress.ToLower();

            for (int i = 0; i < MACAddress.Length; i++)
            {
                // checking delimiters when present
                if ((i + 1) % 3 == 0 && Delimiter != null)
                {
                    if (MACAddress[i] != Delimiter)
                        return false;
                    continue;
                }

                // verifying hexadecimal characters
                if (char.IsDigit(MACAddress[i]))
                    continue;

                if (MACAddress[i] < 'a' || MACAddress[i] > 'f')
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns the converted MACAddress string in IPv6 format
        /// </summary>
        /// <param name="MACAddress"></param>
        /// <returns></returns>
        public static string MACAddrToIPv6(string MACAddress)
        {
            // remove delimiters to ease calculation
            if (MACAddress.Length == 17)
            {
                string Temp = MACAddress.Substring(0, 2);
                Temp += MACAddress.Substring(3, 2);
                Temp += MACAddress.Substring(6, 2);
                Temp += MACAddress.Substring(9, 2);
                Temp += MACAddress.Substring(12, 2);
                Temp += MACAddress.Substring(15, 2);
                MACAddress = Temp;
            }

            /******************************************************************************
            *    Title: C# how convert large HEX string to binary
            *    Author: Guffa
            *    Date: 2011
            *    Availability: https://stackoverflow.com/questions/6617284/c-sharp-how-convert-large-hex-string-to-binaryhttps://stackoverflow.com/questions/6617284/c-sharp-how-convert-large-hex-string-to-binary
            *******************************************************************************/
            // convert first octet to binary
            string BinaryString = String.Join(String.Empty, MACAddress.Substring(0, 2).Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            // invert seventh bit
            BinaryString = BinaryString[6] == '0' ?
                BinaryString.Substring(0, 6) + '1' + BinaryString[7]:
                BinaryString.Substring(0, 6) + '0' + BinaryString[7];

            // replace first octet with newly converted one
            MACAddress = Convert.ToInt32(BinaryString, 2).ToString("X").ToLower() +
                MACAddress.Substring(2, 10);

            // add 'fffe' to the middle of the string
            MACAddress = MACAddress.Substring(0, MACAddress.Length / 2) + "fffe" +
                MACAddress.Substring(MACAddress.Length / 2, MACAddress.Length / 2);

            // adjust formatting to IPv6
            string IPv6 = MACAddress.Substring(0, 4) + ":";
            IPv6 += MACAddress.Substring(4, 4) + ":";
            IPv6 += MACAddress.Substring(8, 4) + ":";
            IPv6 += MACAddress.Substring(12, 4);


            // add 'fe80::' to the beginning of the string
            IPv6 = "fe80::" + IPv6;

            return IPv6;
        }
    }
}