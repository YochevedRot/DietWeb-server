using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietWeb.Core.Models
{
    public class DietaryPreference
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FoodName { get; set; } = "";
        public int Like { get; set; }

    }
}
