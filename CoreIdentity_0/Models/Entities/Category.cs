namespace CoreIdentity_0.Models.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

        //Relational Properties
        public virtual ICollection<Product> Products { get; set; }

    }
}
