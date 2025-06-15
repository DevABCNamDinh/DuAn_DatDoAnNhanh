using System.ComponentModel.DataAnnotations;

namespace DuAn_DoAnNhanh.Manage.Models.ViewModel
{
    public class RegisterViewModel
    {
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string ConfirmPassword { get; set; }
    }
}
