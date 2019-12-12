using System;
using System.Threading.Tasks;
using AlsacWebApiCore.Exceptions;
using ConstituentSearch.DTOs;
using ConstituentSearch.Repositories;
using Microsoft.Extensions.Logging;

namespace ConstituentSearch.Services
{
    public class SearchService : ISearchService
    {
        private readonly ILogger _logger;
        private readonly ISearchRepository _repository;

        public SearchService(ILogger<SearchService> logger, ISearchRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<SearchResponseDto> FindMatchesAsync(SearchRequestDto searchData)
        {
            try
            {
                return await _repository.FindMatchesAsync(searchData);
            }
            catch (Exception inner)
            {
                var ex = new DatabaseReadException("Error during lookup", inner);
                _logger.LogCritical(ex.Message, ex);
                throw ex;
            }
        }
    }
}
