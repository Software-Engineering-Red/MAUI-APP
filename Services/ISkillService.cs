using MauiApp1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    /// <summary>
    /// Interface for managing skills.
    /// </summary>
    public interface ISkillService
    {
        /// <summary>
        /// Retrieves a list of all skills.
        /// </summary>
        /// <returns>A list of skills.</returns>
        Task<List<Skill>> GetSkillList();

        /// <summary>
        /// Adds a new skill.
        /// </summary>
        /// <param name="skill">The skill to add.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> AddSkill(Skill skill);

        /// <summary>
        /// Deletes a skill.
        /// </summary>
        /// <param name="skill">The skill to delete.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> DeleteSkill(Skill skill);

        /// <summary>
        /// Updates an existing skill.
        /// </summary>
        /// <param name="skill">The skill to update.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> UpdateSkill(Skill skill);
    }
}
