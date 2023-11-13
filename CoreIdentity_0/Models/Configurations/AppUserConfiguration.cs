using CoreIdentity_0.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreIdentity_0.Models.Configurations
{
    public class AppUserConfiguration : BaseConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);
            //AppUser'a hem IdentityUser'dan gelen Id hem de IEntity'den gelen ID property'leri var...Ancak SQL tarafı bunları aynı isim olarak görüp (incasesensitivity) hata verecektir... 
            builder.Ignore(x => x.ID); //Kendi ID'mizi ignore ediyoruz cünkü Identity'nin bütün sistemlerini kullanabilmek icin onun Id'sini korumak zorundayız...
            builder.HasMany(x => x.UserRoles).WithOne(x => x.User).HasForeignKey(x => x.UserId).IsRequired();
            builder.HasMany(x => x.Orders).WithOne(x => x.AppUser).HasForeignKey(x => x.AppUserID);
            builder.HasOne(x => x.Profile).WithOne(x => x.AppUser).HasForeignKey<AppUserProfile>(x => x.ID);
        }
    }
}
