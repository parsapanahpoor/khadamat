using DataAccess.Design_Pattern.GenericRepositories;
using Models.Entities.EmployeeReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public interface IDataReservationRepository : IGernericRepository<DataReservation>
    {
    }
}
