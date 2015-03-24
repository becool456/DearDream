using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DearDreamModels;
using DearDreamModels.ViewModel;
using DearDreamModels.Helper;
using ImplOfRepository;

namespace DearDreamBLL.ImplOfService
{
    /// <summary>
    /// 用户管理服务类
    /// </summary>
    public class AccountService :IAccountService
    {
        #region 属性

        private SqlDbContext db = new SqlDbContext();

        /// <summary>
        /// 用户基本信息仓库
        /// </summary>
        [Ninject.Inject]
        private IUserRepository _userRepository { get; set; }

        /// <summary>
        /// 用户详细信息仓库
        /// </summary>
        [Ninject.Inject]
        private IUserDetailRepository _userDetailRepository { get; set; }

        /// <summary>
        /// 角色仓库
        /// </summary>
        [Ninject.Inject]
        private IRoleRepository _roleRepository { get; set; }

        /// <summary>
        /// 用户兴趣分布仓库
        /// </summary>
        [Ninject.Inject]
        private IUserInterestRepository _userInterestRepository { get; set; }

        [Ninject.Inject]
        private INewsCategoryRepository _newsCategoryRepository { get; set; }


        [Ninject.Inject]
        private IUserExtensionInfoRepository _userExtionsionInfoRepository { get; set; }

        [Ninject.Inject]
        private IUserHobbyMapRepository _userHobbyMapRepository { get; set; }

        #endregion 

