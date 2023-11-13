using CoreIdentity_0.Models.Enums;
using CoreIdentity_0.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CoreIdentity_0.Models.Entities
{
    //IdentityUser class'ı bir Identity class'ıdır...İndirdigimiz AspNetCore.Identity kütüphanesinden gelir...Ve bu sınıfı default kullanırsanız sınıf tabloya dönüstürülürken edinecegi primary key string tipte olur...Bizim sistemimizde tablolarımızın primary key'i int olduğu icin  biz IdentityUser'in key'inin int olarak tanımlanmasını söylemeliyiz..Ki bu da IdentityUser generic tarafını kullanarak gercekleştirilir...

    //Eger siz Identity kütüphanesindeki tablo yapılarını sekillendirmek istemezseniz hic AppUser class'ı acmadan işlemleri Identity'e bırakabilirsiniz...Kendisi bir AspNetUsers isimli tablo acarak bu işlemi yapacaktır. Ancak bilmelisiniz ki eger özel bir müdahale yapmazsanız tablonun primary key string olacaktır...

    //Bizim burada özellikle AppUser class'ı acma istegimiz Identity tarafındaki yapıları sekillendirmek(kendi istedigimiz özel property'leri koymak, ilişkileri kendi tarafımızdaki class'lar ile  saglamak vs...) istedigimiz icin kaynaklanmıstır... Dolayısıyla biz bu emri verdigimiz zaman Identity kütüphanesi hem kendi yapısını kendi property'leri ile olusturacak hem de bizim istedigimiz yapıları da icerisine eklemiş olacak...
    public class AppUser : IdentityUser<int>, IEntity
    {
        public AppUser()
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
        public virtual ICollection<AppUserRole> UserRoles { get; set; } //Property isminin UserRoles olmasına dikkat ediniz
        public virtual AppUserProfile Profile { get; set; } //Identity'de böyle bir Relational property bulunmadıgı icin isim konusunda kendi standartlarınıza dönebilirsiniz
        public virtual ICollection<Order> Orders { get; set; }




    }
}
