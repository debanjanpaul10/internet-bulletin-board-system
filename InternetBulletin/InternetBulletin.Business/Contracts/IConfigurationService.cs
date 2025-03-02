// *********************************************************************************
//	<copyright file="IConfigurationService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Configuration service interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Contracts
{
    /// <summary>
    /// Configuration service interface.
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Gets configuration value.
        /// </summary>
        /// <param name="keyName">The key name.</param>
        string GetConfigurationValue(string keyName);
    }
}