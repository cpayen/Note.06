using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Entities.Base;
using Note.Core.Enums;
using System;
using System.Threading.Tasks;

namespace Note.Core.Identity
{
    public static class Roles
    {
        public static string AppUser = "APP_USER";
        public static string AppAdmin = "APP_ADMIN";
    }

    public class Auth
    {
        #region Props

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ICurrentUser _currentUser;
        private User _currentUserEntity;

        #endregion

        #region Ctor

        public Auth(IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        #endregion

        #region Public ICurrentUser informations

        public bool IsAuthenticated
        {
            get => _currentUser.IsAuthenticated;
        }
        public string Login
        {
            get => _currentUser.Login ?? "Anonymous";
        }
        public string FirstName
        {
            get => _currentUser.FirstName;
        }
        public string LastName
        {
            get => _currentUser.LastName;
        }
        public string Email
        {
            get => _currentUser.Email;
        }
        public bool HasRole(string role) => _currentUser.HasRole(role);

        public string FullName
        {
            get =>
                !string.IsNullOrEmpty(_currentUser.FirstName) && !string.IsNullOrEmpty(_currentUser.LastName) ?
                $"{_currentUser.FirstName} {_currentUser.LastName}" :
                _currentUser.Login;
        }

        #endregion

        #region Public access informations

        public bool IsAdmin() => _currentUser.HasRole(Roles.AppAdmin);

        public bool Owns(IOwned item)
        {
            if (item == null)
            {
                //TODO: throw exception properly
                throw new ArgumentNullException();
            }

            return item.Owner.Login == _currentUser.Login;
        }

        public bool CanRead(IOwned item)
        {
            return item.ReadAccess == Access.Public || Owns(item);
        }

        public bool CanWrite(IOwned item)
        {
            return item.WriteAccess == Access.Public || Owns(item);
        }

        #endregion

        #region Internal entity

        internal async Task<User> GetCurrentUserEntityAsync()
        {
            if (_currentUserEntity == null)
            {
                if (_currentUser.IsAuthenticated)
                {
                    _currentUserEntity = await EnsureUserAsync(
                        _currentUser.Login,
                        _currentUser.FirstName,
                        _currentUser.LastName,
                        _currentUser.Email
                    );
                }
                else
                {
                    _currentUserEntity = new User();
                }
            }

            return _currentUserEntity;
        }

        private async Task<User> EnsureUserAsync(string login, string firstName, string lastName, string email)
        {
            var repo = _unitOfWork.UserRepository;
            var user = await repo.FindAsync(login);

            if (user == null)
            {
                user = new User
                {
                    Login = login,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    CreatedAt = DateTime.Now
                };

                user = repo.Create(user);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                var hasChanged = false;

                if (user.FirstName != firstName)
                {
                    hasChanged = true;
                    user.FirstName = firstName;
                }
                if (user.LastName != lastName)
                {
                    hasChanged = true;
                    user.LastName = lastName;
                }
                if (user.Email != email)
                {
                    hasChanged = true;
                    user.Email = email;
                }

                if (hasChanged)
                {
                    user.UpdatedAt = DateTime.Now;
                    repo.Update(user);
                    await _unitOfWork.SaveAsync();
                }
            }

            return user;
        }

        #endregion
    }
}
