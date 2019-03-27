using System.Collections.Generic;
using System.Threading.Tasks;
using Lab3_Client.Models;

namespace Lab3_Client.Repos
{
    public interface IArtTypeRepository
    {
        Task<List<ArtType>> GetArtTypes();
        Task<ArtType> GetArtType(int ArtTypeID);
        Task AddArtType(ArtType typeToAdd);
        Task UpdateArtType(ArtType typeToUpdate);
        Task DeleteArtType(ArtType typeToDelete);
    }
}