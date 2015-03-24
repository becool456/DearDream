using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DearDreamModels
{
    public class UserInterest : Entity
    {
        public int Id { get; set; }

        [DisplayName("绑定用户")]
        public int UserId { get; set; }

        [DisplayName("新闻类别ID")]
        public int NewsId { get; set; }

        [DisplayName("比重")]
        [Range(0,1)]
        public double Proportion { get; set; }

        [DisplayName("当前权重总值")]
        public double CurentWeight { get; set; }

        [DisplayName("更新时间")]
        public DateTime UpdateTime { get; set; }
    }
}
