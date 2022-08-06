using IdentityServer4.Models;
using System.Collections.Generic;

namespace sdakcc.Web
{
    public class Config
    {
        //private readonly IApiResourceRepository _apiResourceRepository;
        //private readonly IClientRepository _clientRepository;


        //public static IEnumerable<ApiResource> GetApiResourcesOut;
        //public static IEnumerable<Client> GetClientsOut;


        //private  Config(IApiResourceRepository apiResourceRepository)
        //{
        //    _apiResourceRepository = apiResourceRepository;

        //}

        //public  static IEnumerable<ApiResource> GetApiResources()
        //{
        //    //GetApiResourcesOut = await _apiResourceRepository.GetListAsync();
        //    //return GetApiResourcesOut;


        //        var apiResource =  new List<ApiResource> {
        //            new ApiResource(
        //                new Guid("3021cf42-cdcb-4f56-bf36-13734a72463f"),
        //                "sdakcc_React",
        //                 "sdakcc_React" + " API"
        //        )};



        //    return apiResource;
        //}

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("myresourceapi", "My Resource API")
                {
                   // Scopes = {new Scope("apiscope")}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // for public api
                new Client
                {
                    ClientId = "secret_client_id",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "apiscope" }
                }
            };
        }


    }
}
