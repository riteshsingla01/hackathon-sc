using System.Threading.Tasks;
using ConstituentSearch.DTOs;

namespace ConstituentSearch.Services
{
    public interface ISearchService
    {
        Task<SearchResponseDto> FindMatchesAsync(SearchRequestDto searchData);
    }
}
