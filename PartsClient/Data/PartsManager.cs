using Newtonsoft.Json;

namespace PartsClient.Data
{
    public static class PartsManager
    {
        // TODO: Add fields for BaseAddress, Url, and authorizationKey
        static readonly string BaseAddress = "https://mslearnpartsserver122054486.azurewebsites.net";
        static readonly string Url = $"{BaseAddress}/api/";
        private static string authorizationKey;

        static HttpClient client;

        private static async Task<HttpClient> GetClient()
        {
            if (client != null)
                return client;

            client = new HttpClient();

            if (string.IsNullOrEmpty(authorizationKey))
            {
                authorizationKey = await client.GetStringAsync($"{Url}login");
                authorizationKey = JsonConvert.DeserializeObject<string>(authorizationKey);
            }

            client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public static async Task<IEnumerable<Part>> GetAll()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new List<Part>();

            HttpClient client = await GetClient();
            string result = await client.GetStringAsync($"{Url}parts");

            return JsonConvert.DeserializeObject<List<Part>>(result);
        }

        public static async Task<Part> Add(string partName, string supplier, string partType)
        {
            throw new NotImplementedException();
        }

        public static async Task Update(Part part)
        {
            throw new NotImplementedException();
        }

        public static async Task Delete(string partID)
        {
            throw new NotImplementedException();                        
        }
    }
}
