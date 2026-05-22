using Authservice.DTOs.Form;
using Authservice.Models;

namespace Authservice.Service;

public interface IFormService
{
    Task<FeedbackForm> CreateFormAsync(CreateFormDTO dto);
    Task<FeedbackForm> GetFormAsync(Guid id);
}