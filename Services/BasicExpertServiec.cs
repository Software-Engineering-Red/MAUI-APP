using System.Collections.Generic;
using System.Threading.Tasks;
using MauiApp1.Models; // Ensure this is using the correct namespace for your Expert model

public class BasicExpertService
{
    private List<BasicExpert> _experts;

    public BasicExpertService()
    {
        _experts = new List<BasicExpert>
        {
            new BasicExpert { ID = 1, Name = "John Doe", Skill = "Logistics", IsAvailable = true },
            new BasicExpert { ID = 2, Name = "Jane Smith", Skill = "First Aid", IsAvailable = false },
        };
    }

    public Task<List<Expert>> GetAvailableExpertsAsync()
    {
        var availableExperts = _experts.FindAll(expert => expert.Status == "Available"); // Adjust the condition based on your logic
        return availableExperts;
    }
}

