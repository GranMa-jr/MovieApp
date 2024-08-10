namespace MovieApp.Core.Entities
{
    public class EntityBase : IEntityBase
    {
        public virtual int Id { get; set; }
        public virtual string CreatedBy { get; set; } = "Undefined";
        public virtual string? ModifiedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual DateTime? ModifiedDate { get; set; }
    }
}