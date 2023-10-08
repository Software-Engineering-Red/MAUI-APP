using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    /// <summary>
    /// Represents the IRotaService interface, which defines methods for CRUD operations on Rota objects.
    /// </summary>
    public interface IRotaService
    {
        /// <summary>
        /// Retrieves a list of all Rotas.
        /// </summary>
        /// <returns>A list of Rota objects.</returns>
        Task<List<Rota>> GetRotaList();

        /// <summary>
        /// Adds a new Rota to the service.
        /// </summary>
        /// <param name="rota">The Rota object to be added.</param>
        /// <returns>The number of rows affected (should be 1 if successful).</returns>
        Task<int> AddRota(Rota rota);

        /// <summary>
        /// Deletes a Rota from the service.
        /// </summary>
        /// <param name="rota">The Rota object to be deleted.</param>
        /// <returns>The number of rows affected (should be 1 if successful).</returns>
        Task<int> DeleteRota(Rota rota);

        /// <summary>
        /// Updates an existing Rota in the service.
        /// </summary>
        /// <param name="rota">The Rota object to be updated.</param>
        /// <returns>The number of rows affected (should be 1 if successful).</returns>
        Task<int> UpdateRota(Rota rota);
    }
}
