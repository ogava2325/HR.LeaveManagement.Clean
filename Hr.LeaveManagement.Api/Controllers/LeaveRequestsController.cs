using Hr.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using Hr.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using Hr.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using Hr.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using Hr.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using Hr.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using Hr.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hr.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController(IMediator mediator) : ControllerBase
    {
        // GET: api/<LeaveRequestsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestDto>>> Get(bool isLoggedInUser = false)
        {
            var leaveRequests = await mediator.Send(new GetAllLeaveRequestsQuery());
            return Ok(leaveRequests);
        }

        // GET api/<LeaveRequestsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
        {
            var leaveRequest = await mediator.Send(new GetLeaveRequestDetailsQuery { Id = id });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestsController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateLeaveRequestCommand leaveRequest)
        {
            var response = await mediator.Send(leaveRequest);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<LeaveRequestsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveRequestCommand leaveRequest)
        {
            await mediator.Send(leaveRequest);
            return NoContent();
        }

        // PUT api/<LeaveRequestsController>/CancelRequest/
        [HttpPut]
        [Route("CancelRequest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequest)
        {
            await mediator.Send(cancelLeaveRequest);
            return NoContent();
        }

        // PUT api/<LeaveRequestsController>/UpdateApproval/
        [HttpPut]
        [Route("UpdateApproval")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand updateApprovalRequest)
        {
            await mediator.Send(updateApprovalRequest);
            return NoContent();
        }

        // DELETE api/<LeaveRequestsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand { Id = id };
            await mediator.Send(command);
            return NoContent();
        }
    }
}