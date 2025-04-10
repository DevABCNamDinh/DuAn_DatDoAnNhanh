using System.ComponentModel.DataAnnotations;

namespace DuAn_DoAnNhanh.Client.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        [StringLength(50)]
        public string FullName {  get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập password.")]
        [StringLength(50)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập confirmpassword.")]
        [StringLength(50)]
        public string ConfirmPassword { get; set; }
    }
}
