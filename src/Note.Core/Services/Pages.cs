using Note.Core.Data;
using Note.Core.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Note.Core.Services
{
    public class Pages
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAuth _auth;

        public Pages(IUnitOfWork unitOfWork, IAuth auth)
        {
            _unitOfWork = unitOfWork;
            _auth = auth;
        }
    }
}
