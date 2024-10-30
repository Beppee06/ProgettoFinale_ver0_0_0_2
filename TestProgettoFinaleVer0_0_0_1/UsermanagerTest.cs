using Moq;
using ProgettoFinale_ver0_0_0_1.Managers.Implementations;
using Microsoft.Extensions.Configuration;
using esDef.Models;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace TestProgettoFinaleVer0_0_0_1.UserControllerTest
{
    //https://www.youtube.com/watch?v=uvqAGchg8bc
    //https://www.youtube.com/watch?v=9ZvDBSQa_so
    public class UsermanagerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new(MockBehavior.Strict);
        private readonly Mock<IConfiguration> _userConfigurationMock = new(MockBehavior.Strict);




        //GetUser


        [Test]
        public async Task GetUserSuccess()
        {
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object); 
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
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object);
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
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object);
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
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object);
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







        //EmailUsed


        [Test]
        public void EmailUsedNoSuchEmailInTheDatabase()
        {
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object);
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
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object);
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
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object);
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
            UserManager _umt = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object);
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
        public async Task LoginFailNoSuchAccount()
        {
            UserManager _userManagerMock = new UserManager(_usCoMock.Object, _usReMock.Object);
            SimpleUser simpleUser0 = new SimpleUser();
            simpleUser0.Email = "boh";
            simpleUser0.Password = "ciao";
            User u = null;

            _usReMock.Setup(m => m.GetUser(simpleUser0)).ReturnsAsync(u);

            async Task Act() => await _userManagerMock.Login(simpleUser0);
            Assert.ThrowsAsync<Exception>(Act);
        }


        
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
            _userConfigurationSectionMock.SetupGet(x => x.Key).Returns("_userConfigurationSectionMock");
            _userConfigurationSectionMock.SetupGet(m => m.Value).Returns(JsonSerializer.Serialize(ExpectedTokenOption));
            _userConfigurationSectionMock.SetupGet(m => m.Path).Returns("TokenOptions");
            _userConfigurationSectionMock.Setup(m => m.GetChildren()).Returns(Enumerable.Empty<IConfigurationSection>());

            _userConfigurationMock.Setup(m => m.GetSection("TokenOptions")).Returns(_userConfigurationSectionMock.Object);
            //_userConfigurationSectionMock.SetupGet(x=> x.).Returns(ExpectedTokenOption);




            var tokenOptions = _userConfigurationMock.Object.GetSection("TokenOptions").Get<TokenOption>();
            Console.Write(_userConfigurationMock);
           // Assert.That(tokenOptions != null);
            
            //var key = Encoding.ASCII.GetBytes(tokenOptions.Secret);


            //setup get
            UserManager _userManagerMock = new UserManager(_userConfigurationMock.Object, _userRepositoryMock.Object);
          
            SimpleUser simpleUser0 = new SimpleUser();
            simpleUser0.Email = "boh";
            simpleUser0.Password = "ciao";
            User u = new User(simpleUser0);

            _userRepositoryMock.Setup(m => m.GetUser(simpleUser0)).ReturnsAsync(u);
            //var tokenOptions = csm.Object.GetSection("TokenOptions").Get<TokenOption>();
            //csm.Setup(m => m.Get<TokenOption>()).Returns(to);
        
            //enc.Setup(m => m.GetBytes("")).Returns(prova);
        
        
            string token = await _userManagerMock.Login(simpleUser0);
        
            Assert.Equals(token, Is.EqualTo(""));
        }
        




        //register

        /*
        [Test]
        public async Task RegisterSuccess()
        {

        }*/




        [Test]
        public async Task RegisterFailMissingFields()
        {
            UserManager _userManagerMock = new UserManager(_usCoMock.Object, _usReMock.Object);
            SimpleUser simpleUser0 = new SimpleUser();
            simpleUser0.Email = "";
            simpleUser0.Password = "ciao";
            User u = new User(simpleUser0);
            SimpleUser simpleUser1 = new SimpleUser();
            simpleUser1.Email = "boh";
            simpleUser1.Password = "";

            async Task Act0() => await _userManagerMock.Register(simpleUser0);
            Assert.ThrowsAsync<Exception>(Act0);

            async Task Act1() => await _userManagerMock.Register(simpleUser0);
            Assert.ThrowsAsync<Exception>(Act1);
        }
    }
}
