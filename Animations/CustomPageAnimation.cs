using Microsoft.Maui.Controls;
using blossom.Utilities;
using System;
using System.Threading.Tasks;

namespace blossom.Animations
{
    public static class CustomPageAnimation
    {
        public static async Task PushAsync(INavigation navigation, Page page)
        {
            try
            {
                FileLogger.Log("CustomPageAnimation PushAsync started", FileLogger.LogCategory.Navigation);
                await navigation.PushAsync(page, false);
                FileLogger.Log("Navigation.PushAsync completed", FileLogger.LogCategory.Navigation);
            }
            catch (Exception ex)
            {
                FileLogger.LogException("CustomPageAnimation PushAsync", ex, FileLogger.LogCategory.Navigation);
                throw;
            }
        }

        public static async Task PopAsync(INavigation navigation)
        {
            try
            {
                FileLogger.Log("CustomPageAnimation PopAsync started", FileLogger.LogCategory.Navigation);
                await navigation.PopAsync(false);
                FileLogger.Log("Navigation.PopAsync completed", FileLogger.LogCategory.Navigation);
            }
            catch (Exception ex)
            {
                FileLogger.LogException("CustomPageAnimation PopAsync", ex, FileLogger.LogCategory.Navigation);
                throw;
            }
        }
    }
}