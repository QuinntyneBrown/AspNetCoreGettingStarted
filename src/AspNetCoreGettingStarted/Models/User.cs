﻿namespace AspNetCoreGettingStarted.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Tenant Tenant { get; set; }
    }
}
