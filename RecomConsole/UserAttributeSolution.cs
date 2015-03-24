using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DearDreamModels;
using ImplOfRepository;

namespace RecomConsole
{
    public class SimilarityMap
    {
        public int UserId { get; set; }
        public int Similarity { get; set; }
    }
    public class UserAttributeSolution
    {
        private SqlDbContext db = new SqlDbContext();

        private int SimilarityUserSettingCount = 10;

        //获取与当前用户具有相同兴趣爱好的用户
        public IEnumerable<int> SelectOtherUsers(User currentUser)
        {
            IList<int> currentUserHobby = db.UserHobbyMaps.Where(x => x.UserId == currentUser.Id).Select(x => x.CategoryId).ToList();
            IEnumerable<int> userIds = new List<int>(currentUser.Id);
            foreach (var Hobby in currentUserHobby)
            {
                IEnumerable<int> tempUserIds = db.UserHobbyMaps.Where(x => x.CategoryId == Hobby).Select(x => x.UserId);
                userIds = userIds.Union(tempUserIds);
            } 
            return userIds;
        }
        //零数据情况下建立用户相似度数据集
        public void CountSimilarity()
        {
            IEnumerable<User> users = db.Users;
            foreach (var user in users)
            {
                IList<SimilarityMap> smilarityMap = new List<SimilarityMap>();
                UserExtensionInfo currentuserExtensionInfo = db.UserExtensionInfos.Single(x => x.UserId == user.Id);
                IEnumerable<int> otherUsersId = SelectOtherUsers(user);
                foreach (var otherUserid in otherUsersId)       //其他用户
                {
                    UserExtensionInfo otherExtensionInfo = db.UserExtensionInfos.Single(x => x.UserId == otherUserid);
                    int similarityDegree = AcquireSimilarityBetween(currentuserExtensionInfo, otherExtensionInfo);
                    SimilarityMap newsimilarityMap = new SimilarityMap
                    {
                        UserId = otherUserid,
                        Similarity = similarityDegree
                    };
                    smilarityMap.Add(newsimilarityMap);
                }
                IEnumerable<SimilarityMap> similarity = smilarityMap.OrderByDescending(x => x.Similarity).Take(SimilarityUserSettingCount);
                int priority = 1;
                foreach (var similarityMap in similarity)
                {
                    UserSimilarity userSimilarity = new UserSimilarity
                    {
                        UserId = user.Id,
                        SimilarUserId = similarityMap.UserId,
                        Priority = priority
                    };
                    db.UserSimilarities.Add(userSimilarity);
                    priority++;
                }
            }
            db.SaveChanges();
        }
        //两两用户相似度计算策略
        private int AcquireSimilarityBetween(UserExtensionInfo preUser, UserExtensionInfo backUser)
        {
            int degree = 0;
            string preCode = preUser.SumCode ; 
            string backCode = backUser.SumCode ;
            for(int i = 0 ; i < preUser.SumCode.Length; i++)
            {
                if (preCode[i] == backCode[i])
                    degree++;
            }
            int missedDegre = 0; 
            #region 考虑用户尚未填写的扩展信息
            string missedStr = "未设置";
            if (preUser.EducationlevelName == missedStr || backUser.EducationlevelName == missedStr)
                missedDegre += preUser.EducationlevelCode.Length;
            if (preUser.MaritalstatusName == missedStr || backUser.MaritalstatusName == missedStr)
                missedDegre += preUser.MaritalstatusCode.Length;
            if (preUser.GenderName == missedStr || backUser.GenderName == missedStr)
                missedDegre += preUser.GenderCode.Length;
            if (preUser.IndustryCategoryName == missedStr || backUser.IndustryCategoryName == missedStr)
                missedDegre += preUser.IndustryCategoryCode.Length;
            if (preUser.PronviceName == missedStr || backUser.PronviceName == missedStr)
                missedDegre += preUser.PronviceCode.Length;
            if (preUser.LocationName == missedStr || backUser.LocationName == missedStr)
                missedDegre += preUser.LocationCode.Length;
            #endregion
            degree = degree - missedDegre;
            //数值型属性相似度
            if(preUser.BirthTime != DateTime.MinValue&&backUser.BirthTime != DateTime.MinValue)
                degree = degree - Math.Abs(preUser.Age - backUser.Age);
            //值越大，相似度越大
            return degree; 
        }
    }
}
