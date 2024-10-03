using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var leaveAllocation = await leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
        if (leaveAllocation is null)
        {
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        return mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
    }
}