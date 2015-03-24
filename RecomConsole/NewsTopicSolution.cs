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
    public class NewsTopicSolution
    {
        private SqlDbContext db = new SqlDbContext();
        
        /// <summary>
        /// 更新新闻的热点话题
        /// </summary>
        /// <returns>更新的记录数目</returns>
        public int CollectNewsTopic()
        {
            IQueryable<News> newsWaitingUpdate = null;
            if(db.NewsTopics.Count() ==  0 )
            {
                newsWaitingUpdate = db.NewsContainer;
            }
            else
            {
                 DateTime lastUpdateTime =db.NewsTopics.Max( o => o.UpdateTime); 
                newsWaitingUpdate = db.NewsContainer.Where(o => o.Time > lastUpdateTime);
            }
            foreach(var newItem in newsWaitingUpdate)
            {
                if(string.IsNullOrEmpty(newItem.Keywords))
                    continue;
                 IEnumerable<string> currentTopics = GetTopicAsEnum(newItem.Keywords);
                foreach(var topic in currentTopics)
                {
                    var oldTopic = db.NewsTopics.FirstOrDefault(o => o.TopicName == topic);
                    if(oldTopic == null)
                    {
                        db.NewsTopics.Add(new NewsTopic{
                            TopicName = topic,
                            UpdateTime = DateTime.Now,
                            Heat = 1 
                        });
                    }
                    else
                    {
                        oldTopic.Heat++;
                        db.Entry(oldTopic).State = EntityState.Modified;
                    }
                }
            }

            return db.SaveChanges();     
        }

        protected IEnumerable<string> GetTopicAsEnum(string topics)
        {
            string[] arrayTopics = Regex.Split(topics.Trim(), " ", RegexOptions.IgnoreCase);
            return arrayTopics.AsEnumerable();
        }
    }
}
