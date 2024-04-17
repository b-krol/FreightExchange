using Domain.User;

namespace Domain.UnitTests
{
    [TestFixture]
    public class UserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void UserCannotBeCreatedWithEmptyName(string? name)
        {
            Assert.Throws<ArgumentException>(() => new User.User(name!, "xyz@domain.com"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void UserCannotBeCreatedWithEmptyEmail(string? email)
        {
            Assert.Throws<ArgumentException>(() => new User.User("Name", email!));
        }

        [Test]
        public void UserCannotBeCreatedWithoutContainingMonkeySymbolInEmail()
        {
            Assert.Throws<ArgumentException>(() => new User.User("Name", "xyz#domain.com"));
        }

        [Test]
        [TestCase("Normal name", "xyz@domain.com")]
        [TestCase("   Normal name ", "xyz@domain.com")]
        [TestCase("Normal name", "xyz@domain.com    ")]
        [TestCase("Normal name  ", "   xyz@domain.com   ")]
        public void UserCanBeCreatedWithCorrectData(string name, string email)
        {
            var createdUser = new User.User(name, email);
            Assert.That(createdUser.Name, Is.EqualTo(name.Trim()));
            Assert.That(createdUser.Email, Is.EqualTo(email.Trim()));
        }
    }
}