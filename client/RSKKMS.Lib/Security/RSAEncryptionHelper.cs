using System;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace RSKKMS.Lib.Security
{
    public enum RSKEncryptionPadding
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
        /// <summary>
        /// Encrypt the plain text payload using the specified X509Certificate2
        /// and the RSAEncryptionPadding
        /// </summary>
        /// <param name="plainText">Plain Text</param>
        /// <param name="cert">X509 Certificate</param>
        /// <param name="encryptionAlgo">Encryption Algo</param>
        /// <returns>Encrypted Text</returns>
        public static string Encrypt(string plainText, 
            X509Certificate2 cert,
            RSKEncryptionPadding encryptionAlgo = RSKEncryptionPadding.Pkcs1)
        {
            byte[] encryptedBytes = null;

            RSA publicKey = (RSA)cert.PublicKey.Key;
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            switch (encryptionAlgo)
            {
                case RSKEncryptionPadding.Pkcs1:
                    encryptedBytes = publicKey.Encrypt(plainBytes,
                        System.Security.Cryptography.RSAEncryptionPadding.Pkcs1);
                    break;
                case RSKEncryptionPadding.OaepSHA256:
                    encryptedBytes = publicKey.Encrypt(plainBytes,
                        System.Security.Cryptography.RSAEncryptionPadding.OaepSHA256);
                    break;
            }
            
            string encryptedText = Convert.ToBase64String(encryptedBytes);
            return encryptedText;
        }

        /// <summary>
        /// Decrypt the specified Encrypted Text using the X509Certificate2
        /// and the RSAEncryptionPadding 
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="cert"></param>
        /// <param name="encryptionAlgo"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptedText, 
            X509Certificate2 cert,
            RSKEncryptionPadding encryptionAlgo = RSKEncryptionPadding.Pkcs1)
        {
            byte[] decryptedBytes = null;

            RSA privateKey = (RSA)cert.PrivateKey;
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
        
            switch (encryptionAlgo)
            {
                case RSKEncryptionPadding.Pkcs1:
                    decryptedBytes = privateKey.Decrypt(encryptedBytes,
                        System.Security.Cryptography.RSAEncryptionPadding.Pkcs1);
                    break;
                case RSKEncryptionPadding.OaepSHA256:
                    decryptedBytes = privateKey.Decrypt(encryptedBytes,
                        System.Security.Cryptography.RSAEncryptionPadding.OaepSHA256);
                    break;
            }

            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedText;
        }
    }
}
