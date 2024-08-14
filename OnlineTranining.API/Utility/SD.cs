using OnlineTranining.API.Data.DataAccess;

namespace OnlineTranining.API.Utility
{
    public static class SD
    {
        public const string Role_Admin = "admin";
        public const string Role_User = "user";
        public const string Role_Trainer = "trainer";
    }

    public static class GenerateToken
    {
        public static async Task<string> GernrateJWTToken(ApplicationUser user)
        {
            string token = string.Empty;

            var 

            return await Task.FromResult<string>(token);
        }
    }
}
