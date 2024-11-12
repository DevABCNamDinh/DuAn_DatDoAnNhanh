﻿using System.ComponentModel.DataAnnotations;

namespace DuAn_DoAnNhanh.Client.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
       
    }
}
