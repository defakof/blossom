using Microsoft.Maui.Controls;

namespace blossom.Animations
{
    public static class CustomTransitionAnimation
    {
        public static async Task PushWithAnimation(this INavigation navigation, Page page)
        {
            var lastPage = navigation.NavigationStack.LastOrDefault();
            if (lastPage != null)
            {
                page.Opacity = 0;
                navigation.InsertPageBefore(page, lastPage);

                await Task.WhenAll(
                    lastPage.FadeTo(0, 200, Easing.CubicInOut),
                    page.FadeTo(1, 200, Easing.CubicInOut)
                );

                await navigation.PopAsync(false);
            }
            else
            {
                await navigation.PushAsync(page);
            }
        }
    }
}