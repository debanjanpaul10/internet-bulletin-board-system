

namespace InternetBulletin.Data.DataServices
{
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Shared.DTOs;
    public class ProfilesDataService(SqlDbContext dbContext) : IProfilesDataService
    {
        private readonly SqlDbContext _dbContext = dbContext;

        public Task<UserProfileDto> GetUserProfileDataAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}