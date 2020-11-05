using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace RSKKMS.Lib.Security
{
    public enum SigningAlgorithm
    {
        RSA,
        ECDsa
    }

    /// <summary>
    /// Reused and Refactored code from 
    /// https://stackoverflow.com/questions/37894878/how-can-i-encrypt-decrypt-and-sign-using-pfx-certificate
    /// </summary>
    public class SigningHelper
    {
        public static byte[] SignArbitrarily(SigningAlgorithm signingAlgorithm,
            byte[] data, X509Certificate2 cert)
        {
            switch (signingAlgorithm)
            {
                case SigningAlgorithm.RSA:
                    // .NET 4.6(.0):
                    using (RSA rsa = cert.GetRSAPrivateKey())
                    {
                        if (rsa != null)
                        {
                            // You need to explicitly pick a hash/digest algorithm and padding for RSA,
                            // these are just some example choices.
                            return rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                        }
                    }
                    break;
                case SigningAlgorithm.ECDsa:
                    // .NET 4.6.1:
                    using (ECDsa ecdsa = cert.GetECDsaPrivateKey())
                    {
                        if (ecdsa != null)
                        {
                            // ECDSA signatures need to explicitly choose a hash algorithm, but there
                            // are no padding choices (unlike RSA).
                            return ecdsa.SignData(data, HashAlgorithmName.SHA256);
                        }
                    }
                    break;
                default:
                    throw new InvalidOperationException("No algorithm handler");
            }
            return null;
        }

        // Uses the same choices as SignArbitrarily.
        public static bool VerifyArbitrarily(SigningAlgorithm signingAlgorithm,
            byte[] data, byte[] signature, X509Certificate2 cert)
        {
            using (RSA rsa = cert.GetRSAPublicKey())
            {
                if (rsa != null)
                {
                    return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }

            using (ECDsa ecdsa = cert.GetECDsaPublicKey())
            {
                if (ecdsa != null)
                {
                    return ecdsa.VerifyData(data, signature, HashAlgorithmName.SHA256);
                }
            }

            throw new InvalidOperationException("No algorithm handler");
        }
    }
}
