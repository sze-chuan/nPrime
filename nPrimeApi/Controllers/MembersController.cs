using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nPrimeApi.Models;
using nPrimeApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nPrimeApi.Controllers
{
    [Produces("application/json")]
    [Route("api/members")]
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Member>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadAllAsync()
        {
            var members = await _memberService.ReadAllAsync();
            return Ok(members);
        }

        [HttpGet("{memberId}")]
        public async Task<IActionResult> ReadSingleEvent(string memberId)
        {
            var memberObj = await _memberService.ReadSingleAsync(memberId);

            if (memberObj != null)
                return Ok(memberObj);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult CreateAsync([FromBody] Member memberObj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _memberService.CreateAsync(memberObj).Wait();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMember([FromBody] Member memberObj)
        {
            var result = await _memberService.UpdateAsync(memberObj);

            if (result)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("{memberId}")]
        public async Task<IActionResult> DeleteEvent(string memberId)
        {
            var result = await _memberService.DeleteAsync(memberId);

            if (result)
                return Ok(result);
            else
                return NotFound();
        }
    }
}