// *********************************************************************************
//	<copyright file="ProfilesServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Profiles service tests.</summary>
// *********************************************************************************

using InternetBulletin.Business.Contracts;

namespace InternetBulletin.UnitTests.Business
{
    /// <summary>
    /// Profiles service tests.
    /// </summary>
    public class ProfilesServiceTests
    {
        /// <summary>
        /// The user name.
        /// </summary>
        private static readonly string UserName = "user@example.com";

        /// <summary>
        /// The mock logger.
        /// </summary>
        private readonly Mock<ILogger<ProfilesService>> _mockLogger;

        /// <summary>
        /// The mock profiles data service.
        /// </summary>
        private readonly Mock<IProfilesDataService> _mockProfilesDataService;

        private readonly Mock<IUsersService> _mockUsersService;

        /// <summary>
        /// The profiles service.
        /// </summary>
        private readonly ProfilesService _profilesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilesServiceTests"/> class.
        /// </summary>
        public ProfilesServiceTests()
        {
            this._mockLogger = new Mock<ILogger<ProfilesService>>();
            this._mockProfilesDataService = new Mock<IProfilesDataService>();
            this._mockUsersService = new Mock<IUsersService>();

            this._profilesService = new ProfilesService(
                this._mockLogger.Object,
                this._mockProfilesDataService.Object,
                this._mockUsersService.Object
            );
        }


    }
}


