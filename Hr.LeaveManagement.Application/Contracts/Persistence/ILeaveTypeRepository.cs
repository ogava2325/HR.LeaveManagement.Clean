using Hr.LeaveManagement.Domain.Entities;

namespace Hr.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
    Task<bool> IsUnique(string name);
}