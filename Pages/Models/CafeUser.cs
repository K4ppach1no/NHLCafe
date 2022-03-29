using System;

namespace NHLCafe.Pages.Models;

public class CafeUser
    {
        public Guid UniqueGuid { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }