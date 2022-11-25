// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResource(
                name: "profile",
                userClaims: new[] { "name", "email", "website" },
                displayName: "Your profile data")
            };

        /// <summary>
        /// API — це ресурс у вашій системі, який ви хочете захистити
        /// </summary>
        /// <remarks>
        /// Якщо ви будете використовувати це у виробництві, важливо дати вашому API логічну назву. 
        /// Розробники використовуватимуть це для підключення до вашого API через сервер ідентифікації. 
        /// Він має описувати ваш API простими словами як для розробників, так і для користувачів.
        /// </remarks>
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1", "My API"),
                new ApiScope("api2", "Vitaliy API 2")
            };

        /// <summary>
        ///  Клієнтська програма, яку ми будемо використовувати для доступу до нашого нового API
        /// </summary>
        /// <remarks>
        /// Ви можете розглядати ClientId і ClientSecret як логін і пароль для самої програми. 
        /// Він ідентифікує вашу програму на сервері ідентифікації, щоб він знав, яка програма намагається з’єднатися з нею.
        /// </remarks>
        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                new Client
                {
                    ClientId = "client",

                    // немає інтерактивного користувача, використовуйте clientid/secret для автентифікації
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // секрет для аутентифікації
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // області, до яких клієнт має доступ
                    AllowedScopes = { "api1" }
                },
                new Client
                {
                    ClientId = "client2",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "openid", "profile" }
                }
            };
    }
}