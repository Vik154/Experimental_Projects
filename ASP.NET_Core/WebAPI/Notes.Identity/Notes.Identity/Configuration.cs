﻿using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4;

namespace Notes.Identity;

public static class Configuration {

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope> {
            new ApiScope("NotesWebAPI", "Web API")
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource> {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource> {
            new ApiResource("NotesWebAPI"
                            , "Web API"
                            , new [] { JwtClaimTypes.Name}) { Scopes = {"NotesWebAPI"} }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client> {
            new Client {
                ClientId = "notes-web-app",
                ClientName = "Notes Web",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris = { "http://localhost:..../signin-oidc" },
                AllowedCorsOrigins = { "http://localhost:...." },
                PostLogoutRedirectUris = { "http://localhost:..../signout-oidc" },
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "NotesWebAPI"
                },
                AllowAccessTokensViaBrowser = true
            }
        };
}
