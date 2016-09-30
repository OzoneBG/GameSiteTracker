namespace GST.Data.Models.Base.Interfaces
{
    public interface IEntityOwner
    {
        User Author { get; set; }

        string AuthorId { get; set; }
    }
}
