﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Project.IdentityServer
{
    public static class Config
    {
        // startup.cs -->   .AddInMemoryApiResources(Config.ApiResources)
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_category"){Scopes={"category_fullpermission"}},
            new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
            new ApiResource("resource_cart"){Scopes={"cart_fullpermission"}},
            new ApiResource("resource_discount"){Scopes={"discount_fullpermission"}},
            new ApiResource("resource_order"){Scopes={"order_fullpermission"}},
            // new ApiResource("resource_payment"){Scopes={"payment_fullpermission"}},
            // new ApiResource("resource_gateway"){Scopes={"gateway_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(), 
                new IdentityResource(){ Name="roles", DisplayName="Roles", Description="Kullanıcı rolleri", UserClaims=new []{ "role"} }

                //new IdentityResources.OpenId(),
                //new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
 

                new ApiScope("category_fullpermission","Category API için full erişim"),
                new ApiScope("photo_stock_fullpermission","Photo Stock API için full erişim"),
                new ApiScope("cart_fullpermission","Cart API için full erişim"),
                new ApiScope("discount_fullpermission","Discount API için full erişim"),
                new ApiScope("order_fullpermission","Order API için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)

                //new ApiScope("scope1"),
                //new ApiScope("scope2"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                   ClientName="Asp.Net Core MVC",
                   ClientId="WebMvcClient",
                   ClientSecrets= {new Secret("secret".Sha256())},
                   AllowedGrantTypes= GrantTypes.ClientCredentials,
                   AllowedScopes=
                    { 
                        "category_fullpermission", "photo_stock_fullpermission", "gateway_fullpermission"
                        , IdentityServerConstants.LocalApi.ScopeName 
                    }
                },
                new Client 
                {
                   ClientName="Asp.Net Core MVC",
                   ClientId="WebMvcClientForUser",
                   AllowOfflineAccess=true,
                   ClientSecrets= {new Secret("secret".Sha256())},
                   AllowedGrantTypes= GrantTypes.ResourceOwnerPassword, // ResourceOwnerPassword --> IdentityResourceOwnerPasswordValidator.cs'eyi tetikler ve bu da token döner
                   AllowedScopes={ "cart_fullpermission", "order_fullpermission", "gateway_fullpermission", "discount_fullpermission", "order_fullpermission", IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess
                    , IdentityServerConstants.LocalApi.ScopeName // client (postman), artık identityserv4'e istek yapabilir
                    , "roles" },
                    AccessTokenLifetime=1*60*60, // 1 saat
                    RefreshTokenExpiration=TokenExpiration.Absolute, 
                    AbsoluteRefreshTokenLifetime= (int) (DateTime.Now.AddDays(60)- DateTime.Now).TotalSeconds,
                    RefreshTokenUsage= TokenUsage.ReUse
                }

                //// m2m client credentials flow client
                //new Client
                //{
                //    ClientId = "m2m.client",
                //    ClientName = "Client Credentials Client",

                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                //    AllowedScopes = { "scope1" }
                //},

                //// interactive client using code flow + pkce
                //new Client
                //{
                //    ClientId = "interactive",
                //    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                //    AllowedGrantTypes = GrantTypes.Code,

                //    RedirectUris = { "https://localhost:44300/signin-oidc" },
                //    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                //    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                //    AllowOfflineAccess = true,
                //    AllowedScopes = { "openid", "profile", "scope2" }
                //},
            };
    }
}