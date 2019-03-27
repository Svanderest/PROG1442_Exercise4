using Lab3_Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3_Client.Repos
{
    public interface IArtworkRepository
    {
        Task<List<Artwork>> GetArtwork();
        Task<Artwork> GetArtwork(int ID);
        Task<List<Artwork>> GetArtworkByType(int TypeID);
        Task AddArtwoork(Artwork workToAdd);
        Task UpdateArtwork(Artwork workToUpdate);
        Task DeleteArtwork(Artwork workToDelete);
    }
}