        #region 方法
        /// <summary>
        /// 用户登陆验证
        /// </summary>
        /// <param name="viewModel">用户登陆基本信息</param>
        /// <returns>帐号和密码是否正确</returns>
        public bool Login(UserLoginViewModel viewModel)
        {
            User user = _userRepository.Entities.SingleOrDefault(u => u.LoginName == viewModel.LoginName);
            if(user == null)
            {
                return false;
            }
            else
            {
                if (user.PassWord == viewModel.PassWord)
                {
                    viewModel.UserId = user.Id;
                    viewModel.RoleName = user.RoleName;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="viewModel">用户注册基本信息</param>
        /// <returns>注册是否成功</returns>
        public OperationResult Register(UserRegisterViewModel viewModel)
        {
            if (_userRepository.Entities.Any(u => u.LoginName == viewModel.LoginName))
                return new OperationResult(false,"该用户名已存在");
            if (viewModel.PassWord != viewModel.PassWordConfirmed)
                return new OperationResult(false, "两次密码不匹配");
            #region 新建用户信息
            Role role = _roleRepository.Entities.First( r => r.RoleName == "general");
            User newUser = new User
            {
                LoginName = viewModel.LoginName,
                PassWord = viewModel.PassWord,
                RoleId = role.Id,
                RoleName = role.RoleName,      
                CreateAt = DateTime.Now         
            };
            _userRepository.Insert(newUser);
            newUser = _userRepository.Entities.Single(u => u.LoginName
                == viewModel.LoginName);
            //添加用户详细信息
            UserDetail userDetail = new UserDetail
            {
                NickName = viewModel.LoginName,
                Email = viewModel.Email,
                UserId = newUser.Id
            };
            _userDetailRepository.Insert(userDetail);
            //添加用户扩展信息
            string undefined = "未设置";
            UserExtensionInfo userInfo = new UserExtensionInfo
            {
                BirthTime = DateTime.MinValue,
                EducationlevelName = undefined,
                GenderName = undefined,
                IndustryCategoryName = undefined,
                LocationName = undefined,
                PronviceName = undefined,
                MaritalstatusName = undefined
            };
            UpdateUseExtensionAttributeCode(userInfo);
            userInfo.SumCode = GetUserExtensionSumCode(userInfo);
            userInfo.UserId = newUser.Id;
            _userExtionsionInfoRepository.Insert(userInfo);
            #endregion
            CreateUserInterest(newUser.Id);            
            return new OperationResult(true);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>更改密码结果</returns>
        public bool UpdatePassword(string userName, string password)
        {
            User user = _userRepository.Entities.SingleOrDefault(
                u => u.LoginName == userName);
            if (user == null)
                throw new Exception("视图查找不存在的用户名！");
            user.PassWord = password;
            int affectedRows = _userRepository.Update(user);
            if (affectedRows == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// /// <summary>
        /// 验证用户密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>验证结果</returns>
        public OperationResult CheckPassword(string userName, string password)
        {
            User user = _userRepository.Entities.SingleOrDefault(u => u.LoginName ==userName);
            if (user == null)
            {
                throw new Exception("试图查找不存在的用户名！");
            }
            
            if (user.PassWord == password)
            {
                return new OperationResult(true);  
            }
            return new OperationResult(false, "验证用户密码错误！");
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        public OperationResult UpdateUserInfo(UserDetail userDetail,bool isSave = true)
        {
            int affectedRows = _userDetailRepository.Update(userDetail,isSave);
            if (affectedRows == 0)
                return new OperationResult(false,"个人信息修改失败");
            else
                return new OperationResult(true);
        }

        /// <summary>
        /// 获取用户的具体信息
        /// </summary>
        /// <param name="id">用户标识</param>
        /// <returns>实体对象</returns>
        public UserDetail GetUserInfo(int id)
        {
            return _userDetailRepository.GetByKey(id);
        }

        public UserExtensionInfo GetUserExtensionInfo(int id)
        {
            var result = _userExtionsionInfoRepository.Entities.FirstOrDefault(
                o => o.UserId == id);
            if (result == null)
                throw new Exception("找不到指定ID的用户扩展信息!");
            return result;
        }

        /// <summary>
        /// 建立指定用户的兴趣模型
        /// </summary>
        /// <param name="userId">用户Id</param>
        public void CreateUserInterest(int userId)
        {
            var categories = _newsCategoryRepository.Entities;
            IList<UserInterest> userInterests = new List<UserInterest>();
            double size = categories.Count();
            foreach(var o in categories)
            {
                UserInterest newUserInterst = new UserInterest
                {
                    UserId = userId,
                    NewsId = o.Id,
                    UpdateTime = DateTime.Now,
                    Proportion = 1 / size,
                    CurentWeight = 0
                };
                userInterests.Add(newUserInterst);
            }
            _userInterestRepository.Insert(userInterests);
        }


        /// <summary>
        /// 更新用户扩展信息
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        public OperationResult UpdateUserExtensionInfo(UserExtensionInfo ui)
        {
            ui = UpdateUseExtensionAttributeCode(ui);
            if(ui.BirthTime != DateTime.MinValue)
                ui.Age = DateTime.Now.Year - ui.BirthTime.Year;
            int result = _userExtionsionInfoRepository.Update(ui);
            if (result != 0)
                return new OperationResult(true , "用户扩展信息更新成功");
            else
                return new OperationResult(false, "用户扩展信息更新失败");
        }

        public IQueryable<Educationlevel> GetEducationlevels()
        {
            return db.Educationlevels.OrderByDescending(o =>o.Id);
        }

        public IQueryable<Gender> GetGenders()
        {
            return db.Genders.OrderByDescending(o => o.Id);
        }

        public IQueryable<Location> GetLocations(int ? pronviceID)
        {
            string code = db.Pronvices.Find(pronviceID).Code;
            return GetChildLocationsByCode(code);
        }

        public IQueryable<Location> GetChildLocationsByCode(string parentCode)
        {
            return db.Locations.Where(o => o.ParentCode == parentCode).OrderByDescending(o => o.LocationId);
        }

        public IQueryable<Location> GetChildLocationsByName(string parentName)
        {
            var parentPro = db.Pronvices.FirstOrDefault(o => o.Name == parentName);
            string code = string.Empty;
            if (parentPro != null)
                code = parentPro.Code;
            else
            {
                var parentLoc = db.Locations.FirstOrDefault(o => o.Name == parentName);
                if (parentLoc == null)
                    throw new Exception("未找到指定名称的地区");
                code = parentLoc.Code;
            }
            return GetChildLocationsByCode(code);            
        }

        public IQueryable<Pronvice> GetPrinvice()
        {
            return db.Pronvices.OrderByDescending(o => o.Id);
        }

        public IQueryable<Maritalstatus> GetMaritalstatus()
        {
            return db.Maritalstatuses.OrderByDescending(o => o.Id);
        }

        public IQueryable<IndustryCategory> GetIndustryCategories()
        {
            return db.IndustryCategories.OrderByDescending(o => o.Id);
        }

        /// <summary>
        /// 获取指定用户对应的兴趣集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<NewsCategory> GetUserHobbyMaps(int userId)
        {
            //var maps =  _userHobbyMapRepository.Entities.Where( o => o.UserId == userId);
            //return _newsCategoryRepository.Entities.Where(o => maps.Any(m => m.CategoryId == o.Id));
            var maps = db.UserHobbyMaps.Where(o => o.UserId == userId);
            return db.NewsCategorys.Where(o => maps.Any(m => m.CategoryId == o.Id));
        }

        /// <summary>
        /// 更新用户兴趣偏好映射集合
        /// </summary>
        /// <param name="maps"></param>
        /// <returns></returns>
        public OperationResult UpdateUserHobbyMaps(IEnumerable<UserHobbyMap> maps,int userId)
        {
            var oldMaps = _userHobbyMapRepository.Entities.Where( o => o.UserId == userId);
            _userHobbyMapRepository.Delete(oldMaps);
            int affectedNum = _userHobbyMapRepository.Insert(maps);
            if(affectedNum != maps.Count())
            {
                return new OperationResult{
                    isSuccess = false,
                    AddInfo = "兴趣映射更新失败!"
                };
            }
            else
            {
                return new OperationResult{
                    isSuccess = true
                };
            }
                
        }

        #endregion

        #region 辅助接口函数
        /// <summary>
        /// 计算用户名称型属性的综合编码
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        private string GetUserExtensionSumCode(UserExtensionInfo ui)
        {
            string sumCode = string.Empty;
            sumCode += ui.PronviceCode;
            sumCode += ui.EducationlevelCode;
            sumCode += ui.GenderCode;
            sumCode += ui.MaritalstatusCode;
            sumCode += ui.IndustryCategoryCode;
            sumCode += ui.LocationCode;
            return sumCode;
        }

        /// <summary>
        /// 根据属性名更新用户的各项扩展属性编码
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        private UserExtensionInfo UpdateUseExtensionAttributeCode(UserExtensionInfo ui)
        {
            try
            {
                ui.EducationlevelCode = db.Educationlevels.Single(o => o.Name == ui.EducationlevelName).Code;
                ui.PronviceCode = db.Pronvices.Single(o => o.Name == ui.PronviceName).Code;
                ui.GenderCode = db.Genders.Single(o => o.Name == ui.GenderName).Code;
                ui.MaritalstatusCode = db.Maritalstatuses.Single(o => o.Name == ui.MaritalstatusName).Code;
                ui.IndustryCategoryCode = db.IndustryCategories.Single(o => o.Name == ui.IndustryCategoryName).Code;
                ui.LocationName = db.Locations.Single(o => o.Name == ui.LocationName).Code;
            }
            catch(Exception e)
            {
                throw e; 
            }
            return ui;
        }

        

        #endregion
    }
}
