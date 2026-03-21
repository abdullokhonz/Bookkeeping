using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;

namespace Bookkeeping.Contracts.Interfaces.Components
{
    public interface IIfrsAccountComponent
    {
        string AccountName { get; }

        void AddSub(IfrsAccountTreeDto child);

        void RemoveSub(IfrsAccountTreeDto child);

        IEnumerable<IfrsAccountTreeDto> GetChildren();
    }
}
