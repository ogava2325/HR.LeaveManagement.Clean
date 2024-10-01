using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Domain.Entities;
using Hr.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Hr.LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository(HrDatabaseContext context)
    : GenericRepository<LeaveType>(context), ILeaveTypeRepository
{
    private readonly HrDatabaseContext _context = context;

    public async Task<bool> IsUnique(string name)
    {
        return await _context.LeaveTypes.AnyAsync(q => q.Name == name) == false;
    }
}