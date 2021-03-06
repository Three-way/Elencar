﻿using System;

namespace Elencar.Domain.Entities
{
    public class Actor
    {
        public Actor(){}

        public Actor(int id) 
        {
            Id = id;
        }

        public Actor(string bio, decimal fee, User user)
        {
            Bio= bio;
            Fee= fee;
            User= user;
        }

        public Actor(int id, string bio, decimal fee)
        {
            Id = id;
            Bio = bio;
            Fee = fee;
        }

        public Actor(int id, string bio, decimal fee, User user)
        {
            Id = id;
            Bio = bio;
            Fee = fee;
            User = user;
        }

        public int Id { get; private set; }
        public string Bio { get; private set; }
        public decimal Fee { get; private set; }
        public bool Status { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }
        public User User { get; private set; }

    }
}
