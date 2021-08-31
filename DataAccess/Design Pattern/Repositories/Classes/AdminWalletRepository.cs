﻿using DataAccess.Design_Pattern.GenericRepositories;
using DataAccess.Design_Pattern.Repositories.Interfaces;
using DataContext.Context;
using Models.Entities.Factor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class AdminWalletRepository : GenericRepository<AdminWallet>, IAdminWalletRepository
    {
        private readonly KhadamatContext _db;

        public AdminWalletRepository(KhadamatContext db) : base(db)
        {
            _db = db;
        }
    }
}
