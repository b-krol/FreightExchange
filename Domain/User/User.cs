﻿using System;
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
