// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using ConwaysGameOfLife.ViewModels.Pages;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace ConwaysGameOfLife.Views.Pages
{
    public partial class DataPage : INavigableView<DataViewModel>
    {
        public DataViewModel ViewModel { get; }

        public DataPage(DataViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            
        }

        private void CellCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var canvasSender = (Canvas)sender;

            ViewModel.ActualCanvasHeight = canvasSender.ActualHeight;
            ViewModel.ActualCanvasWidth = canvasSender.ActualWidth;

            ViewModel.UpdateCanvas();
        }
    }
}
