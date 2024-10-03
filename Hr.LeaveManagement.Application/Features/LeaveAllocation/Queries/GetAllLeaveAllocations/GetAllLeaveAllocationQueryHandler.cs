using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public class GetAllLeaveAllocationQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    : IRequestHandler<GetAllLeaveAllocationsQuery, List<LeaveAllocationDto>>
{
    public async Task<List<LeaveAllocationDto>> Handle(GetAllLeaveAllocationsQuery request,
        CancellationToken cancellationToken)
    {
        var leaveAllocations = await leaveAllocationRepository.GetLeaveAllocationsWithDetails();

        return mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
    }
}