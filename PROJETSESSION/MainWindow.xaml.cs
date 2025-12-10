using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using MySqlX.XDevAPI;
using PROJETSESSION.Boites_de_dialogue;
using PROJETSESSION.Classes;
using PROJETSESSION.Pages.ClientPages;
using PROJETSESSION.Pages.Connexion;
using PROJETSESSION.Pages.EmployePages;
using PROJETSESSION.Pages.ProjetPages;
using PROJETSESSION.Singletons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Protection.PlayReady;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PROJETSESSION
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public static MainWindow mainWindow;

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            VerifierCompteAdmin();
            MettreAJourConnexion();
          
        }

        private async void navView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer is NavigationViewItem item)
            {
                switch (item.Name)
                {
                    case "iEmploye":
                        mainFrame.Navigate(typeof(AfficherEmployes));
                        break;
                    case "iEmployeAjout":
                        if (SingletonAdmin.getInstance().EstConnecte)
                        {
                            mainFrame.Navigate(typeof(AjouterEmploye));
                        }
                        else
                        {
                            await connexionRequis();
                        }

                        break;
                    
                    case "iClients":
                        mainFrame.Navigate(typeof(PageAfficherClient));
                        break;
                    case "iClientsAjout":
                        if (SingletonAdmin.getInstance().EstConnecte)
                        {
                            mainFrame.Navigate(typeof(PageAjouterClient));
                        }
                        else
                        {
                            await connexionRequis();
                        }

                        break;
                    case "iProjets":
                        mainFrame.Navigate(typeof(PageAfficherProjets));
                        break;

                    case "iProjetsAjout":
                        if (SingletonAdmin.getInstance().EstConnecte)
                        {
                            mainFrame.Navigate(typeof(PageAjouterProjet));
                        }
                        else
                        {
                            await connexionRequis();
                        }

                        break;

                   
                    case "iConnexion":
                        mainFrame.Navigate(typeof(BlankPage1), this);
                        break;

                    case "iDeconnexion":
                        confirmationDeconnexion();
                        
                        mainFrame.Navigate(typeof(PageAfficherProjets));
                        break;


                    default:
                        break;
                }
            }
        }

        private void VerifierCompteAdmin()
        {
            if (!SingletonAdmin.getInstance().CompteAdminExiste())
            {
                mainFrame.Navigate(typeof(PageCreationAdmin));
            }
            else
            {
                mainFrame.Navigate(typeof(PageAfficherProjets));
            }
        }

        public void MettreAJourConnexion()
        {
            if (SingletonAdmin.getInstance().EstConnecte)
            {
                iConnexion.Visibility = Visibility.Collapsed;
                iDeconnexion.Visibility = Visibility.Visible;
                iEmployeAjout.Visibility = Visibility.Visible;
                iClientsAjout.Visibility = Visibility.Visible;
                iProjetsAjout.Visibility = Visibility.Visible;
            }
            else
            {
                iConnexion.Visibility = Visibility.Visible;
                iDeconnexion.Visibility = Visibility.Collapsed;
                iEmployeAjout.Visibility = Visibility.Collapsed;
                iClientsAjout.Visibility = Visibility.Collapsed;
                iProjetsAjout.Visibility = Visibility.Collapsed;
            }
        }


        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            if (SingletonAdmin.getInstance().EstConnecte)
            {
                var picker = new Windows.Storage.Pickers.FileSavePicker();
                var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);
                picker.SuggestedFileName = "test";
                picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });
                //crée le fichier
                Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

                if (monFichier == null) return;

                List<Projets> lignes = SingletonProjet.getInstance().ListeCSV;

                if (monFichier != null)
                    await Windows.Storage.FileIO.WriteLinesAsync(monFichier, lignes.ConvertAll(x => x.stringCSV()), Windows.Storage.Streams.UnicodeEncoding.Utf8);
            }
            else
            {
                await connexionRequis();
            }
            
        }




        private async System.Threading.Tasks.Task connexionRequis()
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Connexion requise",
                Content = "Seul un administrateur peut éffectuer cette action",
                PrimaryButtonText = "Se connecter",
                CloseButtonText = "Annuler",
                XamlRoot = this.Content.XamlRoot
            };

            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                mainFrame.Navigate(typeof(BlankPage1));
            }
        }



        private async System.Threading.Tasks.Task confirmationDeconnexion()
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.Content.XamlRoot;
            dialog.Title = "Déconnexion";
            dialog.PrimaryButtonText = "Oui";
            dialog.SecondaryButtonText = "Non";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Secondary;
            dialog.Content = "Êtes-vous sûr de vouloir vous déconnecter?";

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                SingletonAdmin.getInstance().Deconnecter();
                MettreAJourConnexion();
                mainFrame.Navigate(typeof(PageAfficherProjets));
            }
            else if (resultat == ContentDialogResult.Secondary)
            {
                Debug.WriteLine("Bouton Non sélectionné");
            }
            else
            {
                Debug.WriteLine("Annulé");
            }
        }
    }
}
