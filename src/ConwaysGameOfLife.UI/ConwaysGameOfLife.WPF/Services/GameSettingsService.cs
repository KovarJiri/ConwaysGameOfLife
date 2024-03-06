using ConwaysGameOfLife.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.WPF.Services
{
    public class GameSettingsService
    {
        GameSettings userGameSettings;
        public GameSettingsService()
        {
            userGameSettings = new GameSettings();
        }

        public void SetUserGameSettings(GameSettings settings)
        {
            userGameSettings = settings;
        }

        public GameSettings GetUserGameSettings()
        {
            return userGameSettings;
        }
    }
}
