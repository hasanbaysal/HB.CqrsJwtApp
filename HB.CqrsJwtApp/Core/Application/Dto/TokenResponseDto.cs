namespace HB.CqrsJwtApp.Core.Application.Dto
{
    public class TokenResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }

    }
}
