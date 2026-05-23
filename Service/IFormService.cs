using Authservice.DTOs.Form;
using Authservice.Models;

namespace Authservice.Service;

public interface IFormService
{
    Task<FormResponseDTO> CreateFormAsync(CreateFormDTO dto);
    Task<FormResponseDTO> GetFormAsync(Guid id);
    
}