using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using DearDreamModels;
using DearDreamModels.Helper;

namespace ImplOfRepository
{
    /// <summary>
    /// 数据库上下文类
    /// </summary>
    public class SqlDbContext:DbContext
    {
        #region 属性
        /// <summary>
        /// 用户集
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 用户行为集
        /// </summary>
        public DbSet<UserBehavior> UserBehaviors { get; set; }

        /// <summary>
        /// 用户具体信息集
        /// </summary>
        public DbSet<UserDetail> UserDetails { get; set; }

        /// <summary>
        /// 用户兴趣分布集
        /// </summary>
        public DbSet<UserInterest> UserInterets { get; set; }


        /// <summary>
        /// 用户权限集
        /// </summary>
        public DbSet<Authority> Authorities { get; set; }

        /// <summary>
        /// 新闻集
        /// </summary>
        public DbSet<News> NewsContainer { get; set; }

        /// <summary>
        /// 新闻类别集
        /// </summary>
        public DbSet<NewsCategory> NewsCategorys { get; set; }

        /// <summary>
        /// 角色集
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// 角色与权限映射集
        /// </summary>
        public DbSet<RoleAuthorityMap> RoleAuthorityMaps { get; set; }

        /// <summary>
        /// 用户行为类型集
        /// </summary>
        public DbSet<TBehaviorName> TBahaviorNames { get; set; }

        /// <summary>
        /// 用户扩展信息集
        /// </summary>
        public DbSet<UserExtensionInfo> UserExtensionInfos { get; set; }

        /// <summary>
        /// 用户类别爱好映射集
        /// </summary>
        public DbSet<UserHobbyMap> UserHobbyMaps { get; set; }
        #endregion

        /// <summary>
        /// 用户属性相似度集
        /// </summary>
        public DbSet<UserSimilarity> UserSimilarities { get; set; } 

        /// <summary>
        /// 新闻相似度映射集合
        /// </summary>
        public DbSet<NewsSimilarityMap> NewsSimilarityMaps { get; set; }

        /// <summary>
        /// 新闻话题
        /// </summary>
        public DbSet<NewsTopic> NewsTopics { get; set; }

        #region 用户扩展属性
        public DbSet<Educationlevel> Educationlevels { get; set; }

        public DbSet<Pronvice> Pronvices { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<IndustryCategory> IndustryCategories { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Maritalstatus> Maritalstatuses { get; set; }

        #endregion
    }
}
