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
        public void UserCannotBeCreatedWithNullName()
        {
            Assert.Throws<ArgumentException>(() => new User.User(null!, "xyz@domain.com"));
        }

        [Test]
        public void UserCannotBeCreatedWithEmptyName()
        {
            Assert.Throws<ArgumentException>(() => new User.User(string.Empty, "xyz@domain.com"));
        }

        [Test]
        public void UserCannotBeCreatedWithNameMadeOfSpacesOnly()
        {
            Assert.Throws<ArgumentException>(() => new User.User("    ", "xyz@domain.com"));
        }

        [Test]
        public void UserCannotBeCreatedWithNullEmail()
        {
            Assert.Throws<ArgumentException>(() => new User.User("Name", null!));
        }

        [Test]
        public void UserCannotBeCreatedWithEmptyEmail()
        {
            Assert.Throws<ArgumentException>(() => new User.User("Name", String.Empty));
        }

        [Test]
        public void UserCannotBeCreatedWithEmailMadeOfSpacesOnly()
        {
            Assert.Throws<ArgumentException>(() => new User.User("Name", "        "));
        }

        [Test]
        public void UserCannotBeCreatedWithoutContainingMonkeySymbolInEmail()
        {
            Assert.Throws<ArgumentException>(() => new User.User("Name", "xyz#domain.com"));
        }

        //[Test]
        //public void UserCannotBeCreatedWithoutContainingDotInEmail()
        //{
        //    Assert.Throws<ArgumentException>(() => new User.User("Name", "xyz@domain,com"));
        //}

        //[Test]
        //public void UserCannotBeCreatedWithoutContainingDotAfterMonkeySymbolInEmail()
        //{
        //    Assert.Throws<ArgumentException>(() => new User.User("Name", "xyz.domain@com"));
        //}

        //[Test]
        //public void UserCannotBeCreatedWithoutContainingDotAfterMonkeySymbolWithAtLeastOneCharacterBetweenThemInEmail()
        //{
        //    Assert.Throws<ArgumentException>(() => new User.User("Name", "xyz@.com"));
        //}

        //[Test]
        //public void UserCannotBeCreatedWithoutContainingDotAfterMonkeySymbolWithAtLeastOneCharacterNotBeingSpaceBetweenThemInEmail()
        //{
        //    Assert.Throws<ArgumentException>(() => new User.User("Name", "xyz@  .com"));
        //}

        //[Test]
        //public void UserCannotBeCreatedWithoutContainingDotAfterMonkeySymbolWithAtLeastOneCharacterBeingLetterBetweenThemInEmail()
        //{
        //    throw new NotImplementedException("Test not implemented yet");
        //}

        //[Test]
        //public void UserCannotBeCreatedWithContainingAnySpaceInEmail()
        //{
        //    Assert.Throws<ArgumentException>(() => new User.User("Name", "xyz @domain.com"));
        //}

        [Test]
        public void UserCanBeCreatedWithCorrectData()
        {
            var createdUser = new User.User("Normal name", "xyz@domain.com");
            Assert.That(createdUser.Name, Is.EqualTo("Normal name"));
            Assert.That(createdUser.Email, Is.EqualTo("xyz@domain.com"));
        }

        [Test]
        public void UserCanBeCreatedWithCorrectDataWithNameContainingWhiteSpacesInConstructorAndWithoutThemInNameProperty()
        {
            var createdUser = new User.User("  Normal name ", "xyz@domain.com");
            Assert.That(createdUser.Name, Is.EqualTo("Normal name"));
            Assert.That(createdUser.Email, Is.EqualTo("xyz@domain.com"));
        }

        [Test]
        public void UserCanBeCreatedWithCorrectDataWithEmailContainingWhiteSpacesInConstructorAndWithoutThemInEmailProperty()
        {
            var createdUser = new User.User("Normal name", "  xyz@domain.com ");
            Assert.That(createdUser.Name, Is.EqualTo("Normal name"));
            Assert.That(createdUser.Email, Is.EqualTo("xyz@domain.com"));
        }
    }
}