namespace Backend.Services;
using Backend.Models;
public interface ISpeciesService
{
    Task<IEnumerable<SpeciesDTO>> GetAll();
    Task<SpeciesDTO> Get(int id);
    Task<SpeciesDTO> Add(SpeciesDTO species);
    Task<SpeciesDTO> Update(SpeciesDTO species);
    Task<bool> Delete(int id);
}
public class SpeciesService
{
    
}