using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveType;

public record GetAllLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;