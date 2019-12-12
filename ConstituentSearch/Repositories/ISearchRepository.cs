using System.Threading.Tasks;
using ConstituentSearch.DTOs;

namespace ConstituentSearch.Repositories
{
    public interface ISearchRepository
    {
        Task<SearchResponseDto> FindMatchesAsync(SearchRequestDto searchData);
    }
}
