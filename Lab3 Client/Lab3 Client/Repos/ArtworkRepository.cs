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
    public class ArtworkRepository : IArtworkRepository
    {
        HttpClient client = new HttpClient();

        public ArtworkRepository()
        {
            client.BaseAddress = Common.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Artwork>> GetArtwork()
        {
            var response = await client.GetAsync("/api/Artworks");
            if (response.IsSuccessStatusCode)
            {
                List<Artwork> Artwork = await response.Content.ReadAsAsync<List<Artwork>>();
                return Artwork;
            }
            else
            {
                return new List<Artwork>();
            }
        }

        public async Task<List<Artwork>> GetArtworkByType(int TypeID)
        {
            var response = await client.GetAsync($"/api/artworks/ByType/{TypeID}");
            if (response.IsSuccessStatusCode)
            {
                List<Artwork> artworks = await response.Content.ReadAsAsync<List<Artwork>>();
                return artworks;
            }
            else
            {
                return new List<Artwork>();
            }
        }

        public async Task<Artwork> GetArtwork(int ID)
        {
            var response = await client.GetAsync($"/api/Artworks/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Artwork artwork = await response.Content.ReadAsAsync<Artwork>();
                return artwork;
            }
            else
            {
                return new Artwork();
            }
        }

        public async Task AddArtwoork(Artwork workToAdd)
        {
            var response = await client.PostAsJsonAsync("/api/Artworks", workToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Common.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateArtwork(Artwork workToUpdate)
        {
            var response = await client.PutAsJsonAsync($"/api/Artworks/{workToUpdate.ID}", workToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Common.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteArtwork(Artwork workToDelete)
        {
            var response = await client.DeleteAsync($"/api/Artworks/{workToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Common.CreateApiException(response);
                throw ex;
            }
        }               
    }
}
