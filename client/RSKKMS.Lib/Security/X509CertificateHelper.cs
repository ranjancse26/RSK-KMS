using System.Security.Cryptography.X509Certificates;

namespace RSKKMS.Lib.Security
{
    public class X509CertificateHelper
    { 
        /// <summary>
        /// Get the filtered certificate by the ThumbPrint
        /// </summary>
        /// <param name="thumbPrint">ThumbPrint</param>
        /// <returns>X509Certificate2</returns>
        public static X509Certificate2 GetRSKCertificate(string thumbPrint,
            StoreLocation storeName)
        {
            X509Store store = new X509Store(StoreName.TrustedPeople,
                storeName);
            store.Open(OpenFlags.ReadOnly);

            var certifiates = store.Certificates;
            X509Certificate2 filteredCert = null;

            foreach (var certificate in certifiates)
            {
                if (certificate.Thumbprint.Equals(thumbPrint))
                {
                    filteredCert = certificate;
                    break;
                }
            }

            return filteredCert;
        }
    }
}
