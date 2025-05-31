// *********************************************************************************
//	<copyright file="UsersService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Users Services Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
    using System.Globalization;
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs.Users;
    using InternetBulletin.Shared.Helpers;

    /// <summary>
    /// The users service class.
    /// </summary>
    /// <param name="httpClientHelper">The http client helper</param>
    /// <param name="usersDataService">The users data service</param>
    /// <seealso cref="IUsersService"/>
    public class UsersService(IHttpClientHelper httpClientHelper, IUsersDataService usersDataService) : IUsersService
    {
        /// <summary>
        /// The http client helper.
        /// </summary>
        private readonly IHttpClientHelper _httpClientHelper = httpClientHelper;

        /// <summary>
        /// The users data service.
        /// </summary>
        private readonly IUsersDataService _usersDataService = usersDataService;

        /// <summary>
        /// Gets graph user data async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The graph user data dto.</returns>
        public async Task<GraphUserDTO> GetGraphUserDataAsync(string userName)
        {
            var responseDto = new GraphUserDTO();
            var graphResponse = await this._httpClientHelper.GetGraphApiDataAsync();
            if (graphResponse?.Value is not null)
            {
                // Find the user with matching username
                var user = graphResponse.Value.FirstOrDefault(u =>
                    u.AdditionalData is not null &&
                    u.AdditionalData.ContainsKey(IbbsConstants.UserNameExtensionConstant) &&
                    Convert.ToString(u.AdditionalData[IbbsConstants.UserNameExtensionConstant], CultureInfo.CurrentCulture) == userName);

                if (user is not null)
                {
                    // Create a new GraphUserDTO object with only the required fields
                    var filteredUser = new GraphUserDTO()
                    {
                        Id = user.Id ?? string.Empty,
                        DisplayName = user.DisplayName ?? string.Empty,
                        UserName = Convert.ToString(user.AdditionalData[IbbsConstants.UserNameExtensionConstant], CultureInfo.CurrentCulture) ?? string.Empty,
                        EmailAddress = user.Identities?
                            .FirstOrDefault(i => i.SignInType == IbbsConstants.EmailAddressConstant)?
                            .IssuerAssignedId ?? string.Empty
                    };

                    responseDto = filteredUser;
                }
            }

            return responseDto;
        }

        /// <summary>
        /// Gets all graph users data async.
        /// </summary>
        public async Task<List<GraphUserDTO>> GetAllGraphUsersDataAsync()
        {
            var responseDto = new List<GraphUserDTO>();
            var graphResponse = await this._httpClientHelper.GetGraphApiDataAsync();
            if (graphResponse?.Value is not null)
            {
                var usersData = graphResponse.Value.Where(u =>
                    u.AdditionalData.ContainsKey(IbbsConstants.UserNameExtensionConstant)
                    && !string.IsNullOrEmpty(Convert.ToString(u.AdditionalData[IbbsConstants.UserNameExtensionConstant], CultureInfo.CurrentCulture))).ToList();
                if (usersData.Count > 0)
                {
                    responseDto = [..usersData.Select(user => new GraphUserDTO
                    {
                        Id = user.Id ?? string.Empty,
                        DisplayName = user.DisplayName ?? string.Empty,
                        UserName = Convert.ToString(user.AdditionalData[IbbsConstants.UserNameExtensionConstant], CultureInfo.CurrentCulture) ?? string.Empty,
                        EmailAddress = user.Identities?.FirstOrDefault(i => i.SignInType == IbbsConstants.EmailAddressConstant)?.IssuerAssignedId ?? string.Empty
                    })];
                }
            }

            return responseDto;
        }

        /// <summary>
        /// Saves users data from azure ad async.
        /// </summary>
        /// <param name="usersData">The users data.</param>
        /// <returns>The boolean for success / failure</returns>
        public async Task<bool> SaveUsersDataFromAzureAdAsync()
        {
            var usersData = await this.GetAllGraphUsersDataAsync();
            if (usersData is not null && usersData.Count > 0)
            {
                return await this._usersDataService.SaveUsersDataAsync(usersData);
            }

            return false;
        }
    }
}


