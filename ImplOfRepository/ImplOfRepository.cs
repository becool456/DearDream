using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DearDreamModels.Helper;
using DearDreamModels;
using DearDreamDAL;
using Ninject;

namespace ImplOfRepository
{
    #region 仓储接口

    /// <summary>
    /// 仓储用户权限接口
    /// </summary>
    public interface IAuthorityRepository : IRepository<Authority> { }

    /// <summary>
    /// 仓储新闻类接口
    /// </summary>
    public interface INewsRepository : IRepository<News> { }

    /// <summary>
    /// 仓储角色权限映射接口
    /// </summary>
    public interface IRoleAuthorityMapRepository : IRepository<RoleAuthorityMap> { }
    /// <summary>
    /// 仓储用户接口
    /// </summary>
    public interface IUserRepository : IRepository<User> { }

    /// <summary>
    /// 仓储用户行为接口
    /// </summary>
    public interface IUserBehaviorRepository : IRepository<UserBehavior> { }

    /// <summary>
    /// 仓储用户具体信息接口
    /// </summary>
    public interface IUserDetailRepository : IRepository<UserDetail> { }

    /// <summary>
    /// 仓储用户兴趣分布接口
    /// </summary>
    public interface IUserInterestRepository : IRepository<UserInterest> { }

    /// <summary>
    /// 仓储角色接口
    /// </summary>
    public interface IRoleRepository : IRepository<Role> { }

    /// <summary>
    /// 仓储新闻类别接口
    /// </summary>
    public interface INewsCategoryRepository : IRepository<NewsCategory> { }

    /// <summary>
    /// 仓储用户行为名称接口
    /// </summary>
    public interface IBehaviorTypeRepository : IRepository<TBehaviorName> { }

    public interface INewsSimilarityMapRepository : IRepository<NewsSimilarityMap> { }

    //public interface IUserExtensionAttributeReferenceRepository : IRepository<UserExtensionAttributionReference> { }

    public interface IUserExtensionInfoRepository : IRepository<UserExtensionInfo> { }


    public interface IUserHobbyMapRepository : IRepository<UserHobbyMap> { }

    public interface ITopicRepository : IRepository<NewsTopic> { }

    #endregion

    #region 仓储接口实现

    /// <summary>
    /// 仓储用户权限实现
    /// </summary>

    public class AuthorityRepository : RepositoryBase<Authority>, IAuthorityRepository { }

    /// <summary>
    /// 仓储新闻类实现
    /// </summary>
    public class NewsRepository : RepositoryBase<News>, INewsRepository { }

    /// <summary>
    /// 仓储角色权限映射实现
    /// </summary>
    public class RoleAuthorityMapRepository : RepositoryBase<RoleAuthorityMap>, IRoleAuthorityMapRepository { }

    /// <summary>
    /// 仓储用户实现
    /// </summary>
    public class UserRepository : RepositoryBase<User>, IUserRepository { }

    /// <summary>
    /// 仓储用户行为实现
    /// </summary>
    public class UserBehaviorRepository : RepositoryBase<UserBehavior>, IUserBehaviorRepository { }

    /// <summary>
    /// 仓储用户具体信息实现
    /// </summary>
    public class UserDetailRepository : RepositoryBase<UserDetail>, IUserDetailRepository { }

    /// <summary>
    /// 仓储用户兴趣分布实现
    /// </summary>
    public class UserInterestRepository : RepositoryBase<UserInterest>, IUserInterestRepository { }

    /// <summary>
    /// 仓库角色实现
    /// </summary>
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository { }

    public class NewsCategoryRepository : RepositoryBase<NewsCategory>, INewsCategoryRepository { }

    public class BehaviorTypeRepository : RepositoryBase<TBehaviorName>, IBehaviorTypeRepository { }

    public class NewsSimilarityMapRepository : RepositoryBase<NewsSimilarityMap>, INewsSimilarityMapRepository { }

    //public class UserExtensionAttributeReferenceRepository : RepositoryBase<UserExtensionAttributionReference>, IUserExtensionAttributeReferenceRepository { }

    public class UserExtensionInfoRepository : RepositoryBase<UserExtensionInfo>, IUserExtensionInfoRepository { }

    public class UserHobbyMapRepository : RepositoryBase<UserHobbyMap>, IUserHobbyMapRepository { }

    public class TopicRepository : RepositoryBase<NewsTopic>, ITopicRepository { }
    
    #endregion

}
