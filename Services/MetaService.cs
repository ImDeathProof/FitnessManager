using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;
using FitnessManager.Repositories;

namespace FitnessManager.Services
{
    public class MetaService : IMetaService
    {
        private readonly IMetaRepository _metaRepository;
        public MetaService(IMetaRepository metaRepository)
        {
            _metaRepository = metaRepository;
        }

        public async Task AddMetaAsync(Meta meta)
        {
            if(meta == null)
            {
                throw new MetaServiceException("El objeto Meta no puede ser nulo.");
            }
            try
            {
                _metaRepository.AddMeta(meta);
                await _metaRepository.SaveChangesAsync();
            }
            catch(DbException dbEx)
            {
                throw new MetaServiceException("Error de base de datos al agregar la meta.", dbEx);
            }
            catch(Exception ex)
            {
                throw new MetaServiceException("Error al agregar la meta.", ex);
            }
        }

        public Task DeleteMetaAsync(int metaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Meta>> GetAllMetasByUserIDAsync(string UsuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<Meta> GetMetaByIDAsync(int metaId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMetaAsync(Meta meta)
        {
            throw new NotImplementedException();
        }
    }
}
public class MetaServiceException : Exception
{
    public MetaServiceException() { }

    public MetaServiceException(string message) : base(message) { }

    public MetaServiceException(string message, Exception inner) : base(message, inner) { }
}