using AspnetCoreMvcFull.Utilities;
using AspnetCoreMvcFull.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AspnetCoreMvcFull.Utilities
{
    public static class Security
    {
        public static string API_MOBILE_USERNAME
        {
            get { return "api_mobile_snoopie"; }
        }
        public static string API_SNOOPIE_PASSWORD
        {
            get { return "AIWxERCUHGX"; }
        }
        public static IDictionary<string, string> keysDictionary { get; private set; }
        static Security()
        {
            keysDictionary = new Dictionary<string, string>();
            keysDictionary.Add("#01", "IH2udpvk3xuViqVhg5Ui5ht9ah3MBgKh");
            keysDictionary.Add("#02", "ZBa6d6KiAbH8zf6bdmTxJ3maRBoUCFgT");
            keysDictionary.Add("#03", "UsvKNDJrkIp4zf86vsyYjwUCfp2W4hap");
            keysDictionary.Add("#04", "jS4wgaXthgReVmjtn5NzGLTCito0X7wJ");
            keysDictionary.Add("#05", "bTHpi534UnsLDFnb6ne1K3RSfyfEAQdT");
            keysDictionary.Add("#06", "OuwdEniaJ2et7YjodYeY5yMwntDsrUIk");
            keysDictionary.Add("#07", "txqbUezWNMIg0odghomUWOIfJMdOFTtG");
            keysDictionary.Add("#08", "FdlZx0dAay5PyJhytmSRbv3P4sLLtUNe");
            keysDictionary.Add("#09", "y4v3Ma6UwKDINJb3X1BLOtRJXh3Ti5Wn");
            keysDictionary.Add("#10", "VYtKN4drSPZCa2AUVwXBBuLMjOuN7pKQ");
            keysDictionary.Add("#11", "qEkJ2MrRtfTZnSqUth4zsDq9z4yPDhG6");
            keysDictionary.Add("#12", "eOvrMlPwQrTboE6x88hfqkUG9EHf3JMH");
        }

        public static string SecurityKey
        {
            get { return "Key^&!##$!100"; }
        }
        public static string iv
        {
            get { return "8902"; }
        }

        public static String Encrypt(string plainText, string key, string iv)
        {
            string cypherText = "";
            try
            {
                CryptLib _crypt = new CryptLib();
                //  string iv = CryptLib.GenerateRandomIV(16); //16 bytes = 128 bits
                key = CryptLib.getHashSha256(key, 31); //32 bytes = 256 bits
                cypherText = _crypt.encrypt(plainText, key, iv);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return cypherText;
        }

        public static String Decrypt(string cypherText, string key, string iv)
        {
            string output = "";
            try
            {
                CryptLib _crypt = new CryptLib();
                key = CryptLib.getHashSha256(key, 32);
                output = _crypt.decrypt(cypherText, key, iv); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
            return output;
        }

        public static class WindowsPhoneEncryption
        {
            public static string Encrypt(string plainData, string password, string salt)
            {
                AesManaged aes = null;
                MemoryStream memoryStream = null;
                CryptoStream cryptoStream = null;

                try
                {
                    //Generate a Key based on a Password, Salt and HMACSHA1 pseudo-random number generator 
                    Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

                    //Create AES algorithm with 256 bit key and 128-bit block size 
                    aes = new AesManaged();
                    aes.Key = rfc2898.GetBytes(aes.KeySize / 8);
                    rfc2898.Reset(); //needed for WinRT compatibility
                    aes.IV = rfc2898.GetBytes(aes.BlockSize / 8);

                    //Create Memory and Crypto Streams 
                    memoryStream = new MemoryStream();
                    cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

                    //Encrypt Data 
                    byte[] data = Encoding.Unicode.GetBytes(plainData);
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();

                    //Return encrypted data 
                    byte[] encryptedData = memoryStream.ToArray();
                    return Convert.ToBase64String(encryptedData);
                }
                catch (Exception eEncrypt)
                {
                    return null;
                }
                finally
                {
                    if (cryptoStream != null)
                        cryptoStream.Close();

                    if (memoryStream != null)
                        memoryStream.Close();

                    if (aes != null)
                        aes.Clear();

                }
            }
            //another example https://www.codeproject.com/Tips/432033/Encrypt-Decrypt-Strings-in-Silverlight
            public static string Decrypt(string encrypteData, string password, string salt)
            {
                AesManaged aes = null;
                MemoryStream memoryStream = null;
                CryptoStream cryptoStream = null;
                string decryptedText = "";
                try
                {

                    byte[] dataToDecrypt = Convert.FromBase64String(encrypteData);
                    //Generate a Key based on a Password, Salt and HMACSHA1 pseudo-random number generator 
                    Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

                    //Create AES algorithm with 256 bit key and 128-bit block size 
                    aes = new AesManaged();
                    aes.Key = rfc2898.GetBytes(aes.KeySize / 8);
                    rfc2898.Reset(); //neede to be WinRT compatible
                    aes.IV = rfc2898.GetBytes(aes.BlockSize / 8);

                    //Create Memory and Crypto Streams 
                    memoryStream = new MemoryStream();
                    cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

                    //Decrypt Data 
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    //Return Decrypted String 
                    byte[] decryptBytes = memoryStream.ToArray();
                    decryptedText = Encoding.Unicode.GetString(decryptBytes, 0, decryptBytes.Length);
                }
                catch (Exception eDecrypt)
                {

                }
                finally
                {
                    if (cryptoStream != null)
                        cryptoStream.Close();

                    if (memoryStream != null)
                        memoryStream.Close();

                    if (aes != null)
                        aes.Clear();
                }
                return decryptedText;
            }

        }
        public static class PassowrdsHelper
        { 
            public static string GeneratePassword(int length) //length of salt    
            {
                try
                {
                const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
                var randNum = new Random();
                var chars = new char[length];
                var allowedCharCount = allowedChars.Length;
                for (var i = 0; i <= length - 1; i++)
                {
                    chars[i] = allowedChars[Convert.ToInt32((allowedChars.Length) * randNum.NextDouble())];
                }
                return new string(chars);
                }
                catch (Exception ex)
                {
                  //  throw new Exception("Error in base64Encode" + ex.Message);
                }
                return "Kkh9";
            }
            public static string EncodePassword(string pass, string salt) //encrypt password    
            {
                byte[] bytes = Encoding.Unicode.GetBytes(pass);
                byte[] src = Encoding.Unicode.GetBytes(salt);
                byte[] dst = new byte[src.Length + bytes.Length];
                System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
                System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
                HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
                byte[] inArray = algorithm.ComputeHash(dst);
                //return Convert.ToBase64String(inArray);    
                return EncodePasswordMd5(Convert.ToBase64String(inArray));
            }
            public static string EncodePasswordMd5(string pass) //Encrypt using MD5    
            {
                Byte[] originalBytes;
                Byte[] encodedBytes;
                MD5 md5;
                //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
                md5 = new MD5CryptoServiceProvider();
                originalBytes = ASCIIEncoding.Default.GetBytes(pass);
                encodedBytes = md5.ComputeHash(originalBytes);
                //Convert encoded bytes back to a 'readable' string    
                return BitConverter.ToString(encodedBytes);
            }
            public static string base64Encode(string sData) // Encode    
            {
                try
                {
                    byte[] encData_byte = new byte[sData.Length];
                    encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                    string encodedData = Convert.ToBase64String(encData_byte);
                    return encodedData;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in base64Encode" + ex.Message);
                }
            }
            public static string base64Decode(string sData) //Decode    
            {
                try
                {
                    var encoder = new System.Text.UTF8Encoding();
                    System.Text.Decoder utf8Decode = encoder.GetDecoder();
                    byte[] todecodeByte = Convert.FromBase64String(sData);
                    int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                    char[] decodedChar = new char[charCount];
                    utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                    string result = new String(decodedChar);
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in base64Decode" + ex.Message);
                }
            }
        }


        public static string EncryptUrl(string plainText)
        {
            string key = "jdsa674520#";
            byte[] EncryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            EncryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
        public static string DecryptUrl(string encryptedText)
        {
            string key = "jdsa674520#";
            byte[] DecryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] inputByte = new byte[encryptedText.Length];

            DecryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByte = Convert.FromBase64String(encryptedText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByte, 0, inputByte.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        #region --> Generate SALT Key AND HASH Using SHA512

        public static byte[] Get_SALT()
        {
            return Get_SALT(Constants.saltLengthLimit);
        }

        public static byte[] Get_SALT(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];

            //Require NameSpace: using System.Security.Cryptography;  
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }  

        public static string Get_HASH_SHA512(string password, string username, byte[] salt)
        {
            try
            {
                //required NameSpace: using System.Text;  
                //Plain Text in Byte  
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(password + username);

                //Plain Text + SALT Key in Byte  
                byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + salt.Length];

                for (int i = 0; i < plainTextBytes.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainTextBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = salt[i];
                }

                HashAlgorithm hash = new SHA512Managed();
                byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
                byte[] hashWithSaltBytes = new byte[hashBytes.Length + salt.Length];

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashWithSaltBytes[i] = hashBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    hashWithSaltBytes[hashBytes.Length + i] = salt[i];
                }

                return Convert.ToBase64String(hashWithSaltBytes);
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion 

    }
}
