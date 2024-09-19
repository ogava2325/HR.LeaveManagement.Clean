using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler(Mapper mapper, ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var leaveType = await leaveTypeRepository.GetByIdAsync(request.Id);

        //verify that record exists

        //convert data object to DTO
        var data = mapper.Map<LeaveTypeDetailsDto>(leaveType);

        //return DTO
        return data;
    }
}