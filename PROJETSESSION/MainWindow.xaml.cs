using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PROJETSESSION.Pages.ClientPages;
using PROJETSESSION.Pages.EmployePages;
using PROJETSESSION.Pages.ProjetPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PROJETSESSION
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(typeof(PageAfficherProjets));

        }

        private void navView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer is NavigationViewItem item)
            {
                switch (item.Name)
                {
                    case "iEmploye":
                        mainFrame.Navigate(typeof(AfficherEmployes));
                        break;
                    case "iEmployeAjout":
                        mainFrame.Navigate(typeof(AjouterEmploye));
                        break;
                    case "iEmployeModifier":
                        mainFrame.Navigate(typeof(ModifierEmploye));
                        break;
                    case "iClients":
                        mainFrame.Navigate(typeof(PageAfficherClient));
                        break;
                    case "iClientsAjout":
                        mainFrame.Navigate(typeof(PageAjouterClient));
                        break;
                    case "iProjets":
                        mainFrame.Navigate(typeof(PageAfficherProjets));
                        break;
                    case "iProjetsAjout":
                        mainFrame.Navigate(typeof(PageAjouterProjet));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
