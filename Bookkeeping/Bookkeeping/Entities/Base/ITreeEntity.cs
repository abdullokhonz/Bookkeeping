namespace Bookkeeping.Entities.Base
{
    public interface ITreeEntity<T> where T : class
    {
        Guid Id { get; set; }

        Guid? ParentId { get; set; }

        ICollection<T> Children { get; set; }
    }
}
