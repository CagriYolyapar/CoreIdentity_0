using System.ComponentModel.DataAnnotations;

namespace CoreIdentity_0.Models.Admins.AppUsers.RequestModels
{
    public class CreateUserRequestModel
    {
        [Required(ErrorMessage ="Kullanıcı ismi alanı girilmesi zorunludur")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage ="Email formatında giriş yapınız")]
        public string Email { get; set; }

    }
}
