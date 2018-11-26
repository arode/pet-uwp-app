using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PetApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        // TODO: Follow method similar to this?: https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/navigationview
        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            NavigationViewItem selectedItem = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);

            if (selectedItem != null)
            {
                switch (selectedItem.Tag)
                {
                    case "Tab1":
                        contentFrame.Navigate(typeof(PetList));
                        break;
                    case "Tab2":
                        // TODO: for now, these point to the same page
                        contentFrame.Navigate(typeof(PetList));
                        break;
                }
            }
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            // set the initial SelectedItem
            foreach (NavigationViewItemBase item in TopNav.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Tag1")
                {
                    TopNav.SelectedItem = item;
                    break;
                }
            }
            contentFrame.Navigate(typeof(PetList));
        }
    }
}