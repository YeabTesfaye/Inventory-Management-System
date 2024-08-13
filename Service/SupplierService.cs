using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Service.Contracts;

namespace Service
{
    public class SupplierService  : ISupplierService
    {
        private readonly IRepositoryManager _repositoryManager;
        public SupplierService(IRepositoryManager repositoryManager){
            _repositoryManager = repositoryManager;
         }
    }
}