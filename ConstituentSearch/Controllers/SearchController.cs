using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConstituentSearch.DTOs;
using ConstituentSearch.Services;

namespace ConstituentSearch.Controllers
{
    [ApiController]
    [Route("v1/search")]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        /// <summary>
        /// Find matches
        /// </summary>
        /// <remarks>
        /// Given search data, return matching constituents
        /// </remarks>
        /// <returns>The SearchReponseDto data matching zipCode passed in</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SearchResponseDto>> FindMatches([FromBody] SearchRequestDto searchData)
        {
            var result = await _searchService.FindMatchesAsync(searchData);

            if (result == null)
                return NotFound("No matches found");

            return Ok(result);
        }
    }
}
