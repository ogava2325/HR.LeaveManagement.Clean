using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class GetAllLeaveRequestsQuery : IRequest<List<LeaveRequestDto>>
{
}