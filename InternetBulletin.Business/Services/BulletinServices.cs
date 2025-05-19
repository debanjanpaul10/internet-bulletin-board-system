// *********************************************************************************
//	<copyright file="BulletinServices.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Bulletin Services Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
    using System.Globalization;
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs.Users;
    using InternetBulletin.Shared.Helpers;

    /// <summary>
    /// Bulletin services class.
    /// </summary>
    /// <param name="httpClientHelper">The http client helper.</param>
    /// <seealso cref="IBulletinServices"/>
    public class BulletinServices(IHttpClientHelper httpClientHelper) : IBulletinServices
    {
        /// <summary>
        /// The http client helper.
        /// </summary>
        private readonly IHttpClientHelper _httpClientHelper = httpClientHelper;

        /// <summary>
        /// Gets graph user data async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The graph user data dto.</returns>
        public async Task<GraphUserDTO> GetGraphUserDataAsync(string userName)
        {
            var responseDto = new GraphUserDTO();
            var graphResponse = await this._httpClientHelper.GetGraphApiDataAsync(userName);
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
    }
}

