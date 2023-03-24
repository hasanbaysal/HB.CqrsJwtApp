namespace HB.CqrsJwtApp.Infrastructure.Tools
{
    public class JwtTokenDefaults
    {
        /*
         
           ValidAudience = "http//localhost",
                    ValidIssuer = "http//localhost",
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hasanhasanhasan1.")),
                    ValidateIssuerSigningKey=true,
                    ValidateLifetime=true,
         
         */

        public const string ValidAudience = "https//localhost";
        public const string ValidIssuer = "https//localhost";
        public const string Key = "hasanhasanhasan1.";
        public const int Exprie = 5;


    }
}
