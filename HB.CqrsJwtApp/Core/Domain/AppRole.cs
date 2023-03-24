namespace HB.CqrsJwtApp.Core.Domain
{
    public class AppRole
    {
        public int Id { get; set; }
        public string Definition { get; set; } = null!;
        public List<AppUser>? AppUsers { get; set; }

        
    }

}

