using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.RegionRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data from Database
            List<Region> regions = await _regionRepository.GetAllAsync();

            // return DTOs
            return Ok(_mapper.Map<List<RegionDto>>(regions));
        }

        // GET SINGLE REGION (Get Region By ID)
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{regionId:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid regionId)
        {
            Region? region = await _regionRepository.GetByIdAsync(regionId);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDto>(region));
        }

        // POST To Create New Region
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            Region regionDomainModel = await _regionRepository.AddAsync(_mapper.Map<Region>(addRegionRequestDto));
            RegionDto regionDto = _mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { regionId = regionDto.Id }, regionDto);
        }

        // UPDATE Region
        // PUT: https://localhost:portnumber/api/regions/{regionId}
        [HttpPut]
        [Route("{regionId:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid regionId, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            Region? region = await _regionRepository.UpdateAsync(regionId, _mapper.Map<Region>(updateRegionRequestDto));
            if (region == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionDto>(region));
        }

        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{regionId}
        [HttpDelete]
        [Route("{regionId:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid regionId)
        {
            var region = await _regionRepository.DeleteAsync(regionId);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDto>(region));
        }
    }
}
