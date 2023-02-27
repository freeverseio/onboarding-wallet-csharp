using Nethereum.Signer;

namespace IdentityLibrary
{
    public class Ethereum
    {
        private EthECKey _eckey = null;

        public Ethereum()
        {
            CreateAccount();
        }

        public Ethereum(string privateKey)
        {
            RegenerateAccount(privateKey);
        }

        private void CreateAccount()
        {
            _eckey = EthECKey.GenerateKey();
        }

        private void RegenerateAccount(string privateKey)
        {
            _eckey = new EthECKey(privateKey);
        }

        public string GetEthAddress()
        {
            if (_eckey == null) { return null; }
            return _eckey.GetPublicAddress();
        }

        public string GetPrivateKey()
        {
            if (_eckey == null) { return null; }
            return _eckey.GetPrivateKey();
        }
    }
}
