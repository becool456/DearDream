using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.Entity;
using ImplOfRepository;
using DearDreamModels;
using DearDreamModels.Helper;

namespace RecomConsole
{
    public class NewsSimilaritySolution
    {
        private SqlDbContext db = new SqlDbContext();

        private int similarityNum = 10;

        #region 接口函数
        /// <summary>
        /// 统计每条新闻的相似新闻
        /// </summary>
        public int CollectSimilarity(bool isFirstCollected = false)
        {
            IEnumerable<NewsCategory> typeDB = db.NewsCategorys;
            foreach(var type in typeDB)
            {
                IEnumerable<News> typeNewsContainer = db.NewsContainer.Where( o => o.Category == type.Name).ToList();
                if(typeNewsContainer == null)
                    throw new Exception("找不到指定类别的新闻");
                foreach(var item in typeNewsContainer)
                {
                    //LinkedList<NewsSimilarityMap> itemMapContainer = new LinkedList<NewsSimilarityMap>();
                    Dictionary<int, int> updateMapNews = new Dictionary<int, int>();
                    var tmpList = new List<News>(); tmpList.Add(item);
                    IEnumerable<News> exceptedNewsContainer = null;
                    if(isFirstCollected)
                       exceptedNewsContainer = typeNewsContainer.Except(tmpList);
                    else
                        exceptedNewsContainer = typeNewsContainer.Where(o => o.Time > item.Time).Except(tmpList);
                    foreach(var readyItem in exceptedNewsContainer)
                    {
                        //两新闻间相似度计算策略
                        var newMap = new NewsSimilarityMap();
                        newMap.OwingNewsId = item.Id;
                        int similarity = 0;
                        //如果新闻来源相同，则相似度加2
                        if (IsSimilaritySource(item.Source, readyItem.Source))
                            similarity = 2;        
                        else
                            similarity = 0;
                        similarity += GetKeyWordSimilarity(item.Keywords, readyItem.Keywords);
                        if(similarity != 0)
                            updateMapNews.Add(readyItem.Id, similarity);
                    }
                    NewsSimilarityMap map = db.NewsSimilarityMaps.FirstOrDefault(o => o.OwingNewsId == item.Id);
                    if (map == null)
                        throw new Exception(string.Format("不存在ID为{0}的新闻相似度映射", item.Id));
                    var oldMapNews = Serialization.ConvertStrToDic(map.StrRelatedNewsIds, map.StrSimilarities);
                    var currentMapNews = updateMapNews.Union(oldMapNews).OrderByDescending(o => o.Value).Take(similarityNum);
                    //out修饰符无法对属性使用
                    string tmpRelatedNewIds, tmpSimilarities;
                    Serialization.ConvertEnumToStr(currentMapNews, out tmpRelatedNewIds, out tmpSimilarities);
                    map.StrRelatedNewsIds = tmpRelatedNewIds;
                    map.StrSimilarities = tmpSimilarities;
                    //保存修改
                    map.UpdateTime = DateTime.Now;
                    db.Entry(map).State = EntityState.Modified;
                }
            }
            return db.SaveChanges();
            
        }

        public int SimilarityMapInitial()
        {
            DateTime lastUpdateTime;
            if(db.NewsSimilarityMaps.Any())
              lastUpdateTime = db.NewsSimilarityMaps.Max(o => o.UpdateTime);
            else
              lastUpdateTime =  new DateTime(2000,1,1);
            var newAddedNewsIDs = (from o in db.NewsContainer
                               where o.Time > lastUpdateTime
                               select o.Id).ToList();
            int currentUpdateCount = 0;
            foreach(var item in newAddedNewsIDs)
            {
                NewsSimilarityMap newMap = new NewsSimilarityMap
                {
                    OwingNewsId = item,
                    UpdateTime = new DateTime(2000,1,1)
                };
                db.NewsSimilarityMaps.Add(newMap);
                currentUpdateCount++;
                if (currentUpdateCount % 1000 == 0)
                    Console.WriteLine("{0}条新闻相似度映射已被添加！", currentUpdateCount);
            }
            return db.SaveChanges();
        }

        #endregion

        #region 辅助函数
        /// <summary>
        /// 判断新闻关键词是否重合
        /// </summary>
        /// <param name="preStrings"></param>
        /// <param name="backStrings"></param>
        /// <returns></returns>
        protected int GetKeyWordSimilarity(string preStrings,string backStrings)
        {
            if (String.IsNullOrEmpty(preStrings) || String.IsNullOrEmpty(backStrings))
                return 0;
            string [] preArray = Regex.Split(preStrings.Trim(), " ", RegexOptions.IgnoreCase);
            string[] backArray = Regex.Split(backStrings.Trim(), " ", RegexOptions.IgnoreCase);
            var totalEnum = preArray.Union(backArray.AsEnumerable());
            return preArray.Length + backArray.Length - totalEnum.Count();
        }

        /// <summary>
        /// 判断新闻来源是否相同
        /// </summary>
        /// <param name="preSource"></param>
        /// <param name="backSource"></param>
        /// <returns></returns>
        protected bool IsSimilaritySource(string preSource,string backSource)
        {
            if (String.IsNullOrEmpty(preSource) || String.IsNullOrEmpty(backSource))
                throw new Exception("比对字符串无效！");
            if(preSource.Trim().Equals(backSource.Trim()))
                return true;
            else
                return false;
        }
        #endregion
    }
}
