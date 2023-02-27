using System;

namespace IdentityLibrary
{
    public static class Cryptography
    {
        public static bool Decrypt(string pkEncrypted, string pass, out string pk)
        {
            bool ok;

            try
            {
                pk = AESEncryption.AESDecrypt(pkEncrypted, pass);
                ok = true;

            }
            catch (Exception)
            {
                pk = "";
                ok = false;
            }

            ok = ok && (pk != "");
            return ok;
        }

        public static bool Encrypt(string privateKey, string password, out string encryptedPK)
        {
            bool ok;

            try
            {
                encryptedPK = AESEncryption.AESEncrypt(privateKey, password);;
                ok = true;
            }
            catch (Exception)
            {
                encryptedPK = "";
                ok = false;
            }

            ok = ok && (encryptedPK != "");
            return ok;
        }
    }

}
