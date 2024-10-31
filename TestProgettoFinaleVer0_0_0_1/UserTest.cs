using Moq;
using ProgettoFinale_ver0_0_0_1.Managers.Implementations.Users;
using Microsoft.Extensions.Configuration;
using ProgettoFinale_ver0_0_0_1.Models.Users;
using ProgettoFinale_ver0_0_0_1.Models.TokenOptions;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces.Users;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using ProgettoFinale_ver0_0_0_1.Microsoft.Extensions.Configuration.Wrapper;



namespace TestProgettoFinaleVer0_0_0_1.Tests.Users
{
    //https://www.youtube.com/watch?v=uvqAGchg8bc
    //https://www.youtube.com/watch?v=9ZvDBSQa_so
    internal class UserTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new(MockBehavior.Strict);
        private readonly Mock<IConfiguration> _userConfigurationMock = new(MockBehavior.Strict);
        private readonly Mock<IWrapperConfiguration> _wrapperUserConfigurationMock = new(MockBehavior.Strict);

        [Test]
        public async Task GetUserSuccess()
        {
            UserManager _umt = new (_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "boh",
                Password = "ciao"
            };
            User expectedUser = new ("boh", "ciao"); // Dati utente attesi

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
            UserManager _umt = new (_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "",
                Password = "ciao"
            };

            // Assert
            async Task Act0()
            {
                await _umt.GetUser(simpleUser);
            }

            Assert.ThrowsAsync<Exception>(Act0);

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.GetUser(simpleUser), Times.Never);
        }







        [Test]
        public void GetUserFailPasswordMissing()
        {
            UserManager _umt = new (_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "boh",
                Password = ""
            };

            // Assert
            async Task Act0()
            {
                await _umt.GetUser(simpleUser);
            }

            Assert.ThrowsAsync<Exception>(Act0);

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.GetUser(simpleUser), Times.Never);
        }





        [Test]
        public void GetUserAccountDoesNotExist()
        {
            UserManager _umt = new (_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "boh",
                Password = "ciao"
            };

#pragma warning disable CS8603
            _userRepositoryMock.Setup(m => m.GetUser(simpleUser)).ReturnsAsync(() => null);
#pragma warning restore CS8603

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
            UserManager _userManagerMock = new (_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "boh",
                Password = "ciao"
            };

#pragma warning disable CS8603
            _userRepositoryMock.Setup(m => m.FindUserWithEmail(simpleUser)).ReturnsAsync(() => null);
#pragma warning restore CS8603

            async Task Act()
            {
                await _userManagerMock.EmailUsed(simpleUser);
            }

            // Assert
            Assert.DoesNotThrowAsync(Act);

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.FindUserWithEmail(simpleUser), Times.Once);
        }





        [Test]
        public void EmailUsedEmailAlreadyInUse()
        {
            UserManager _umt = new (_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "boh",
                Password = "ciao"
            };
            
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
            UserManager _umt = new (_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "",
                Password = "ciao"
            };

            // Assert
            async Task Act()
            {
                await _umt.EmailUsed(simpleUser);
            }

            Assert.ThrowsAsync<Exception>(Act);

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.FindUserWithEmail(simpleUser), Times.Never);
        }





        [Test]
        public void EmailUsedPasswordMissing()
        {
            UserManager _umt = new (_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "boh",
                Password = ""
            };

            // Assert
            async Task Act()
            {
                await _umt.EmailUsed(simpleUser);
            }

            Assert.ThrowsAsync<Exception>(Act);

            // Verifica che i metodi siano stati chiamati
            _userRepositoryMock.Verify(m => m.FindUserWithEmail(simpleUser), Times.Never);
        }




        //Login




        [Test]
        public void LoginFailNoSuchAccount()
        {
            UserManager _userManagerMock = new (_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "boh",
                Password = "ciao"
            };

#pragma warning disable CS8603
            _userRepositoryMock.Setup(m => m.GetUser(simpleUser)).ReturnsAsync(() => null);
#pragma warning restore CS8603

            Assert.ThrowsAsync<Exception>(
                async () => await _userManagerMock.Login(simpleUser));
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
            _userConfigurationSectionMock.Setup(x => x.Key).Returns("tokenoptions");
            _userConfigurationSectionMock.Setup(m => m.Value).Returns(JsonSerializer.Serialize(ExpectedTokenOption));
            _userConfigurationSectionMock.Setup(m => m.Path).Returns("tokenoptions");
            _userConfigurationSectionMock.Setup(m => m.GetChildren()).Returns(Enumerable.Empty<IConfigurationSection>());

            _userConfigurationMock.Setup(m => m.GetSection("TokenOptions")).Returns(_userConfigurationSectionMock.Object);

            _wrapperUserConfigurationMock.Setup(m => m.GetTokenOption("TokenOptions")).Returns(ExpectedTokenOption);

            UserManager _userManagerMock = new(_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "boh",
                Password = "ciao"
            };
            User u = new(simpleUser);

            _userRepositoryMock.Setup(m => m.GetUser(simpleUser)).ReturnsAsync(u);

            string token = await _userManagerMock.Login(simpleUser);

            Assert.IsFalse(string.IsNullOrEmpty(token));
        }



        //tokenGeneration

        [Test]
        public void TokenGenerationTokenOptionsNull()
        {
            TokenOption ExpectedTokenOption = new ();

            var _userConfigurationSectionMock = new Mock<IConfigurationSection>(MockBehavior.Strict);
            _userConfigurationSectionMock.Setup(x => x.Key).Returns("tokenoptions");
            _userConfigurationSectionMock.Setup(m => m.Value).Returns(JsonSerializer.Serialize(ExpectedTokenOption));
            _userConfigurationSectionMock.Setup(m => m.Path).Returns("tokenoptions");
            _userConfigurationSectionMock.Setup(m => m.GetChildren()).Returns(Enumerable.Empty<IConfigurationSection>());

            _userConfigurationMock.Setup(m => m.GetSection("TokenOptions")).Returns(_userConfigurationSectionMock.Object);

#pragma warning disable CS8603
            _wrapperUserConfigurationMock.Setup(m => m.GetTokenOption("TokenOptions")).Returns(() => null);
#pragma warning restore CS8603

            UserManager _userManagerMock = new(_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "boh",
                Password = "ciao"
            };
            User u = new(simpleUser);

            _userRepositoryMock.Setup(m => m.GetUser(simpleUser)).ReturnsAsync(u);

            Assert.ThrowsAsync<Exception>(async () => await _userManagerMock.Login(simpleUser));
        }




        //register

        [Test]
        public async Task RegisterSuccessfull()
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
                        
            UserManager _userManagerMock = new(_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "boh",
                Password = "ciao"
            };
            User u = new(simpleUser);

            _userRepositoryMock.Setup(m => m.GetUser(simpleUser)).ReturnsAsync(u);
#pragma warning disable CS8603
            _userRepositoryMock.Setup(m => m.FindUserWithEmail(simpleUser)).ReturnsAsync(() => null);
#pragma warning restore CS8603
            _userRepositoryMock.Setup(m => m.Register(simpleUser)).Returns(Task.CompletedTask);

            string token = await _userManagerMock.Login(simpleUser);

            Assert.IsFalse(string.IsNullOrEmpty(token));
        }



        [Test]
        public void RegisterEmailUsed()
        {
            UserManager _userManagerMock = new(_userConfigurationMock.Object, _userRepositoryMock.Object, _wrapperUserConfigurationMock.Object);
            SimpleUser simpleUser = new()
            {
                Email = "boh",
                Password = "ciao"
            };
            User u = new(simpleUser);

            _userRepositoryMock.Setup(m => m.GetUser(simpleUser)).ReturnsAsync(u);
            _userRepositoryMock.Setup(m => m.FindUserWithEmail(simpleUser)).ReturnsAsync(u);
            _userRepositoryMock.Setup(m => m.Register(simpleUser)).Returns(Task.CompletedTask);

            Assert.ThrowsAsync<Exception>(async () => await _userManagerMock.Register(simpleUser));
        }
    }
}
