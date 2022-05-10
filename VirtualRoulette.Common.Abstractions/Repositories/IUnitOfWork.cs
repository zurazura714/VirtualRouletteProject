using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualRoulette.Common.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}