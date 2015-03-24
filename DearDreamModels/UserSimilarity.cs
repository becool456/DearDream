using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearDreamModels
{
    public class UserSimilarity : Entity
    {
        public int Id { get; set; }

        [DisplayName("用户")]
        public int UserId { get; set; }

        [DisplayName("相似用户")]
        public int SimilarUserId { get; set; }

        [DisplayName("优先级")]
        public int Priority { get; set; }
    }
}
