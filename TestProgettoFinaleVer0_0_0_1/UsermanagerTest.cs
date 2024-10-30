using Moq;
using ProgettoFinale_ver0_0_0_1.Managers.Implementations;
using Microsoft.Extensions.Configuration;
using esDef.Models;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using ProgettoFinale_ver0_0_0_1.Microsoft.Extensions.Configuration.Wrapper;

namespace TestProgettoFinaleVer0_0_0_1.UserControllerTest
{
    //https://www.youtube.com/watch?v=uvqAGchg8bc
    //https://www.youtube.com/watch?v=9ZvDBSQa_so
    public class UsermanagerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new(MockBehavior.Strict);
        private readonly Mock<IConfiguration> _userConfigurationMock = new(MockBehavior.Strict);
        private readonly Mock<IWrapperConfiguration> _wrapperUserConfigurationMock = new(MockBehavior.Strict);

       /* public UsermanagerTest(IConfigurationWrapper configurationWrapper)
        {
            _wrapperUserConfigurationMock = configurationWrapper;
        }*/


        [Test]
        public async Task GetUserSuccess()
        {
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new SimpleUser();
            simpleUser.Email = "boh";
            simpleUser.Password = "ciao";
            User expectedUser = new User("boh", "ciao"); // Dati utente attesi
            
            _userRepositoryMock.Setup(m => m.GetUser(simpleUser)).ReturnsAsync(expectedUser);

            var result = await _umt.GetUser(simpleUser);

            // Assert
            Assert.That(result, Is.EqualTo(expectedUser));

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.GetUser(simpleUser), Times.Once);
        }


        
        [Test]
        public void GetUserFailEmailMissing()
        {
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser0 = new SimpleUser();
            simpleUser0.Email = "";
            simpleUser0.Password = "ciao";
            User expectedUser = new User("boh", "ciao"); // Dati utente attesi


            // Assert
            async Task Act0()
            {
                await _umt.GetUser(simpleUser0);
            }

            Assert.ThrowsAsync<Exception>(Act0);

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.GetUser(simpleUser0), Times.Never);
        }




        [Test]
        public void GetUserFailPasswordMissing()
        {
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser0 = new SimpleUser();
            simpleUser0.Email = "boh";
            simpleUser0.Password = "";
            User expectedUser = new User("boh", "ciao"); // Dati utente attesi


            // Assert
            async Task Act0()
            {
                await _umt.GetUser(simpleUser0);
            }


            Assert.ThrowsAsync<Exception>(Act0);

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.GetUser(simpleUser0), Times.Never);
        }



        [Test]
        public void GetUserAccountDoesNotExist() {
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new SimpleUser();
            simpleUser.Email = "boh";
            simpleUser.Password = "ciao";
            User expectedUser = null; // Dati utente attesi

            _userRepositoryMock.Setup(m => m.GetUser(simpleUser)).ReturnsAsync(expectedUser);

            async Task Act() => await _umt.GetUser(simpleUser);

            // Assert
            Assert.ThrowsAsync<Exception>(Act);

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.GetUser(simpleUser), Times.Once);
        }




        [Test]
        public void EmailUsedNoSuchEmailInTheDatabase()
        {
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new SimpleUser();
            simpleUser.Email = "boh";
            simpleUser.Password = "ciao";
            User expectedUser = null; // Dati utente attesi

            _userRepositoryMock.Setup(m => m.FindUserWithEmail(simpleUser)).ReturnsAsync(expectedUser);

            async Task Act()
            {
                await _umt.EmailUsed(simpleUser);
            }

            // Assert
            Assert.DoesNotThrowAsync(Act);

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.FindUserWithEmail(simpleUser), Times.Once);
        }



        [Test]
        public void EmailUsedEmailAlreadyInUse()
        {
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new SimpleUser();
            simpleUser.Email = "boh";
            simpleUser.Password = "ciao";
            User expectedUser = new User("", ""); // Dati utente attesi

            _userRepositoryMock.Setup(m => m.FindUserWithEmail(simpleUser)).ReturnsAsync(expectedUser);

            async Task Act()
            {
                await _umt.EmailUsed(simpleUser);
            }

            // Assert
            Assert.ThrowsAsync<Exception>(Act);

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.FindUserWithEmail(simpleUser), Times.Once);
        }





        [Test]
        public void EmailUsedEmailMissing()
        {
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser0 = new SimpleUser();
            simpleUser0.Email = "";
            simpleUser0.Password = "ciao";


            // Assert
            async Task Act()
            {
                await _umt.EmailUsed(simpleUser0);
            }

            Assert.ThrowsAsync<Exception>(Act);

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.FindUserWithEmail(simpleUser0), Times.Never);
        }



        [Test]
        public void EmailUsedPasswordMissing()
        {
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser0 = new SimpleUser();
            simpleUser0.Email = "boh";
            simpleUser0.Password = "";


            // Assert
            async Task Act()
            {
                await _umt.EmailUsed(simpleUser0);
            }

            Assert.ThrowsAsync<Exception>(Act);

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.FindUserWithEmail(simpleUser0), Times.Never);
        }

        //Login

        
        

        
        [Test]
        public async Task LoginSuccess()
        {
            var ExpectedTokenOption = new TokenOption
            {
                Secret = "non sapevo cosa mettere ma serve renderla piu' lunga e quindi questo sto facendo, o no?",
                ExpiryDays = 7,
                Issuer = "ServerProva",
                Audience = "API"
            };

            
            var _userConfigurationSectionMock = new Mock<IConfigurationSection>(MockBehavior.Strict);
            _userConfigurationSectionMock.Setup(x => x.Key).Returns("tokenoptions");
            _userConfigurationSectionMock.Setup(m => m.Value).Returns(JsonSerializer.Serialize(ExpectedTokenOption));
            _userConfigurationSectionMock.Setup(m => m.Path).Returns("tokenoptions");
            _userConfigurationSectionMock.Setup(m => m.GetChildren()).Returns(Enumerable.Empty<IConfigurationSection>());

            _userConfigurationMock.Setup(m => m.GetSection("TokenOptions")).Returns(_userConfigurationSectionMock.Object);


            _wrapperUserConfigurationMock.Setup(m => m.GetTokenOption("TokenOptions")).Returns(ExpectedTokenOption);

            //setup get
            UserManager _userManagerMock = new (_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser0 = new();
            simpleUser0.Email = "boh";
            simpleUser0.Password = "ciao";
            User u = new (simpleUser0);

            _userRepositoryMock.Setup(m => m.GetUser(simpleUser0)).ReturnsAsync(u);
            
            
            //var tokenOptions = csm.Object.GetSection("TokenOptions").Get<TokenOption>();
            //csm.Setup(m => m.Get<TokenOption>()).Returns(to);

            //enc.Setup(m => m.GetBytes("")).Returns(prova);


            string token = await _userManagerMock.Login(simpleUser0);

            Assert.AreEqual(token, "");

        }
        




        //register
    }

    /*
    public interface IConfigurationWrapper
    {
        IConfigurationSection GetSectionValue(string sectionName);
    }

    public class ConfigurationWrapper : IConfigurationWrapper
    {
        private readonly IConfiguration _configuration;

        public ConfigurationWrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfigurationSection GetSectionValue(string sectionName)
        {
            return _configuration.GetSection(sectionName);
        }
    }*/

}