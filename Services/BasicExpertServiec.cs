using System.Collections.Generic;
using System.Threading.Tasks;
using MauiApp1.Models; // Adjust the namespace based on your project structure

public class BasicExpertService
{
    private List<BasicExpert> _experts;

    public BasicExpertService()
    {
        _experts = new List<Expert>
        {
            new Expert { ID = 1, Name = "John Doe", Skill = "Logistics", IsAvailable = true },
            new Expert { ID = 2, Name = "Jane Smith", Skill = "First Aid", IsAvailable = false },
            // Add more experts as needed
        };
    }

    public Task<List<Expert>> GetAllExpertsAsync()
    {
        return Task.FromResult(_experts);
    }

    public Task<List<Expert>> GetAvailableExpertsAsync()
    {
        var availableExperts = _experts.FindAll(expert => expert.IsAvailable);
        return Task.FromResult(availableExperts);
    }
}
