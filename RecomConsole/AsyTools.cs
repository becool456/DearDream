using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecomSysCore;
using RecomSysCore.ImplOfRecom;
using System.Threading;

using Ninject;

namespace RecomConsole
{
    public static class AsyTools
    {
        static UserBehaviorSolution ub = new UserBehaviorSolution();

        static NewsSimilaritySolution ns = new NewsSimilaritySolution();

        static NewsTopicSolution ts = new NewsTopicSolution();
        //创建异步线程处理推荐模块
        //public static void RunAsyRecomThread()
        //{
        //    ThreadPool.SetMaxThreads(1000, 1000);
        //    //创建定时器
        //    TimerCallback addUpBehaviorCallback = new TimerCallback(AsyAddUpBehavior);
        //    Timer addUpBehaviorTimer = new Timer(addUpBehaviorCallback, null, 1000, 3000);
        //}

        public static void RunMyTimerMachine()
        {
            int runTimes = 1;
            while(true)
            {
                //AsyCollectNewsSimilarity(runTimes);
                AsyCollectNewsTopic();
                Thread.Sleep(50000);
                runTimes++;
            }
        }

        //更新新闻话题
        private static void AsyCollectNewsTopic()
        {
            int relatedRows = ts.CollectNewsTopic();
            Console.Write(string.Format("更新时间：{0}", DateTime.Now));
            if (relatedRows > 0)
                Console.WriteLine(string.Format("新闻话题更新了{0}条记录", relatedRows));
            else
                Console.WriteLine();
        }

        //异步调用处理用户行为
        private static void AsyAddUpBehavior()
        {
            int relatedRows = ub.UpdateBehaviorToLateset();
            Console.Write(string.Format("更新时间：{0}", DateTime.Now));
            if (relatedRows > 0)
                Console.WriteLine(string.Format("用户行为集合更新了{0}条记录", relatedRows));
            else
                Console.WriteLine();
        }

        private static void AsyCollectNewsSimilarity(int runTimes)
        {
            Console.WriteLine("第{0}次运行相似度模块。", runTimes);
            int relatedRows = 0;
            relatedRows = ns.SimilarityMapInitial();
            Console.WriteLine("新闻相似度映射表更新完毕！共更新{0}条数据。",relatedRows);
            if(runTimes == 1)
                relatedRows = ns.CollectSimilarity(true);
            else
                relatedRows = ns.CollectSimilarity();
            Console.Write(string.Format("更新时间：{0}", DateTime.Now));
            Console.WriteLine(string.Format("项目相似度模块更新了{0}条记录", relatedRows));
        }
    }
}
