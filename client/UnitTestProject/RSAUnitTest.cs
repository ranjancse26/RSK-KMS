using System;
using System.IO;
using RSKKMS.Lib.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class RSAUnitTest : BaseUnitTest
    {
        private string certFilename = "";
        private string keyFilename = "";

        [TestInitialize]
        public void Init()
        {
            SetConfiguration();
        }

        /// <summary>
        /// Reused & Enhanced code
        /// https://stackoverflow.com/questions/13806299/how-can-i-create-a-self-signed-certificate-using-c
        /// </summary>
        /// <param name="certFilename">Cert Filename</param>
        /// <param name="keyFilename">Key Filename</param>
        private void CreateCertificate(string certFilename, string keyFilename)
        {
            const string CRT_HEADER = "-----BEGIN CERTIFICATE-----\n";
            const string CRT_FOOTER = "\n-----END CERTIFICATE-----";

            const string KEY_HEADER = "-----BEGIN RSA PRIVATE KEY-----\n";
            const string KEY_FOOTER = "\n-----END RSA PRIVATE KEY-----";

            RSA rsa = RSA.Create();
            var certRequest = new CertificateRequest("cn=test", rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            // We're just going to create a temporary certificate, that won't be valid for long
            var certificate = certRequest.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));

            rsa = (RSA)certificate.PrivateKey;
            (certificate.PrivateKey as RSACng)?.Key.SetProperty(
                new CngProperty(
                    "Export Policy",
                    BitConverter.GetBytes((int)CngExportPolicies.AllowPlaintextExport),
                    CngPropertyOptions.Persist));

            // export the private key
            var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey(), Base64FormattingOptions.InsertLineBreaks);

            File.WriteAllText(keyFilename, KEY_HEADER + privateKey + KEY_FOOTER);

            // Export the certificate
            var exportData = certificate.Export(X509ContentType.Cert);

            var crt = Convert.ToBase64String(exportData, Base64FormattingOptions.InsertLineBreaks);
            File.WriteAllText(certFilename, CRT_HEADER + crt + CRT_FOOTER);
        }

        [TestMethod]
        public void RSAEncryptionHelper_Encrypt()
        {
            certFilename = "TestCert.crt";
            keyFilename = "TestPrivateKeyfile.pem";

            CreateCertificate(certFilename, keyFilename);

            var x509 = new X509Certificate2(File.ReadAllBytes(certFilename));

            string plainText = "testing";
            string encryptedText = RSAEncryptionHelper.Encrypt(plainText,
                x509, RSAEncryptionAlgo.OaepSHA256);

            Assert.IsTrue(encryptedText.Length > 0);
        }

        [TestMethod]
        public void RSAEncryptionHelper_Encrypt_Decrypt()
        {
            certFilename = "TestCert.crt";
            keyFilename = "TestPrivateKeyfile.pem";

            CreateCertificate(certFilename, keyFilename);

            var cert = new X509Certificate2(File.ReadAllBytes(certFilename));

            string privateKeyPem = File.ReadAllText(keyFilename);

            privateKeyPem = privateKeyPem.Replace("-----BEGIN RSA PRIVATE KEY-----", "");
            privateKeyPem = privateKeyPem.Replace("-----END RSA PRIVATE KEY-----", "");

            byte[] privateKeyBytes = Convert.FromBase64String(privateKeyPem);

            using var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
            var certWithPrivateKey = cert.CopyWithPrivateKey(rsa);

            string plainText = "testing";
            string encryptedText = RSAEncryptionHelper.Encrypt(plainText, 
                certWithPrivateKey, RSAEncryptionAlgo.OaepSHA256);
            string decryptedText = RSAEncryptionHelper.Decrypt(encryptedText, 
                certWithPrivateKey, RSAEncryptionAlgo.OaepSHA256);

            Assert.IsTrue(encryptedText.Length > 0);
            Assert.IsTrue(plainText == decryptedText);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(certFilename))
                File.Delete(certFilename);

            if(File.Exists(keyFilename))
                File.Delete(keyFilename);
        }
    }
}
