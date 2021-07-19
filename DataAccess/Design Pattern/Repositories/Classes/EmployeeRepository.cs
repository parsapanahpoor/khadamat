using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataContext.Context;
using Microsoft.AspNetCore.Http;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Genarator;
using Utilities.Security;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class EmployeeRepository : GenericRepository<EmployeeDocuments>, IEmployeeRepository
    {
        private readonly KhadamatContext _db;

        public EmployeeRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }

        public void AddEmployeeDocument(string userid)
        {
            EmployeeDocuments employee = new EmployeeDocuments()
            {

                Userid = userid,
                PossitionId = 1
                

            };

            Add(employee);
        }

        public EmployeeDocuments GetEmployeeDocument(string userid)
        {
            return GetAll(p => p.Userid == userid)
                        .Single();
        }

        public int GetEmployeeInfoPossition(string userid)
        {
            return GetAll(p => p.Userid == userid)
                        .Select(p => p.PossitionId).Single();
        }

        public void UpdateEmployeeDocumentFromEmployeePanel(EmployeeDocuments employee, IFormFile Picture, IFormFile Certificate)
        {
            if (Picture != null && Picture.IsImage())
            {
                if (employee.PersonalPicture != null)
                {

                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/EmployeeDocuments", employee.PersonalPicture);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                }
                employee.PersonalPicture = NameGenerator.GenerateUniqCode() + Path.GetExtension(Picture.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/EmployeeDocuments", employee.PersonalPicture);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Picture.CopyTo(stream);
                }

            }

            if (Certificate != null && Certificate.IsImage())
            {
                if (employee.EmployeeCertificate != null)
                {

                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/EmployeeDocuments", employee.EmployeeCertificate);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                }
                employee.EmployeeCertificate = NameGenerator.GenerateUniqCode() + Path.GetExtension(Certificate.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/EmployeeDocuments", employee.EmployeeCertificate);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Certificate.CopyTo(stream);
                }

            }

            Update(employee);

        }
    }
}
