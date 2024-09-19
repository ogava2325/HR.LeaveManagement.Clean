using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveType;

public class GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<GetAllLeaveTypesQuery, List<LeaveTypeDto>>
{
    public async Task<List<LeaveTypeDto>> Handle(GetAllLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var leaveTypes = await leaveTypeRepository.GetAllAsync();

        //convert data objects to DTO
        var data = mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        //return list of DTO object
        return data;
    }
}