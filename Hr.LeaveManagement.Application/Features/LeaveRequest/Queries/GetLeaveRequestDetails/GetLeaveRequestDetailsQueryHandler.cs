using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler(
    IMapper mapper,
    ILeaveRequestRepository leaveRequestRepository)
    : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var leaveRequestEntity = await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);

        return mapper.Map<LeaveRequestDetailsDto>(leaveRequestEntity);
    }
}