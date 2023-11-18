using System.Collections.Generic;
using System.Threading.Tasks;
using UndacApp.Models;

public class BasicExpertService
{
    private List<BasicExpert> _experts;

    public BasicExpertService()
    {
        _experts = new List<BasicExpert>
        {
            new BasicExpert { ID = 1, Name = "John Doe", Skill = "  Logistics", IsAvailable = true, Status = " Available" },
            new BasicExpert { ID = 2, Name = "Jane Smith", Skill = "  Supply Chain", IsAvailable = true, Status = " Available" },
            new BasicExpert { ID = 3, Name = "Michael Johnson", Skill = " Procurement", IsAvailable = false, Status = " Unavailable" },
            new BasicExpert { ID = 4, Name = "Emily Davis", Skill = "  Inventory Management", IsAvailable = true, Status = " Available" },
            new BasicExpert { ID = 5, Name = "Robert Wilson", Skill = "  Shipping", IsAvailable = true, Status = " Available" },
            new BasicExpert { ID = 6, Name = "Sarah Brown", Skill = "  Warehousing", IsAvailable = false, Status = " Unavailable" },
        };
    }

    public Task<List<BasicExpert>> GetAvailableExpertsAsync()
    {
        var availableExperts = _experts.FindAll(expert => expert.IsAvailable); // Adjust the condition based on your logic
        return Task.FromResult(availableExperts);
    }

    public List<BasicExpert> GetExperts()
    {
        return _experts;
    }
}

