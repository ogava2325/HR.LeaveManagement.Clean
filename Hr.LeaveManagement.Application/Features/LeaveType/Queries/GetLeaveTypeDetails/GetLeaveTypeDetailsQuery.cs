using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public record GetLeaveTypeDetailsQuery(int Id) : IRequest<GetLeaveTypeDetailsQuery>, IRequest<LeaveTypeDetailsDto>;