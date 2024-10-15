using api_sanarate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_sanarate.Services
{
    public class HistoriaService : IHistoriaService
    {
        private readonly ApplicationDbContext _dbContext;

        // Constructor para inyección de dependencias del contexto de base de datos
        public HistoriaService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // Implementación de los métodos definidos en la interfaz IHistoriaService
        public async Task<List<Historia>> GetAllHistoriasAsync()
        {
            return await _dbContext.historia.ToListAsync();
        }

        public async Task<Historia> GetHistoriaByIdAsync(int id)
        {
            var historia = await _dbContext.historia.FindAsync(id);
            if (historia == null)
            {
                throw new KeyNotFoundException($"Historia con id {id} no encontrada.");
            }
            return historia;
        }

        public async Task AddHistoriaAsync(Historia historia)
        {
            await _dbContext.historia.AddAsync(historia);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateHistoriaAsync(int id, Historia historia)
        {
            var historiaExistente = await _dbContext.historia.FindAsync(id);
            if (historiaExistente == null)
            {
                return false;
            }

            // Actualización de las propiedades
            historiaExistente.evento = historia.evento;
            historiaExistente.descripcion = historia.descripcion;
            historiaExistente.fecha = historia.fecha;
            historiaExistente.imagen = historia.imagen;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteHistoriaAsync(int id)
        {
            var historia = await _dbContext.historia.FindAsync(id);
            if (historia == null)
            {
                return false;
            }

            _dbContext.historia.Remove(historia);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
