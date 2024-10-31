namespace ProgettoFinale_ver0_0_0_1.Models.TokenOptions
{
    public class TokenOption
    {
        public string Secret { get; set; }
        public int ExpiryDays { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
