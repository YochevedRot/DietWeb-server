using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietWeb.Core.Services
{
    public interface IPersonalTrainerService
    {
        Task<string> GetTrainerResponseAsync(string personality, string input, string userId);
    }


}
