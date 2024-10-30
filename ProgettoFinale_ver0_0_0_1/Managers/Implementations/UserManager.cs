using esDef.Models;
using Microsoft.IdentityModel.Tokens;
using ProgettoFinale_ver0_0_0_1.Managers.Interfaces;
using ProgettoFinale_ver0_0_0_1.Microsoft.Extensions.Configuration.Wrapper;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ProgettoFinale_ver0_0_0_1.Microsoft.Extensions.Configuration.Wrapper
{
    public interface IWrapperConfiguration
    {
        TokenOption GetTokenOption(string key);
    }

    public class WrapperConfiguration : IWrapperConfiguration
    {
        private readonly IConfiguration _configuration;
        public WrapperConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenOption GetTokenOption(string key)
        {
            return _configuration.GetSection(key).Get<TokenOption>();
        }
    }
}



namespace ProgettoFinale_ver0_0_0_1.Managers.Implementations
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IWrapperConfiguration _wrapperConfiguration;


        public UserManager(IConfiguration configuration, IUserRepository userRepository, IWrapperConfiguration wrapperConfiguration)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _wrapperConfiguration = wrapperConfiguration;
        }



        public async Task<User> GetUser(SimpleUser s)
        {
            if ((string.IsNullOrEmpty(s.Email) || string.IsNullOrEmpty(s.Password))
                || (string.Equals(s.Email, "") || string.Equals(s.Password, "")))
                throw new Exception("non tutti i campi sono stati compilati");
            User check = await _userRepository.GetUser(s);
            if (check == null)
                throw new Exception("account non trovato");
            return check;
        }




        private Task<string> TokenGeneration(User c)
        {
            
            //legge la configurazione di TokenOptions
            var tokenOptions = _wrapperConfiguration.GetTokenOption("TokenOptions");
            //Console.WriteLine(_configuration.GetSection("TokenOptions").Key);
            //prende secret
            var key = Encoding.ASCII.GetBytes(tokenOptions.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //configurazione del JWT:
                //utente
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.Name, c.Id.ToString())
                    }),
                //Issuer: colui che ha creato il token
                Issuer = tokenOptions.Issuer,
                //Audience: chi utilizzera questo token, cioè quali sono server e API 
                Audience = tokenOptions.Audience,
                //scadenza
                Expires = DateTime.UtcNow.AddDays(tokenOptions.ExpiryDays),
                //algoritmo di generazione della firma, serve per controllare che la token hai creato tu
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //seguente codice è per creare token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return Task.FromResult(tokenHandler.WriteToken(token));
        }



        public async Task EmailUsed(SimpleUser s)
        {
            if (string.IsNullOrEmpty(s.Email) || string.IsNullOrEmpty(s.Password))
                throw new Exception("non tutti i campi sono stati compilati");
            User check = await _userRepository.FindUserWithEmail(s);
            if (check != null)
                throw new Exception("l'email e' gia' in uso");

        }



        public async Task<string> Register(SimpleUser s)
        {
            await EmailUsed(s);
            await _userRepository.Register(s);
            User u = new User(s.Email, s.Password);
            string token = await TokenGeneration(u);
            return token;
        }



        public async Task<string> Login(SimpleUser s)
        {
            User u = await GetUser(s);
            string t = await TokenGeneration(u);
            return t;
        }
    }
}
