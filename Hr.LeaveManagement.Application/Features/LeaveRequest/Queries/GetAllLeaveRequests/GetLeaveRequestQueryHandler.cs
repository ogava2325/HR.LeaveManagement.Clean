using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class GetLeaveRequestQueryHandler(
    IMapper mapper,
    ILeaveRequestRepository leaveRequestRepository)
    : IRequestHandler<GetAllLeaveRequestsQuery, List<LeaveRequestDto>>
{
    public async Task<List<LeaveRequestDto>> Handle(GetAllLeaveRequestsQuery request,
        CancellationToken cancellationToken)
    {
        var leaveRequests = await leaveRequestRepository.GetLeaveRequestsWithDetails();

        return mapper.Map<List<LeaveRequestDto>>(leaveRequests);
    }
}