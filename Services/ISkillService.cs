using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface ISkillService
    {
        Task<List<Skill>> GetSkillList();
        Task<int> AddSkill(Skill skill);
        Task<int> DeleteSkill(Skill skill);
        Task<int> UpdateSkill(Skill skill);
    }
}
