using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public record GetAllLeaveAllocationsQuery : IRequest<List<LeaveAllocationDto>>;