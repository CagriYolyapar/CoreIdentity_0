using System.ComponentModel.DataAnnotations;

namespace CoreIdentity_0.Models.AppUsers.RequestModels
{
    public class UserSignInRequestModel
    {
        [Required(ErrorMessage ="Kullanıcı ismi zorunludur")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Sifre alanı zorunludur")]
        public string Password { get; set; }
        public bool RememberMe { get; set;}
        public string? ReturnUrl { get; set; }
    }
}
