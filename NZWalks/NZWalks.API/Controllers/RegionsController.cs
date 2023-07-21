using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.RegionRepository;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository,
                                  IMapper mapper,
                                   ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            List<Region> regions = await regionRepository.GetAllAsync();
            logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regions)}");
            return Ok(mapper.Map<List<RegionDto>>(regions));
        }

        // GET SINGLE REGION (Get Region By ID)
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{regionId:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid regionId)
        {
            Region? region = await regionRepository.GetByIdAsync(regionId);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(region));
        }

        // POST To Create New Region
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            Region regionDomainModel = await regionRepository.AddAsync(mapper.Map<Region>(addRegionRequestDto));
            RegionDto regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { regionId = regionDto.Id }, regionDto);
        }

        // UPDATE Region
        // PUT: https://localhost:portnumber/api/regions/{regionId}
        [HttpPut]
        [Route("{regionId:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid regionId, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            Region? region = await regionRepository.UpdateAsync(regionId, mapper.Map<Region>(updateRegionRequestDto));
            if (region == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(region));
        }

        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{regionId}
        [HttpDelete]
        [Route("{regionId:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid regionId)
        {
            var region = await regionRepository.DeleteAsync(regionId);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(region));
        }
    }
}
