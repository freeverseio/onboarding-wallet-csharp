// @author Freeverse.io, www.freeverse.io
// Library to export and import profiles
// Definitions:
// - "PrivateKey": device-generated (using random generation) value that allows users to sign transactions, and manange their assets
// - "Profile": name we give to the encrypted PrivateKey. 
//      · Because it is encrypted This can be shown to other persons without compromising the assets.
//      · In particular, it can be sent to user's email, or shared by whatsapp.
//      · Note that the user can generate many Profiles from the same PrivateKey, by entering different passwords. 
//      · ...they all are equally valid.
// - "Password": user-entered string used to encrypt the PrivateKey and hence to generate the Profile
//      · Only by using the password can anyone recover the PrivateKey from the Profile.

namespace IdentityLibrary
{
    public interface FreeverseIdentity
    {
        bool FreeverseIdFromPrivateKey(string privateKey, out string freeverseId);
        bool DecryptIdentity(string encryptedIdentity, string password, out string pk);
        bool EncryptIdentity(string privateKey, string password, out string encryptedIdentity);
        bool AccountFromPrivateKey(string privateKey, out Ethereum account);
        bool CreateNewAccount(out Ethereum account);
    }
}
