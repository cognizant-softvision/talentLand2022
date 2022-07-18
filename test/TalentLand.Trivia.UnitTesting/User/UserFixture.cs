using TalentLand.Trivia.UnitTesting.Mocks;

namespace TalentLand.Trivia.UnitTesting.User
{
    public class UserFixture: FixtureBase
    {
        private const string _usersJson = @"[{'Id': '82a4d2a2-d236-4141-b584-58e8f8723c17', 'Email': 'test@test.com', 'Name': 'test name last', 'University': 'university test', 'Company': 'company test', 'CreationDate': '2022-07-13T00:00:00' }]";

        protected override void AddDataEvent()
        {
            base.DbContext.AddRange(base.DeserializeJson<Domain.User>(_usersJson));
        }
    }
}
