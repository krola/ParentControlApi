using System.Collections.Generic;
using System.Linq;

public class UserRepository 
        : GenericRepository<ParentControlContext, User>, IUserRepository  {
    public List<User> GetAllUsers() => GetAll().ToList();
}