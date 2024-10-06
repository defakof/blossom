using System;
using System.Collections.ObjectModel;
using blossom.Services;
using blossom.Utilities;
using Microsoft.Maui.Controls;

namespace blossom
{
    public partial class StartupPage : ContentPage
    {
        private ObservableCollection<ModOrganizerItem> modOrganizers;
        private string selectedModOrganizer;

        public StartupPage()
        {
            try
            {
                FileLogger.Log("StartupPage constructor started", FileLogger.LogCategory.UI);
                InitializeComponent();
                InitializeModOrganizerList();
                FileLogger.Log("StartupPage initialized", FileLogger.LogCategory.UI);
            }
            catch (Exception ex)
            {
                FileLogger.LogException("StartupPage Constructor", ex, FileLogger.LogCategory.UI);
            }
        }

        private void InitializeModOrganizerList()
        {
            FileLogger.Log("Initializing ModOrganizerList", FileLogger.LogCategory.UI);
            modOrganizers = new ObservableCollection<ModOrganizerItem>
            {
                new ModOrganizerItem { Name = "Mod Organizer 2", TextColor = Colors.White },
                new ModOrganizerItem { Name = "BG3ModManager", TextColor = Colors.Gray },
                new ModOrganizerItem { Name = "Vortex", TextColor = Colors.Gray }
            };
            ModOrganizerListView.ItemsSource = modOrganizers;
            FileLogger.Log("ModOrganizerList initialized", FileLogger.LogCategory.UI);
        }

        private void OnModOrganizerSelected(object sender, SelectedItemChangedEventArgs e)
        {
            FileLogger.Log("OnModOrganizerSelected called", FileLogger.LogCategory.UI);
            if (e.SelectedItem is ModOrganizerItem selectedItem)
            {
                selectedModOrganizer = selectedItem.Name;
                FileLogger.Log($"Selected mod organizer: {selectedModOrganizer}", FileLogger.LogCategory.UI);
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            FileLogger.Log("UpdateUI called", FileLogger.LogCategory.UI);
            bool isSupported = ModOrganizerService.Instance.IsOrganizerSupported(selectedModOrganizer);
            ContinueButton.IsEnabled = isSupported;

            if (isSupported)
            {
                SupportStatusLabel.Text = $"{selectedModOrganizer} is supported.";
            }
            else
            {
                SupportStatusLabel.Text = $"{selectedModOrganizer} is not yet supported.";
            }
            FileLogger.Log($"UI updated. Is supported: {isSupported}", FileLogger.LogCategory.UI);
        }

        private async void OnContinueClicked(object sender, EventArgs e)
        {
            FileLogger.Log("OnContinueClicked called", FileLogger.LogCategory.UI);
            try
            {
                if (ModOrganizerService.Instance.IsOrganizerSupported(selectedModOrganizer))
                {
                    FileLogger.Log($"Navigating to ModOrganizerPathPage for {selectedModOrganizer}", FileLogger.LogCategory.Navigation);
                    var nextPage = new ModOrganizerPathPage(selectedModOrganizer);
                    await Navigation.PushAsync(nextPage);
                    FileLogger.Log("Navigation to ModOrganizerPathPage completed", FileLogger.LogCategory.Navigation);
                }
                else
                {
                    FileLogger.Log($"Attempted to continue with unsupported mod organizer: {selectedModOrganizer}", FileLogger.LogCategory.UI);
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException("OnContinueClicked", ex, FileLogger.LogCategory.UI);
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }
    }

    public class ModOrganizerItem
    {
        public string Name { get; set; }
        public Color TextColor { get; set; }
    }
}