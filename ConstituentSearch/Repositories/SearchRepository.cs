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
                Street2 = "",
                City = "Memphis",
                State = "TN",
                Zip = "38104",
                PhoneNumber = "901-414-6323",
                Email = "sharon.long@gmail.com"
            };

            var item3 = new SearchResponseItemDto()
            {
                FirstName = "Summer",
                MiddleName = "Renee",
                LastName = "Smith",
                ConstituentId = "7E36F2CD-31C5-4D2D-80ED-F50B8E22C15A",
                Street1 = "2017 Cowden Ave.",
                Street2 = "",
                City = "Memphis",
                State = "TN",
                Zip = "38111",
                PhoneNumber = "901-389-2409",
                Email = "summer.smith@gmail.com"
            };

            var item4 = new SearchResponseItemDto()
            {
                FirstName = "Jodi",
                MiddleName = "Rose",
                LastName = "Smith",
                ConstituentId = "A4638643-B876-4F69-B551-19996E27FC04",
                Street1 = "2017 Cowden Ave.",
                Street2 = "",
                City = "Memphis",
                State = "TN",
                Zip = "38111",
                PhoneNumber = "901-846-4895",
                Email = "jodi.smith@gmail.com"
            };

            var item5 = new SearchResponseItemDto()
            {
                FirstName = "Melissa",
                MiddleName = "June",
                LastName = "Smith",
                ConstituentId = "DC37B468-825D-4F9A-86D2-11F30BA8DCAA",
                Street1 = "2017 Cowden Ave.",
                Street2 = "",
                City = "Memphis",
                State = "TN",
                Zip = "38111",
                PhoneNumber = "901-553-7879",
                Email = "melissa.smith@gmail.com"
            };

            var item6 = new SearchResponseItemDto()
            {
                FirstName = "Leroy",
                MiddleName = "Flapjack",
                LastName = "Griddlecakes",
                ConstituentId = "E6F56F1D-7FFB-4D81-94A1-BE2B8B3CEF5C",
                Street1 = "555 Quick St.",
                Street2 = "",
                City = "Memphis",
                State = "TN",
                Zip = "38119",
                PhoneNumber = "901-555-4363",
                Email = "leroy@gmail.com"
            };

            var item7 = new SearchResponseItemDto
            {
                FirstName = "Billy",
                MiddleName = "Brooks",
                LastName = "Greene",
                ConstituentId = "B35CEBFD-867B-4272-B72F-5B091C9CE3BE",
                Street1 = "1257 Oakridge Rd.",
                Street2 = "",
                City = "Memphis",
                State = "TN",
                Zip = "38108",
                PhoneNumber = "901-634-2849",
                Email = "billy@gmail.com"
            };

            var inMemoryData = new SearchResponseDto { Matches = new List<SearchResponseItemDto>() };
            inMemoryData.Matches.Add(item1);
            inMemoryData.Matches.Add(item2);
            inMemoryData.Matches.Add(item3);
            inMemoryData.Matches.Add(item4);
            inMemoryData.Matches.Add(item5);
            inMemoryData.Matches.Add(item6);
            inMemoryData.Matches.Add(item7);

            var matchingRows = new SearchResponseDto { Matches = new List<SearchResponseItemDto>() };

            foreach (var row in inMemoryData.Matches)
            {
                if (!string.IsNullOrEmpty(searchData.FirstName) && !string.IsNullOrEmpty(row.FirstName))
                {
                    if (!row.FirstName.StartsWith(searchData.FirstName.Trim()))
                        break;
                }

                if (!string.IsNullOrEmpty(searchData.MiddleName) && !string.IsNullOrEmpty(row.MiddleName))
                {
                    if (!row.MiddleName.StartsWith(searchData.MiddleName.Trim()))
                        break;
                }

                if (!string.IsNullOrEmpty(searchData.LastName) && !string.IsNullOrEmpty(row.LastName))
                {
                    if (!row.LastName.StartsWith(searchData.LastName.Trim()))
                        break;
                }

                if (!string.IsNullOrEmpty(searchData.Street1) && !string.IsNullOrEmpty(row.Street1))
                {
                    if (!row.Street1.StartsWith(searchData.Street1.Trim()))
                        break;
                }

                if (!string.IsNullOrEmpty(searchData.Street2) && !string.IsNullOrEmpty(row.Street2))
                {
                    if (!row.Street2.StartsWith(searchData.Street2.Trim()))
                        break;
                }

                if (!string.IsNullOrEmpty(searchData.City) && !string.IsNullOrEmpty(row.City))
                {
                    if (!row.City.StartsWith(searchData.City.Trim()))
                        break;
                }

                if (!string.IsNullOrEmpty(searchData.State) && !string.IsNullOrEmpty(row.State))
                {
                    if (!row.State.StartsWith(searchData.State.Trim()))
                        break;
                }

                if (!string.IsNullOrEmpty(searchData.Zip) && !string.IsNullOrEmpty(row.Zip))
                {
                    if (!row.Zip.StartsWith(searchData.Zip.Trim()))
                        break;
                }

                if (!string.IsNullOrEmpty(searchData.PhoneNumber) && !string.IsNullOrEmpty(row.PhoneNumber))
                {
                    if (!row.PhoneNumber.StartsWith(searchData.PhoneNumber.Trim()))
                        break;
                }

                if (!string.IsNullOrEmpty(searchData.Email) && !string.IsNullOrEmpty(row.Email))
                {
                    if (!row.Email.StartsWith(searchData.Email.Trim()))
                        break;
                }

                if (!string.IsNullOrEmpty(searchData.ConstituentId) && !string.IsNullOrEmpty(row.ConstituentId))
                {
                    if (!row.ConstituentId.StartsWith(searchData.ConstituentId.Trim()))
                        break;
                }

                matchingRows.Matches.Add(row);
            }
            return matchingRows;
        }
    }
}
