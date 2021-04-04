using System;
using System.Linq;
using API.DTO;
using API.DTO.Link;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Link;

namespace API.Controllers {

    [ApiController]
    [Route("api/{v:apiVersion}/link")]
    public class LinkController : ControllerBase {

        private readonly ILinkService _service;

        public LinkController(ILinkService service) {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateLink([FromBody] CreateLinkRequestDto body) {
            var validity = body.Validate();
            if (validity != null) return BadRequest(validity);

            // Authorize user
            long tokenId = int.Parse(User.Claims.First(x => x.Type == "uid").Value);
            if (body.userId != tokenId) return new ObjectResult(new ForbiddenDto()) { StatusCode = 403 };
            
            var link = body.ToDomain();

            try {
                _service.Add(link);
            } catch (ArgumentException e) {
                return NotFound(CreateLinkResponseDtoCreator.AuthorNotFound());
            }

            return Ok(new EmptyOkDto());
        }
    }
}