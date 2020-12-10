using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2015
{
    public class Problem4
    {
        public static void Part1()
        {
            HashAlgorithm md5 = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5"));

            for (int i = 100000; i < 10000000; i++)
            {
                string password = @"ckczppom" + i.ToString().PadLeft(6);

                // byte array representation of that string
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);

                // need MD5 to calculate the hash
                byte[] hash = md5.ComputeHash(encodedPassword);

                // string representation (similar to UNIX format)
                string encoded = BitConverter.ToString(hash);

                if (encoded.StartsWith("00-00-0"))
                {
                    Console.WriteLine($"{i} - {encoded}");
                    return;
                }                                       
            }            
        }

        public static void Part2()
        {
            HashAlgorithm md5 = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5"));

            for (int i = 100000; i < 10000000; i++)
            {
                string password = @"ckczppom" + i.ToString().PadLeft(6);

                // byte array representation of that string
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);

                // need MD5 to calculate the hash
                byte[] hash = md5.ComputeHash(encodedPassword);

                // string representation (similar to UNIX format)
                string encoded = BitConverter.ToString(hash);

                if (encoded.StartsWith("00-00-00"))
                {
                    Console.WriteLine($"{i} - {encoded}");
                    return;
                }
            }
        }
    }
}