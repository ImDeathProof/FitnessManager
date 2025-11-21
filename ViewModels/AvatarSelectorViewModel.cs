using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.ViewModels
{
    public class AvatarSelectorViewModel
    {
        public string SelectedAvatarUrl { get; set; }
        public List<string>? AvailableAvatarUrls { get; set; }
    }
}