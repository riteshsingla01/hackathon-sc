using System.ComponentModel.DataAnnotations;

namespace ConstituentSearch.DTOs
{
    public class SearchResponseItemDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ConstituentId { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        [MaxLength(2)]
        public string State { get; set; }
        /// <summary>
        /// Gets or sets Zip code used for look up
        /// </summary>
        [MaxLength(9)]
        [MinLength(5)]
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
