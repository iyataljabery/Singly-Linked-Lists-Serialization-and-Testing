using Utilites.Tests;
using NUnit.Framework;
using System.IO;

namespace Utilites.Tests
{
    public class SerializationTests
    {
        private ILinkedListADT<User> users;
        private readonly string testFileName = "test_users.bin";

        [SetUp]
        public void Setup()
        {
            this.users = new SinglyLinkedList();

            users.AddLast(new User(1, "Joe Blow", "jblow@gmail.com", "password"));
            users.AddLast(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));
            users.AddLast(new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"));
            users.AddLast(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999"));
        }

        [TearDown]
        public void TearDown()
        {
            this.users.Clear();
            if (File.Exists(testFileName))
            {
                File.Delete(testFileName);
            }
        }

        /// <summary>
        /// Tests that the object was serialized.
        /// </summary>
        [Test]
        public void TestSerialization()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            Assert.IsTrue(File.Exists(testFileName));
        }

        /// <summary>
        /// Tests that the object was deserialized correctly.
        /// </summary>
        [Test]
        public void TestDeSerialization()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            ILinkedListADT<User> deserializedUsers = SerializationHelper.DeserializeUsers(testFileName);

            Assert.AreEqual(users.Count(), deserializedUsers.Count());

            for (int i = 0; i < users.Count(); i++)
            {
                User expected = users.GetValue(i);
                User actual = deserializedUsers.GetValue(i);

                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.Email, actual.Email);
                Assert.AreEqual(expected.Password, actual.Password);
            }
        }
    }
}
