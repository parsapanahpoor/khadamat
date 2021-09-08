using DataAccess.Design_Pattern.GenericRepositories;
using Microsoft.AspNetCore.Http;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Interfaces
{
    public interface IEmployeeRepository : IGernericRepository<EmployeeDocuments>
    {

        void AddEmployeeDocument(string userid);
        EmployeeDocuments GetEmployeeDocument(string userid);
        void UpdateEmployeeDocumentFromEmployeePanel(EmployeeDocuments employee, IFormFile Picture, IFormFile Certificate);
        int GetEmployeeInfoPossition(string userid);
        void UpdateEmployeeInfoFromAdminPanel(EmployeeDocuments employee);
        bool IsExistEmployeeDocument(string EmployeeId);
    }
}
