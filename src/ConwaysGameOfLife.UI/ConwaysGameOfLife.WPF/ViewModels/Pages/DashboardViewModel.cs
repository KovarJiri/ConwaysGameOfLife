// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using ConwaysGameOfLife.WPF.Services;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace ConwaysGameOfLife.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private int _numberOfRows = 200;

        [ObservableProperty]
        private int _numberOfColumns = 200;
        
        private GameSettingsService _gameSettingsService;

        [RelayCommand]
        public void SetParameters()
        {
            _gameSettingsService.SetUserGameSettings(new WPF.Models.GameSettings()
            {
                UserBoardXSize = NumberOfColumns,
                UserBoardYSize = NumberOfRows
            });
        }

        public void OnNavigatedTo()
        {
        }

        public void OnNavigatedFrom()
        {
            SetParameters();
        }

        public DashboardViewModel(GameSettingsService gameSettingsService)
        {
            _gameSettingsService = gameSettingsService;
        }
    }
}
