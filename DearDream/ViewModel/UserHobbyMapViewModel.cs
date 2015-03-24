using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DearDreamModels;

namespace DearDream.ViewModel
{
    public class UserHobbyMapViewModel
    {
        public int UserId { get; set; }

        public IEnumerable<NewsCategory> Hobbies { get; set; }

        public IEnumerable<NewsCategory> HobbySelctions { get; set; }
    }
}