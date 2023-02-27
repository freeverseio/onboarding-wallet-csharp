using IdentityLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace Test
{
    [TestClass]
    public class IdentityTest
    {
        const string _groundtruthFilename = "../../../identity_groundtruth/identity_groundtruth.json";

        private FreeverseIdentity _id;
        private TestUtils _tu;
        private dynamic _tests;   

        [TestInitialize]
        public void SetUp()
        {
            _id = new IdentityService();
            _tu = new TestUtils();
            _tests = _tu.LoadJson(_groundtruthFilename);        
        }

        [TestMethod]
        public void IdentityImportTest()
        {
            foreach(dynamic test in _tests) {
                string privateKey;
                bool ok = _id.DecryptIdentity((string) test.encryptedIdentity, (string) test.userPassword, out privateKey);
                Assert.AreEqual((string) test.privateKey, privateKey);
                Assert.IsTrue(ok);
            }
        }

        [TestMethod]
        public void IdentityImportBadPasswordTest()
        {
            const string _wrongPassword = "password";
            foreach(dynamic test in _tests) {
            string privateKey;
                bool ok = _id.DecryptIdentity((string) test.encryptedIdentity, _wrongPassword, out privateKey);
                Assert.IsFalse(ok);
            }
        }

        [TestMethod]
        public void IdentityExportAndThenImportTest()
        {
            foreach(dynamic test in _tests) {
                string exportedEncryptedPK;
                bool ok = _id.EncryptIdentity((string) test.privateKey, (string) test.userPassword, out exportedEncryptedPK);
                Assert.IsTrue(ok);
                Console.WriteLine(exportedEncryptedPK);

                string privateKey;
                ok = _id.DecryptIdentity(exportedEncryptedPK, (string) test.userPassword, out privateKey);
                Assert.IsTrue(ok);
                Assert.AreEqual((string) test.privateKey, privateKey);
            }
        }

        [TestMethod]
        public void DecryptWrongIdentity()
        {
            string exportedEncryptedPK = "c33dcc598252fbbb4a94ff2d0f70dbe7d77360d8ca4a036ad1dd80bc4c7bb0b818517bdd5fdd0cf0562080e33559bfab637d3ed3ccd6ddfdbd58b8d8874047bf";
            string expectedPK = "0x56450b9e335eb41b0c90454285001f793e7bac2b2c94c353c392b38a2292e7d0";
            string correctPassword = "P@ssw0rd";
            string privateKey;
            bool ok = _id.DecryptIdentity(exportedEncryptedPK, correctPassword, out privateKey);
            Assert.AreEqual(privateKey, expectedPK);
            Assert.IsTrue(ok);
            string wrongPassword = "password";
            ok = _id.DecryptIdentity(exportedEncryptedPK, wrongPassword, out privateKey);
            Assert.AreNotEqual(privateKey, expectedPK);
            Assert.IsFalse(ok);
        }

        [TestMethod]
        public void DecryptFromTheField()
        {
            foreach(dynamic test in _tests) {
                string privateKey;
                bool ok = _id.DecryptIdentity((string) test.encryptedIdentity, (string) test.userPassword, out privateKey);
                Assert.AreEqual((string) test.privateKey, privateKey);
                Assert.IsTrue(ok);
            }
        }
        [TestMethod]
        public void FreeverseIdTest()
        {
            foreach(dynamic test in _tests) {
                string freeverseId;
                bool ok = _id.FreeverseIdFromPrivateKey((string) test.privateKey, out freeverseId);
                Assert.IsTrue(ok);
                Assert.AreEqual((string) test.freeverseId, freeverseId);
            }
        }

        [TestMethod]
        public void EncryptionIsDifferentForSamePasswordTest()
        {
            Ethereum account;
            bool ok = _id.CreateNewAccount(out account);
            Assert.IsTrue(ok);
            string privateKey = account.GetPrivateKey();
            string password = "1234";
            string encryptedIdentity1;
            string encryptedIdentity2;
            ok = _id.EncryptIdentity(privateKey, password, out encryptedIdentity1);
            Assert.IsTrue(ok);
            ok = _id.EncryptIdentity(privateKey, password, out encryptedIdentity2);
            Assert.IsTrue(ok);
            Assert.AreNotEqual(encryptedIdentity1, encryptedIdentity2);
        }
        
        // [TestMethod]
        // public void CreatedAccountsAreAlwaysDifferentTest()
        // {
        //     Ethereum account1;
        //     Ethereum account2;
        //     bool ok = _id.CreateNewAccount(out account1);
        //     Assert.IsTrue(ok);
        //     ok = _id.CreateNewAccount(out account2);
        //     Assert.IsTrue(ok);
        //     Assert.AreNotEqual((string) account1.GetPrivateKey(), (string) account2.GetPrivateKey());
        // }
        
    }
    
}
