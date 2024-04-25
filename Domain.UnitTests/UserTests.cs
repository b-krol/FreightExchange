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
            Assert.Throws<ArgumentException>(() => new User.User(name!, "P@ssw0rd", "xyz@domain.com"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void UserCannotBeCreatedWithEmptyPassword(string? password)
        {
            Assert.Throws<ArgumentException>(() => new User.User("Name", password!, "xyz@domain.com"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void UserCannotBeCreatedWithEmptyEmail(string? email)
        {
            Assert.Throws<ArgumentException>(() => new User.User("Name", "P@ssw0rd", email!));
        }

        [Test]
        public void UserCannotBeCreatedWithoutContainingMonkeySymbolInEmail()
        {
            Assert.Throws<ArgumentException>(() => new User.User("Name", "P@ssw0rd", "xyz#domain.com"));
        }

        [Test]
        [TestCase("Normal name", "P@ssw0rd", "xyz@domain.com")]
        [TestCase("   Normal name ", "P@ssw0rd", "xyz@domain.com")]
        [TestCase("Normal name", "P@ssw0rd", "xyz@domain.com    ")]
        [TestCase("Normal name  ", "P@ssw0rd", "   xyz@domain.com   ")]
        public void UserCanBeCreatedWithCorrectData(string name, string password, string email)
        {
            var createdUser = new User.User(name, "P@ssw0rd", email);
            Assert.That(createdUser.Name, Is.EqualTo(name.Trim()));
            Assert.That(createdUser.Email, Is.EqualTo(email.Trim()));
        }
    }
}