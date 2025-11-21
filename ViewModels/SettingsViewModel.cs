using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.ViewModels
{
    public class SettingsViewModel
    {
        public string SelectedOption { get; set; }

        public AccountSettingsViewModel AccountSettings { get; set; }
        public ChangePasswordViewModel ChangePassword { get; set; }
        public DeleteAccountViewModel DeleteAccount { get; set; }
    }
}