using System.Threading.Tasks;
using MessagingService.Entity;
using MessagingService.Repository;

namespace MessagingService.Service
{
    public static class Extensions
    {
        public static async Task<User> GetUser(this string username, IRepository<User> userRepository)
        {
            var user = await userRepository.GetOne(x => x.Name == username);
            return user;
        }
    }
}