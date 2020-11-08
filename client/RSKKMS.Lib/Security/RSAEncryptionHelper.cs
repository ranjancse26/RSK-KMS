using System;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace RSKKMS.Lib.Security
{
    public enum RSAEncryptionAlgo
    {
        Pkcs1,
        OaepSHA256
    }

    /*
      *  Reused and Enhanced code
      *  Encrypt/Decrypt from https://stackoverflow.com/questions/16261819/c-sharp-encrypting-and-decrypting-data-using-rsa
    */
    public static class RSAEncryptionHelper
    {
       public static string Encrypt(string plainText, 
            X509Certificate2 cert,
            RSAEncryptionAlgo encryptionAlgo = RSAEncryptionAlgo.Pkcs1)
        {
            byte[] encryptedBytes = null;

            RSA publicKey = (RSA)cert.PublicKey.Key;
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            switch (encryptionAlgo)
            {
                case RSAEncryptionAlgo.Pkcs1:
                    encryptedBytes = publicKey.Encrypt(plainBytes,
                        RSAEncryptionPadding.Pkcs1);
                    break;
                case RSAEncryptionAlgo.OaepSHA256:
                    encryptedBytes = publicKey.Encrypt(plainBytes,
                        RSAEncryptionPadding.OaepSHA256);
                    break;
            }
            
            string encryptedText = Convert.ToBase64String(encryptedBytes);
            return encryptedText;
        }

        public static string Decrypt(string encryptedText, 
            X509Certificate2 cert,
            RSAEncryptionAlgo encryptionAlgo = RSAEncryptionAlgo.Pkcs1)
        {
            byte[] decryptedBytes = null;

            RSA privateKey = (RSA)cert.PrivateKey;
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
        
            switch (encryptionAlgo)
            {
                case RSAEncryptionAlgo.Pkcs1:
                    decryptedBytes = privateKey.Decrypt(encryptedBytes,
                        RSAEncryptionPadding.Pkcs1);
                    break;
                case RSAEncryptionAlgo.OaepSHA256:
                    decryptedBytes = privateKey.Decrypt(encryptedBytes,
                        RSAEncryptionPadding.OaepSHA256);
                    break;
            }

            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedText;
        }
    }
}
