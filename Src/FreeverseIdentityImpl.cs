using System;

namespace IdentityLibrary
{
    public class IdentityService : FreeverseIdentity
    {
        public bool FreeverseIdFromPrivateKey(string privateKey, out string freeverseId)
        {
            var account = new Ethereum(privateKey);
            freeverseId = account.GetEthAddress();
            return freeverseId != null;
        }

        // returns false if either the decryption process failed, 
        // or if the decrypted private key cannot generate a valid FreeverseID 
        public bool DecryptIdentity(string encryptedIdentity, string password, out string pk)
        {
            bool ok = Cryptography.Decrypt(encryptedIdentity, password, out pk);
            // if (!ok) return false;
            // IsValidPrivateKey(pk, out ok);
            return ok;
        }

        // Verifies that the provided private key can generate a valid FreeverseID
        public bool IsValidPrivateKey(string privateKey, out bool ok)
        {
            ok = true;
            try
            {
                string freeverseId;
                FreeverseIdFromPrivateKey(privateKey, out freeverseId);
            }
            catch (Exception)
            {
                ok = false;
            }
            return ok;
        }

        // todo: if encryptPK returns bool, this one should too
        public bool EncryptIdentity(string privateKey, string password, out string encryptedIdentity)
        {
            bool ok = Cryptography.Encrypt(privateKey, password, out encryptedIdentity);
            return ok;
        }

        public bool AccountFromPrivateKey(string privateKey, out Ethereum account) 
        {
            account = new Ethereum(privateKey);
            return account.GetPrivateKey() == privateKey;
        }
        public bool CreateNewAccount(out Ethereum account) 
        {
            account = new Ethereum();
            return account.GetPrivateKey() != null;
        }
    }
}
