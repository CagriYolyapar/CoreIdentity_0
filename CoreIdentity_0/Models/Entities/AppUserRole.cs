using CoreIdentity_0.Models.Enums;
using CoreIdentity_0.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CoreIdentity_0.Models.Entities
{
    //AppUserRole sınıfınıda Relational Property isimlerine cok dikkat ediniz...Bunların hepsi Identity'nin istedigi standartlara uygun verilmiştir...Cok dikkat edin eger bu Relation'lara müdahale edecekseniz bizzat Identity standartlarına uyun...


    //Identity Standartlarında normalde ilişkisel Property User, Role olarak istenir...UserId ve RoleId property'lerine gerek yoktur cünkü onlar zaten miras olarak gelmektedir...Relational property'leri de bu yüzden User ve Role olarak veririz...
    public class AppUserRole : IdentityUserRole<int>, IEntity
    {
        public AppUserRole()
        {
            CreatedDate = DateTime.UtcNow;
            Status = DataStatus.Inserted;
        }
     

        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus Status { get; set; }

        //Relational Properties
        public virtual AppUser User { get; set; } //Propert isminin User olduguna dikkat edin...Cünkü UserId property'sinden kaynaklanmaktadır
        public virtual AppRole Role { get; set; }

    }
}
