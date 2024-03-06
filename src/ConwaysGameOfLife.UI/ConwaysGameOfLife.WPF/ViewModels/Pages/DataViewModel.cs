// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using ConwaysGameOfFile.DTO;
using ConwaysGameOfLife.Services;
using ConwaysGameOfLife.WPF.Models;
using ConwaysGameOfLife.WPF.Services;
using System.Collections.ObjectModel;

using Wpf.Ui.Controls;

namespace ConwaysGameOfLife.ViewModels.Pages
{
    public partial class DataViewModel : ObservableObject, INavigationAware
    {
        // View helpers
        public double ActualCanvasHeight;
        public double ActualCanvasWidth;

        // Board data
        private Guid _boardGuid;
        private BoardDto _board;

        // State description
        private bool _isInitialized = false;
        
        // Board service
        private Services.BoardService _service;
        private GameSettingsService _gameSettingsService;

        // Graphical cell
        public ObservableCollection<GraphicCell> Cells { get; set; }

        #region Commands

        [RelayCommand]
        private async Task OnNextStep()
        {
            await UpdateBoard();
        }


        [RelayCommand]
        private async Task OnRestart()
        {
            await InitiateNewGame();
        }

        #endregion


        public DataViewModel(BoardService boardService, GameSettingsService gameSettingsService)
        {
            Cells = new();

            _gameSettingsService = gameSettingsService;
            _service = boardService;
        }

        public async void OnNavigatedTo()
        {
            if (!_isInitialized)
                await InitializeViewModel();
        }

        public void OnNavigatedFrom() 
        { }

        // Initiate view model
        private async Task InitializeViewModel()
        {
            await InitiateNewGame();

            _isInitialized = true;
        }

        private async Task InitiateNewGame()
        {
            var actualSettings = _gameSettingsService.GetUserGameSettings();
            try
            {
                _boardGuid = await _service.CreateNewBoard(actualSettings.UserBoardYSize, actualSettings.UserBoardXSize, 1);

                _board = await _service.GetActualStateOfBoard(_boardGuid);

                UpdateCanvas();
            }
            catch (Exception ex) 
            {
                Wpf.Ui.Controls.MessageBox message = new Wpf.Ui.Controls.MessageBox();
                message.Content = "Error while connecting server!";
                await message.ShowDialogAsync();
            }
        }

        private async Task UpdateBoard()
        {
            await NextBoardStep();
            UpdateCanvas();
        }

        // Gets evolved board
        private async Task NextBoardStep()
        {
            try { 
                _board = await _service.EvolveBoard(_boardGuid);
            }
            catch (Exception ex) 
            {
                Wpf.Ui.Controls.MessageBox message = new Wpf.Ui.Controls.MessageBox();
                message.Content = "Error while connecting server!";
                await message.ShowDialogAsync();
            }
        }

        // Data to graphics
        public void UpdateCanvas()
        {
            if (_board.Cells == null)
                return;

            if (!_board.Cells.Any())
                return;

            double widthOfCell = ActualCanvasWidth / _board.BoardSizeX;
            double heightOfCell = ActualCanvasHeight / _board.BoardSizeY;

            Cells.Clear();
            foreach (var item in _board.Cells)
            {
                GraphicCell cell = new GraphicCell()
                {
                    Width = widthOfCell,
                    Height = heightOfCell,
                    CanvasXCoord = item.XCoord * widthOfCell,
                    CanvasYCoord = item.YCoord * heightOfCell
                };

                Cells.Add(cell);
            }

            OnPropertyChanged(nameof(Cells));
        }
    }
}