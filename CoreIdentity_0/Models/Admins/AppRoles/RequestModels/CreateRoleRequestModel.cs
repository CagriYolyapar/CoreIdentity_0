using System.ComponentModel.DataAnnotations;

namespace CoreIdentity_0.Models.Admins.AppRoles.RequestModels
{
    public class CreateRoleRequestModel
    {
        [Required(ErrorMessage = "Rol ismi gereklidir")]
        public string RoleName { get; set; }

    }
}
