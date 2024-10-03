using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQuery : IRequest<LeaveAllocationDetailsDto>
{
    public int Id { get; set; }
}