using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecomSysCore;
using RecomSysCore.ImplOfRecom;
using System.Threading;
using Ninject;
using DearDreamModels;
using DearDreamModels.Helper;
using ImplOfRepository;

namespace RecomConsole
{
    class Program
    {
        static SqlDbContext db = new SqlDbContext();
        static void Main(string[] args)
        {
            AsyTools.RunMyTimerMachine();

            //UserAttributeSolution u = new UserAttributeSolution();
            //u.CountSimilarity();
            //Console.WriteLine("Succeed!");
            

        }
    }
}
