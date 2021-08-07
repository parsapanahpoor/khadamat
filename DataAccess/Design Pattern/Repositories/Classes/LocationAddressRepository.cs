using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataContext.Context;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class LocationAddressRepository : GenericRepository<Location>, ILocationAdressRepository
    {
        private readonly KhadamatContext _db;

        public LocationAddressRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void AddLocationBeforeReserve(string Userid, string Location, string PostalCode)
        {
            Location loc = new Location()
            { 
            
                UserID = Userid,
                LocationAddress = Location,
                PostalCode = PostalCode
            
            };

            Add(loc);
        }

        public void AddUserLocation(Location lcoation)
        {
            Location loc = new Location()
            { 
                UserID = lcoation.UserID,
                LocationAddress = lcoation.LocationAddress,
                PostalCode = lcoation.PostalCode
            };

            Add(loc);
         }

        public void DeleteUserLocation(Location location)
        {
            Delete(location);
        }

        public Location GetUserLocationById(int id)
        {
            return GetById(id);
        }

        public List<Location> GetUserLocations(string Userid)
        {
            return GetAll(p => p.UserID == Userid).ToList();
        }

        public void UpdateUserLocation(Location location)
        {
            Update(location);
        }
    }
}
