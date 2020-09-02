using System;

namespace ExplosiveMemes.Entities
{
    public class BotUser
    {
        public int Id { get; set; }
        public string ChatId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastMessage { get; set; }
        public DateTime LastBear { get; set; }
    }
}
