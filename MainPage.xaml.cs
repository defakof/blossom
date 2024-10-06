using blossom.Utilities;
using Microsoft.Maui.Controls;
using System;

namespace blossom
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            try
            {
                FileLogger.Log("MainPage constructor started", FileLogger.LogCategory.UI);
                InitializeComponent();
                SetupUI();
                FileLogger.Log("MainPage initialized", FileLogger.LogCategory.UI);
            }
            catch (Exception ex)
            {
                FileLogger.LogException("MainPage Constructor", ex, FileLogger.LogCategory.UI);
            }
        }

        private void SetupUI()
        {
            try
            {
                FileLogger.Log("Setting up UI", FileLogger.LogCategory.UI);
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label
                        {
                            Text = "Welcome to the Main Page!",
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.CenterAndExpand
                        }
                    }
                };
                FileLogger.Log("UI setup completed", FileLogger.LogCategory.UI);
            }
            catch (Exception ex)
            {
                FileLogger.LogException("SetupUI", ex, FileLogger.LogCategory.UI);
            }
        }
    }
}