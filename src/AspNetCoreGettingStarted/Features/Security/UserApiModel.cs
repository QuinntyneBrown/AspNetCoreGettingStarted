using AspNetCoreGettingStarted.Model;

namespace AspNetCoreGettingStarted.Features.Security
{
    public class UserApiModel
    {        
        public int UserId { get; set; }
        public string UserName { get; set; }

        public static TModel FromUser<TModel>(User user) where
            TModel : UserApiModel, new()
        {
            var model = new TModel();
            model.UserId = user.UserId;
            model.UserName = user.UserName;
            return model;
        }

        public static UserApiModel FromUser(User user)
            => FromUser<UserApiModel>(user);
    }
}
