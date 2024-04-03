using CRUD1_M.DTOs;
using CRUD1_M.Entities;
using CRUD1_M.Repositories.Impl;

namespace CRUD1_M.Services.Impl
{
    public class AuthServiceImpl
    {
        private AuthRepositoryImpl authRepository;
        public AuthServiceImpl(AuthRepositoryImpl authRepository)
        {
            this.authRepository = authRepository;
        }
        public User register(RegisterDTO dto)
        {
            User user = new User();
            user.name = dto.name;
            user.email = dto.email;
            user.password = dto.password;
            if(this.authRepository.register(user) == null)
            {
                return null;
            }
            return this.authRepository.register(user);
        }

        public User login(LoginDTO dto, IConfiguration configuration)
        {
            User user = new User();
            user.email = dto.email;
            user.password = dto.password;
            if(this.authRepository.login(user, configuration) == null)
            {
                return null;
            }
            return this.authRepository.login(user, configuration);
        }
    }
}
