using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DearDreamModels.Helper
{
    /// <summary>
    /// 教育水平
    /// </summary>
    public class Educationlevel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; } 
    }

    /// <summary>
    /// 婚姻状况
    /// </summary>
    public class Maritalstatus
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; } 
    }

    public class Gender
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; } 
    }

    public class IndustryCategory
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }

    public class Pronvice
    {
        public int Id { get; set; }

        public string Code { get; set; }
        
        public string Name { get; set; }

    }

    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ParentCode { get; set; }
    }


}
