using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualAssistant.API.Data.Domain
{
    public class User
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("username")]
        public string? Username { get; set; }

        [Column("firstname")]
        public string? FirstName { get; set; }

        [Column("lastname")]
        public string? LastName { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }
    }
}
