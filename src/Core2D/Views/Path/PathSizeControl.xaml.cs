﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Core2D.Views.Path
{
    public class PathSizeControl : UserControl
    {
        public PathSizeControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
