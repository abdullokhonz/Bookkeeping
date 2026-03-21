using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;

namespace Bookkeeping.Contracts.Interfaces.Components
{
    public interface ICategoryAccount5dComponent
    {
        string Name { get; }

        void AddSub(CategoryAccount5dTreeDto child);

        void RemoveSub(CategoryAccount5dTreeDto child);

        IEnumerable<CategoryAccount5dTreeDto> GetChildren();
    }
}
