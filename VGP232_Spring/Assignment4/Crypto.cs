using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Assignment4
{
    public enum CryptoAlgorithm { AES, RSA };
    public class Crypto
    {
        AesCryptoServiceProvider aes;
        RSACryptoServiceProvider rsa;

        CryptoAlgorithm mType;

        public string publicKeyPathRSA;
        public string privateKeyPathRSA;

        public void Initialize(CryptoAlgorithm type)
        {
            mType = type;

            if (type == CryptoAlgorithm.AES)
            {
                aes = new AesCryptoServiceProvider();
            }
            else if (type == CryptoAlgorithm.RSA)
            {
                rsa = new RSACryptoServiceProvider();
            }
        }

        public void Terminate()
        {
            if (aes != null)
            {
                aes.Dispose();
            }

            if (rsa != null)
            {
                rsa.Dispose();
            }
        }

        public byte[] Encrypt(byte[] data)
        {
            if (mType == CryptoAlgorithm.AES)
            {
                if (data == null || data.Length == 0)
                {
                    return null;
                }

                using(var ms= new MemoryStream())
                {
                    using(var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
            else
            {
                byte[] cipherBytes;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024))
                {
                    rsa.PersistKeyInCsp = false;
                    string publicKey = File.ReadAllText(publicKeyPathRSA);
                    rsa.FromXmlString(publicKey);
                    cipherBytes = rsa.Encrypt(data, false);
                }
                return cipherBytes;
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            if (mType == CryptoAlgorithm.AES)
            {
                if (data == null || data.Length == 0)
                {
                    return null;
                }

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
            else
            {
                byte[] TextBytes;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024))
                {
                    rsa.PersistKeyInCsp = false;
                    string publicKey = File.ReadAllText(privateKeyPathRSA);
                    rsa.FromXmlString(publicKey);
                    TextBytes = rsa.Decrypt(data, false);
                }
                return TextBytes;
            }
        }

        public void ImportPrivateKey(string path)
        {
            if (mType == CryptoAlgorithm.AES)
            {
                byte[] Sharedkey = File.ReadAllBytes(path);
                aes.Key = Sharedkey;
            }
            else
            {
                string xmlString = File.ReadAllText(path);
                rsa.FromXmlString(xmlString);
            }
        }

        public void ImportPublicKey(string path)
        {
            if (mType == CryptoAlgorithm.AES)
            {
                byte[] IV = File.ReadAllBytes(path);
                aes.Key = IV;
            }
            else
            {
                string xmlString = File.ReadAllText(path);
                rsa.FromXmlString(xmlString);
            }
        }

        public void ExportPrivateKey(string privateKeyPath)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024))
            {
                rsa.PersistKeyInCsp = false;

                string privateKey = rsa.ToXmlString(true);
                using (var w = new StreamWriter(privateKeyPath))
                {
                    w.WriteLine(privateKey);
                }
            }
        }

        public void ExportPublicKey(string publicKeyPath)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024))
            {
                rsa.PersistKeyInCsp = false;

                string publicKey = rsa.ToXmlString(false);
                using (var w = new StreamWriter(publicKeyPath))
                {
                    w.WriteLine(publicKey);
                }
            }
        }

        public void ExportAESKey(string path)
        {
            File.WriteAllBytes(path, aes.Key);
        }

        public void ExportAESIV(string path)
        {
            File.WriteAllBytes(path, aes.IV);
        }
    }
}
