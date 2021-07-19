using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.ViewModels;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories
{
    public interface IUserRepository : IGernericRepository<User>
    {

        bool IsExistUserName(string username);
        bool IsExistEmail(string email);
        bool IsExistPhoneNumber(string phonenumber);
        string GetUserByUserName(string username);
        SideBarUserPanelViewModel GetSideBarUserPanelData(string username);
        Models.Entities.User.User GetUserByForgotPasswordCode(string Code);
        void DeleteUserAvatar(User user);
        bool? GetEmployeeAcceptedPossition(string username);
        User UpdateUserAvatar(User user, EditUserInAdminPanel userEdited);
    }
}
