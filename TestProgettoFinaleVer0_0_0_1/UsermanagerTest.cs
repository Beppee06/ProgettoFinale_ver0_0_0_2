using Moq;
using ProgettoFinale_ver0_0_0_1.Managers.Implementations;
using Microsoft.Extensions.Configuration;
using esDef.Models;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TestProgettoFinaleVer0_0_0_1.UserControllerTest
{
    //https://www.youtube.com/watch?v=uvqAGchg8bc
    //https://www.youtube.com/watch?v=9ZvDBSQa_so
    public class UsermanagerTest
    {
        private readonly Mock<IUserRepository> _usReMock = new(MockBehavior.Strict);
        private readonly Mock<IConfiguration> _usCoMock = new(MockBehavior.Strict);

        [Test]
        public async Task GetUserSuccess()
        {
            UserManager _umt = new UserManager(_usCoMock.Object, _usReMock.Object); 
            SimpleUser simpleUser = new SimpleUser();
            simpleUser.Email = "boh";
            simpleUser.Password = "ciao";
            User expectedUser = new User("boh", "ciao"); // Dati utente attesi
            
            _usReMock.Setup(m => m.GetUser(simpleUser)).ReturnsAsync(expectedUser);

            var result = await _umt.GetUser(simpleUser);

            // Assert
            Assert.That(result, Is.EqualTo(expectedUser));

            // Verifica che i metodi siano stati chiamati
            _usReMock.Verify(m => m.GetUser(simpleUser), Times.Once);
        }


        
        [Test]
        public void GetUserFailEmailMissing()
        {
            UserManager _umt = new UserManager(_usCoMock.Object, _usReMock.Object);
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
            _usReMock.Verify(m => m.GetUser(simpleUser0), Times.Never);
        }




        [Test]
        public void GetUserFailPasswordMissing()
        {
            UserManager _umt = new UserManager(_usCoMock.Object, _usReMock.Object);
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
            _usReMock.Verify(m => m.GetUser(simpleUser0), Times.Never);
        }



        [Test]
        public void GetUserAccountDoesNotExist() {
            UserManager _umt = new UserManager(_usCoMock.Object, _usReMock.Object);
            SimpleUser simpleUser = new SimpleUser();
            simpleUser.Email = "boh";
            simpleUser.Password = "ciao";
            User expectedUser = null; // Dati utente attesi

            _usReMock.Setup(m => m.GetUser(simpleUser)).ReturnsAsync(expectedUser);

            async Task Act() => await _umt.GetUser(simpleUser);

            // Assert
            Assert.ThrowsAsync<Exception>(Act);

            // Verifica che i metodi siano stati chiamati
            _usReMock.Verify(m => m.GetUser(simpleUser), Times.Once);
        }




        [Test]
        public void EmailUsedNoSuchEmailInTheDatabase()
        {
            UserManager _umt = new UserManager(_usCoMock.Object, _usReMock.Object);
            SimpleUser simpleUser = new SimpleUser();
            simpleUser.Email = "boh";
            simpleUser.Password = "ciao";
            User expectedUser = null; // Dati utente attesi

            _usReMock.Setup(m => m.FindUserWithEmail(simpleUser)).ReturnsAsync(expectedUser);

            async Task Act()
            {
                await _umt.EmailUsed(simpleUser);
            }

            // Assert
            Assert.DoesNotThrowAsync(Act);

            // Verifica che i metodi siano stati chiamati
            _usReMock.Verify(m => m.FindUserWithEmail(simpleUser), Times.Once);
        }



        [Test]
        public void EmailUsedEmailAlreadyInUse()
        {
            UserManager _umt = new UserManager(_usCoMock.Object, _usReMock.Object);
            SimpleUser simpleUser = new SimpleUser();
            simpleUser.Email = "boh";
            simpleUser.Password = "ciao";
            User expectedUser = new User("", ""); // Dati utente attesi

            _usReMock.Setup(m => m.FindUserWithEmail(simpleUser)).ReturnsAsync(expectedUser);

            async Task Act()
            {
                await _umt.EmailUsed(simpleUser);
            }

            // Assert
            Assert.ThrowsAsync<Exception>(Act);

            // Verifica che i metodi siano stati chiamati
            _usReMock.Verify(m => m.FindUserWithEmail(simpleUser), Times.Once);
        }





        [Test]
        public void EmailUsedEmailMissing()
        {
            UserManager _umt = new UserManager(_usCoMock.Object, _usReMock.Object);
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
            _usReMock.Verify(m => m.FindUserWithEmail(simpleUser0), Times.Never);
        }



        [Test]
        public void EmailUsedPasswordMissing()
        {
            UserManager _umt = new UserManager(_usCoMock.Object, _usReMock.Object);
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
            _usReMock.Verify(m => m.FindUserWithEmail(simpleUser0), Times.Never);
        }

        //Login

        
        

        /*
        [Test]
        public async Task LoginSuccess()
        {
            var to = new TokenOption
            {
                Secret = "non sapevo cosa mettere ma serve renderla piu' lunga e quindi questo sto facendo, o no?'",
                ExpiryDays = 7,
                Issuer = "ServerProva",
                Audience = "API"
            };

            var csm = new Mock<IConfigurationSection>();

            UserManager _userManagerMock = new UserManager(_usCoMock.Object, _usReMock.Object);
            SimpleUser simpleUser0 = new SimpleUser();
            simpleUser0.Email = "boh";
            simpleUser0.Password = "ciao";
            User u = new User(simpleUser0);

            _usReMock.Setup(m => m.GetUser(simpleUser0)).ReturnsAsync(u);
            _usCoMock.Setup(m => m.GetSection("TokenOptions")).Returns(csm.Object);
            //csm.Setup(m => m.Get<TokenOption>()).Returns(to);

            //enc.Setup(m => m.GetBytes("")).Returns(prova);


            string token = await _userManagerMock.Login(simpleUser0);

            Assert.Equals(token, Is.EqualTo(""));

        }
        */




        //register
    }
}