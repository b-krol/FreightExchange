using System;
using System.Diagnostics;

namespace Domain.User
{
    public class User
    {
        public const int NameMaxLength = 64;
        public const int EmailMaxLength = 100;
        public const int PasswordMinLength = 8;
        public const int PasswordMaxLength = 100;
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        private User()
        {
            //required by ORM
        }
        public User(string name, string password, string email)
        {
            //name validation
            ThrowIfNullOrEmpty(name);
            //email validation
            ThrowIfNullOrEmpty(email);
            ThrowIfEmailIsIncorrect(email);
            //password validation
            ThrowIfNullOrEmpty(password);
            ThrowIfDoesNotFitLengthRestrictions(password, PasswordMaxLength, PasswordMinLength);

            Name = name.Trim();
            Email = email.Trim();
            Password = password.Trim();//TODO decide - Trim() the given password or rejected passwords with white spaces
        }

        public void SetName(string name)
        {
            ThrowIfNullOrEmpty(name);
            Name = name.Trim();
        }

        public void SetEmail(string email)
        {
            ThrowIfNullOrEmpty(email);
            ThrowIfEmailIsIncorrect(email);
            Email = email.Trim();
        }

        private void ThrowIfDoesNotFitLengthRestrictions(string value, int maxLength, int minLength = 3)
        {
            int valueLength = value.Length;
            if (valueLength > maxLength) throw new ArgumentException($"{nameof(value)} cannot exceed length of {maxLength} characters");
            if (valueLength < minLength) throw new ArgumentException($"{nameof(value)} must reach at least length of {minLength} characters");
        }

        private void ThrowIfEmailIsIncorrect(string email)
        {
            if (!email.Contains("@")) throw new ArgumentException("Email has to cointain @ symbol");
        }

        private void ThrowIfNullOrEmpty(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) throw new ArgumentException();
        }

    }
}
