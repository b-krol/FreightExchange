using System;
using System.Diagnostics;

namespace Domain.User
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public User(string name, string email)
        {

            ThrowIfNullOrEmpty(name);
            ThrowIfNullOrEmpty(email);
            ThrowIfEmailIsIncorrect(email);

            Name = name.Trim();
            Email = email.Trim();
        }

        private void ThrowIfEmailIsIncorrect(string email)
        {
            if (!email.Contains("@")) throw new ArgumentException();
        }

        private void ThrowIfNullOrEmpty(string value)
        {
            if (value == null) throw new ArgumentException();
            if (value == string.Empty) throw new ArgumentException();
            if (value.Trim() == string.Empty) throw new ArgumentException();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return Equals((obj as User)!);
        }

        public bool Equals(User other)
        {
            if (Id != other.Id)
                return false;
            if (Name != other.Name)
                return false;
            if (Email != other.Email)
                return false;
            return true;
        }
    }
}
