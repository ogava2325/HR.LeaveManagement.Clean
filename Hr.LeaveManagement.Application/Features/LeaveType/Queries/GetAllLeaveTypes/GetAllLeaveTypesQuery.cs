using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public record GetAllLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;