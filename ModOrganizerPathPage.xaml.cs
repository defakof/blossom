using System;
using System.IO;
using Microsoft.Maui.Controls;
using blossom.Services;
using blossom.Utilities;
using blossom.Animations;

namespace blossom
{
    public partial class ModOrganizerPathPage : ContentPage
    {
        private string selectedModOrganizer;

        public ModOrganizerPathPage(string modOrganizer)
        {
            try
            {
                FileLogger.Log("ModOrganizerPathPage constructor started", FileLogger.LogCategory.UI);
                InitializeComponent();
                selectedModOrganizer = modOrganizer;
                SelectedOrganizerLabel.Text = $"Selected: {selectedModOrganizer}";
                FileLogger.Log($"ModOrganizerPathPage initialized with organizer: {modOrganizer}", FileLogger.LogCategory.UI);
            }
            catch (Exception ex)
            {
                FileLogger.LogException("ModOrganizerPathPage Constructor", ex, FileLogger.LogCategory.UI);
            }
        }

        private async void OnBrowseClicked(object sender, EventArgs e)
        {
            FileLogger.Log("OnBrowseClicked started", FileLogger.LogCategory.UI);
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.WinUI, new[] { ".exe" } }
                    }),
                    PickerTitle = $"Select {selectedModOrganizer} executable"
                });

                if (result != null)
                {
                    PathEntry.Text = result.FullPath;
                    FileLogger.Log($"File selected: {result.FullPath}", FileLogger.LogCategory.FileOperation);
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException("OnBrowseClicked", ex, FileLogger.LogCategory.UI);
                await DisplayAlert("Error", $"An error occurred while selecting the file: {ex.Message}", "OK");
            }
            FileLogger.Log("OnBrowseClicked completed", FileLogger.LogCategory.UI);
        }

        private async void OnContinueClicked(object sender, EventArgs e)
        {
            FileLogger.Log("OnContinueClicked started", FileLogger.LogCategory.UI);
            try
            {
                if (string.IsNullOrWhiteSpace(PathEntry.Text))
                {
                    FileLogger.Log("No file path entered", FileLogger.LogCategory.UI);
                    ErrorLabel.Text = "Please select the executable file.";
                    ErrorLabel.IsVisible = true;
                    return;
                }

                if (!File.Exists(PathEntry.Text))
                {
                    FileLogger.Log($"File does not exist: {PathEntry.Text}", FileLogger.LogCategory.FileOperation);
                    ErrorLabel.Text = "The selected file does not exist.";
                    ErrorLabel.IsVisible = true;
                    return;
                }

                FileLogger.Log($"Saving mod organizer path: {PathEntry.Text}", FileLogger.LogCategory.Backend);
                ModOrganizerService.Instance.SaveModOrganizerPath(selectedModOrganizer, PathEntry.Text);

                FileLogger.Log("Creating MainPage instance", FileLogger.LogCategory.UI);
                var mainPage = new MainPage();

                FileLogger.Log("Attempting to navigate to MainPage", FileLogger.LogCategory.Navigation);
                await CustomPageAnimation.PushAsync(Navigation, mainPage);
                FileLogger.Log("Navigation to MainPage completed", FileLogger.LogCategory.Navigation);
            }
            catch (Exception ex)
            {
                FileLogger.LogException("OnContinueClicked", ex, FileLogger.LogCategory.UI);
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }
    }
}