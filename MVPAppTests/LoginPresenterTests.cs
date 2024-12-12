using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using MVPApp.Context;
using MVPApp.Interfaces;
using MVPApp.Models;

namespace MVPAppTests
{
    [TestFixture]
    public class LoginPresenterTests
    {
        private Mock<ILoginView> _mockView;
        private AppDbContext _dbContext;
        private LoginPresenter _presenter;

        private IConfiguration Configuration { get; set; }

        [SetUp]
        public void Setup()
        {
            User _dummyUser = new User { Username = "testuser", Email = "testuser@gmail.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password") };

            var builder = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // Set the base directory
           .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true); // Load the test config

            Configuration = builder.Build();

            // Set up InMemory Database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new AppDbContext(options, Configuration);

            // Seed data
            _dbContext.Users.Add(_dummyUser);
            _dbContext.SaveChanges();

            // Mock ILoginView
            _mockView = new Mock<ILoginView>();

            // Instantiate the presenter
            _presenter = new LoginPresenter(_dbContext);
            _presenter.SetView(_mockView.Object);
        }

        [Test]
        public void Login_SuccessfulLogin_CallsNavigateToDashboard()
        {
            // Arrange
            _mockView.Setup(v => v.Username).Returns("testuser");
            _mockView.Setup(v => v.Password).Returns("password");

            // Act
            _presenter.Login();

            // Assert
            _mockView.Verify(v => v.NavigateToDashboard(It.Is<User>(u => u.Username == "testuser")), Times.Once);
        }

        [Test]
        public void Login_InvalidCredentials_ShowsErrorMessage()
        {
            // Arrange
            _mockView.Setup(v => v.Username).Returns("invaliduser");
            _mockView.Setup(v => v.Password).Returns("wrongpassword");

            // Act
            _presenter.Login();

            // Assert
            _mockView.Verify(v => v.ShowMessage("Invalid credentials"), Times.Once);
            _mockView.Verify(v => v.NavigateToDashboard(It.IsAny<User>()), Times.Never);
        }
    }
}