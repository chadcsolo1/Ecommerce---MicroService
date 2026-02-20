namespace Order.Entities
{
    public abstract class EntityBase
    {
        //protected set is made to use in the derived class
        public int Id { get; protected set; }
        //few audit props
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDate  { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedDate { get; set; }
    }
}
