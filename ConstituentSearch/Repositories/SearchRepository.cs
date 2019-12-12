using System.Collections.Generic;
using System.Threading.Tasks;
using AlsacWebApiCore.Infrastructure;
using Dapper;
using ConstituentSearch.DTOs;

namespace ConstituentSearch.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IConnectionFactory _connection;

        public SearchRepository(IConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<SearchResponseDto> FindMatchesAsync(SearchRequestDto searchData)
        {
            var item1 = new SearchResponseItemDto()
            {
                FirstName = "Kenny",
                MiddleName = "Alan",
                LastName = "Long",
                ConstituentId = "DEB850E7-4ACC-4BA5-9282-83E268EA03FA",
                Street1 = "2017 Cowden Ave.",
                Street2 = "",
                City = "Memphis",
                State = "TN",
                Zip = "38104",
                PhoneNumber = "901-463-7727",
                Email = "kenny.long@stjude.org"
            };

            var item2 = new SearchResponseItemDto()
            {
                FirstName = "Sharon",
                MiddleName = "Andrews",
                LastName = "Long",
                ConstituentId = "1BB7022F-6A5D-4B26-9027-36B3C74DE290",
                Street1 = "2017 Cowden Ave.",
                Street2 = "Suite 105",
                City = "Memphis",
                State = "TN",
                Zip = "38104",
                PhoneNumber = "901-414-6323",
                Email = "sharon.long@gmail.com"
            };

            var response = new SearchResponseDto
            {
                Matches = new List<SearchResponseItemDto>()
            };
            response.Matches.Add(item1);
            response.Matches.Add(item2);

            return response;
        }
    }
}
