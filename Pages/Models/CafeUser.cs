using System;

namespace NHLCafe.Pages.Models;

public class CafeUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; 
    }