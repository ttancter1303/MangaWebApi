﻿namespace MangaWeb.Domain.Models.Users
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Email  { get; set; }
        public string Password  { get; set; }
        public bool RememberMe  { get; set; }
    }
}
