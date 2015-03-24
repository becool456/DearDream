using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DearDreamModels
{
    public class NewsCategory : Entity
    {
        public int Id { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [DisplayName("类别名")]
        public string Name { get; set; }
    }
}
