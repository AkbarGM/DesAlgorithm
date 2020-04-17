using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DES DESalg = DES.Create();
                string sData = "abcdefghabcdefghabcdefghabcdef";
                Console.WriteLine(sData.Length);
                string FileName = "CText.txt";
                EncryptTextToFile(sData, FileName, DESalg.Key, DESalg.IV);
                string Final = DecryptTextFromFile(FileName, DESalg.Key, DESalg.IV);
                Console.WriteLine(Final);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();

        }
        public static void EncryptTextToFile(String Data, String FileName, byte[] Key, byte[] IV)
        {
            try
            {
                FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);
                DES DESalg = DES.Create();
                CryptoStream cStream = new CryptoStream(fStream,
                    DESalg.CreateEncryptor(Key, IV),
                    CryptoStreamMode.Write);
                StreamWriter sWriter = new StreamWriter(cStream);
                sWriter.WriteLine(Data);
                sWriter.Close();
                cStream.Close();
                fStream.Close();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file error occurred: {0}", e.Message);
            }
        }
        public static string DecryptTextFromFile(String FileName, byte[] Key, byte[] IV)
        {
            try
            {
                FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);
                DES DESalg = DES.Create();
                CryptoStream cStream = new CryptoStream(fStream,
                    DESalg.CreateDecryptor(Key, IV),
                    CryptoStreamMode.Read);
                StreamReader sReader = new StreamReader(cStream);
                string val = sReader.ReadLine();
                sReader.Close();
                cStream.Close();
                fStream.Close();
                return val;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file error occurred: {0}", e.Message);
                return null;
            }
        }

    }
}
