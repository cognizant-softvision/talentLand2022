using AutoMapper;
using FluentValidation.TestHelper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Features.User.GetUsers;
using TalentLand.Trivia.Application.Interfaces.Repositories;
using TalentLand.Trivia.Application.Mappings;
using TalentLand.Trivia.Infra.Persistence.Repositories;
using Xunit;

namespace TalentLand.Trivia.UnitTesting.User
{
    public class UserHandlerTest : IClassFixture<UserFixture>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly GetUsersValidator _getUsersValidator;
        private readonly GetUsersQueryHandler _getUsersQueryHandler;

        private const string SUCCESS_STATUS = "Success";

        public UserHandlerTest(UserFixture userFixture)
        {
            _userRepository = new UserRepository(userFixture.DbContext);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new UserProfile())).CreateMapper();
            _getUsersValidator = new GetUsersValidator();
            _getUsersQueryHandler = new GetUsersQueryHandler(_mapper, _userRepository);
        }

        public class UserTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new
                    {
                        Name = "test name last",
                        Email = "test@test.com",
                        University = "university test",
                        Company = "company test"
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(null)]
        public void Test_Validation_Limit(int? limit)
        {
            //Arrange
            var usersQuery = new GetUsersQuery() { Limit = limit, Offset = 0 };

            //Act
            var result = _getUsersValidator.TestValidate(usersQuery);

            //Assert
            result.ShouldHaveValidationErrorFor(query => query.Limit);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(null)]
        public void Test_Validation_Offset(int? offset)
        {
            //Arrange
            var usersQuery = new GetUsersQuery() { Limit = 0, Offset = offset };

            //Act
            var result = _getUsersValidator.TestValidate(usersQuery);

            //Assert
            result.ShouldHaveValidationErrorFor(query => query.Offset);
        }

        [Theory]
        [ClassData(typeof(UserTestData))]
        public async Task Test_Hanlder_Success(dynamic userTestData)
        {
            //Arrange
            var query = new GetUsersQuery() { Limit = 50, Offset = 0 };

            //Act
            var result = await _getUsersQueryHandler.Handle(query, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(SUCCESS_STATUS ,result.Status);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Users);
            Assert.NotEmpty(result.Data.Users);
            var sut = result.Data.Users.FirstOrDefault();
            Assert.Equal(userTestData.Name, sut!.Name);
            Assert.Equal(userTestData.Email, sut!.Email);
            Assert.Equal(userTestData.University, sut!.University);
            Assert.Equal(userTestData.Company, sut!.Company);
        }

        [Fact]
        public async Task Test_Hanlder_EmptyData_When_Limit_is_Zero()
        {
            //Arrange
            var query = new GetUsersQuery() { Limit = 0, Offset = 0 };

            //Act
            var result = await _getUsersQueryHandler.Handle(query, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(SUCCESS_STATUS, result.Status);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Users);
            Assert.Empty(result.Data.Users);
        }

        [Fact]
        public async Task Test_Hanlder_EmptyData_When_Offset_is_One()
        {
            //Arrange
            var query = new GetUsersQuery() { Limit = 0, Offset = 1 };

            //Act
            var result = await _getUsersQueryHandler.Handle(query, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(SUCCESS_STATUS, result.Status);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Users);
            Assert.Empty(result.Data.Users);
        }
    }
}