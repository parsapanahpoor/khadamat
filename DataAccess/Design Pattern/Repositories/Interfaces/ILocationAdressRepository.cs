using DataAccess.Design_Pattern.GenericRepositories;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
   public interface ILocationAdressRepository : IGernericRepository<Location>
    {

        List<Location> GetUserLocations(string Userid);
        void AddUserLocation(Location lcoation);
        Location GetUserLocationById(int id );
        void UpdateUserLocation(Location location);
        void DeleteUserLocation(Location location);

    }
    
}
