﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Core2D.Views.Editor.Tools.Settings
{
    public class ToolSettingsPointControl : UserControl
    {
        public ToolSettingsPointControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
