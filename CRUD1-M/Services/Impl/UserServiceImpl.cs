using CRUD1_M.DTOs;
using CRUD1_M.Entities;
using CRUD1_M.Repositories.Impl;

namespace CRUD1_M.Services.Impl
{
    public class UserServiceImpl
    {
        private UserRepository userRepository;
        public UserServiceImpl(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public User register(RegisterDTO dto)
        {
            User user = new User();
            user.name = dto.name;
            user.email = dto.email;
            user.password = dto.password;
            return userRepository.register(user);
        }

        public User login(LoginDTO dto, IConfiguration configuration)
        {
            User user = new User();
            user.email = dto.email;
            user.password = dto.password;
            return userRepository.login(user, configuration);
        }
    }
}
