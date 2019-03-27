using Lab3_Client.Models;
using Lab3_Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Client.Repos
{
    public class ArtTypeRepository : IArtTypeRepository
    {
        HttpClient client = new HttpClient();

        public ArtTypeRepository()
        {
            client.BaseAddress = Common.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ArtType>> GetArtTypes()
        {
            var response = await client.GetAsync("/api/ArtTypes");
            if (response.IsSuccessStatusCode)
            {
                List<ArtType> doctors = await response.Content.ReadAsAsync<List<ArtType>>();
                return doctors;
            }
            else
            {
                return new List<ArtType>();
            }

        }

        public async Task<ArtType> GetArtType(int ArtTypeID)
        {
            var response = await client.GetAsync($"/api/ArtTypes/{ArtTypeID}");
            if (response.IsSuccessStatusCode)
            {
                ArtType artType = await response.Content.ReadAsAsync<ArtType>();
                return artType;
            }
            else
            {
                return new ArtType();
            }
        }
        public async Task AddArtType(ArtType typeToAdd)
        {
            var response = await client.PostAsJsonAsync("/api/ArtTypes", typeToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Common.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateArtType(ArtType typeToUpdate)
        {
            var response = await client.PutAsJsonAsync($"/api/artTypes/{typeToUpdate.ID}", typeToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Common.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteArtType(ArtType typeToDelete)
        {
            var response = await client.DeleteAsync($"/api/ArtTypes/{typeToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Common.CreateApiException(response);
                throw ex;
            }
        }                                
    }
}
