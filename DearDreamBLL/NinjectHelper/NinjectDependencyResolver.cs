using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;

using DearDreamModels;
using DearDreamDAL;
using ImplOfRepository;
using RecomSysCore.ImplOfRecom;
using RecomSysCore;
using Ninject;

using DearDreamBLL.ImplOfService;

namespace DearDreamBLL.NinjectHelper
{
    /// <summary>
    /// 实现注入接口IDenpendencyResolver
    /// </summary>
    public class NinjectDependencyResolver
       : IDependencyResolver
    {
        /// <summary>
        /// Ninject核心类
        /// </summary>
        private Ninject.IKernel kernel;

        /// <summary>
        /// 构造函数
        /// </summary>
        public NinjectDependencyResolver()
        {
            this.kernel = new Ninject.StandardKernel();
            this.kernel.Settings.InjectNonPublic = true;
            this.AddBindings();
        }

        /// <summary>
        /// 添加接口与继承类绑定
        /// </summary>
        private void AddBindings()
        {
            this.kernel.Bind<IUnitOfWork>().To<SqlUnitOfWorkContext>();

            //设置SqlDbContext为单例模式
            this.kernel.Bind<DbContext>().To<SqlDbContext>();

            this.kernel.Bind<IUserRepository>().To<UserRepository>();

            this.kernel.Bind<IUserInterestRepository>().To<UserInterestRepository>();

            this.kernel.Bind<IUserDetailRepository>().To<UserDetailRepository>();

            this.kernel.Bind<IRoleRepository>().To<RoleRepository>();

            this.kernel.Bind<IRoleAuthorityMapRepository>().To<RoleAuthorityMapRepository>();

            this.kernel.Bind<INewsCategoryRepository>().To<NewsCategoryRepository>();

            this.kernel.Bind<IUserBehaviorRepository>().To<UserBehaviorRepository>();

            this.kernel.Bind<INewsRepository>().To<NewsRepository>();

            this.kernel.Bind<IAuthorityRepository>().To<AuthorityRepository>();

            this.kernel.Bind<IBehaviorTypeRepository>().To<BehaviorTypeRepository>();

            this.kernel.Bind<INewsSimilarityMapRepository>().To<NewsSimilarityMapRepository>();

            this.kernel.Bind<IUserExtensionInfoRepository>().To<UserExtensionInfoRepository>();

            this.kernel.Bind<IUserHobbyMapRepository>().To<UserHobbyMapRepository>();

            this.kernel.Bind<ITopicRepository>().To<TopicRepository>();

            this.kernel.Bind<IAccountService>().To<AccountService>();

            this.kernel.Bind<INewsService>().To<NewsService>();

            this.kernel.Bind<ICookieService>().To<CookieService>();

            this.kernel.Bind<ISqlTreament>().To<SqlTreatment>();

            this.kernel.Bind<IAcquireBehavior>().To<AcquireBehavior>();

            this.kernel.Bind<IAddUpBehaivor>().To<AddUpBehavior>();

            this.kernel.Bind<IAcquireRecom>().To<AcquireRecom>();

        }

        /// <summary>
        /// 获取指定类型的对象实例
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }

        /// <summary>
        /// 获取指定类型的对象实例的集合
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType);
        }
    }
}
