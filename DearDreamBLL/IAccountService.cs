using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DearDreamModels;
using DearDreamModels.ViewModel;
using DearDreamModels.Helper;

namespace DearDreamBLL
{
    /// <summary>
    /// 用户管理接口
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 用户登陆帐号验证
        /// </summary>
        /// <param name="viewModel">用户登陆基本信息</param>
        /// <returns>帐号密码是否正确</returns>
        bool Login(UserLoginViewModel viewModel);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="viewModel">用户注册基本信息</param>
        /// <returns>注册是否成功</returns>
        OperationResult Register(UserRegisterViewModel viewModel);

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>更改密码结果</returns>
        bool UpdatePassword(string userName, string password);

        /// <summary>
        /// 验证用户密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>验证结果</returns>
        OperationResult CheckPassword(string userName, string password);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        OperationResult UpdateUserInfo(UserDetail userDetail,bool isSave = true);

        /// <summary>
        /// 获取用户的具体信息
        /// </summary>
        /// <param name="id">用户标识</param>
        /// <returns>实体对象</returns>
        UserDetail GetUserInfo(int id);

        /// <summary>
        /// 更新用户扩展信息
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        OperationResult UpdateUserExtensionInfo(UserExtensionInfo ui);

        /// <summary>
        /// 获取ID对应的用户扩展信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserExtensionInfo GetUserExtensionInfo(int id);

        /// <summary>
        /// 获取指定用户对应的兴趣偏好集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<NewsCategory> GetUserHobbyMaps(int userId);

        /// <summary>
        /// 更新用户兴趣偏好映射集合
        /// </summary>
        /// <param name="maps"></param>
        /// <returns></returns>
        OperationResult UpdateUserHobbyMaps(IEnumerable<UserHobbyMap> maps, int userId);
        
        IQueryable<Educationlevel> GetEducationlevels();

        IQueryable<Gender> GetGenders();

        IQueryable<Location> GetLocations(int ? pronviceID);

        IQueryable<Location> GetChildLocationsByCode(string parentCode);

        IQueryable<Location> GetChildLocationsByName(string parentName);

        IQueryable<Pronvice> GetPrinvice();

        IQueryable<Maritalstatus> GetMaritalstatus();

        IQueryable<IndustryCategory> GetIndustryCategories();

    }
}
