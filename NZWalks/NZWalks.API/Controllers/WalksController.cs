using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.WalkRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // Create Walk
        // POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map DTO to Domain Model
            Walk walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);

            // Map Model to DTO
            WalkDto walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        // Get Walks
        // GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walkDomainModel = await walkRepository.GetAllAsync();
            return Ok(mapper.Map<List<WalkDto>>(walkDomainModel));
        }

        // Get Walk By Id
        // GET: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Walk? walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Update Walk By id
        // PUT: /api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map Dto to Doamin Model
            Walk walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            Walk? updatedWalk = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (updatedWalk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(updatedWalk));
        }
    }
}
