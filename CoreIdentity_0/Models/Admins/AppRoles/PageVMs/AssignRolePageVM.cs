using CoreIdentity_0.Models.Admins.AppRoles.ResponseModels;

namespace CoreIdentity_0.Models.Admins.AppRoles.PageVMs
{
    //SharedPVM(Yani hem Request icin hem Response icin kullanılacak)
    public class AssignRolePageVM
    {
        public List<AppRoleResponseModel> Roles { get; set; }
        public int UserID { get; set; }

    }
}
