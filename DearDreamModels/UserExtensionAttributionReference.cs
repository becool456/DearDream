using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DearDreamModels
{
    public class UserExtensionAttributionReference
    {
        [Key]
        public int AttributeId { get; set; }

        [DisplayName("属性组名称")]
        public string AttributeGroupName { get; set; }

        [DisplayName("属性名称")]
        public string AttributeName { get; set; }
        [DisplayName("属性编码")]
        public string AttributeCode { get; set; }
    }
}
