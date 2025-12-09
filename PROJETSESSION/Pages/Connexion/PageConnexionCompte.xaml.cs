using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using MySqlX.XDevAPI.Common;
using PROJETSESSION.Boites_de_dialogue;
using PROJETSESSION.Pages.EmployePages;
using PROJETSESSION.Pages.ProjetPages;
using PROJETSESSION.Singletons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PROJETSESSION.Pages.Connexion
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            InitializeComponent();
            
        }

        private async void creationCompte(object sender, RoutedEventArgs e)
        {
            DialogueCompteAdmin dialog = new DialogueCompteAdmin
            {
                Title = "Création du compte admin",
                PrimaryButtonText = "Modifier",
                CloseButtonText = "Annuler",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }



        private async void btnConnecter_Click(object sender, RoutedEventArgs e)
        {
            bool valide = true;
            erreurNomUtilisateur.Text = "";
            erreurMotDePasse.Text = "";


            if (string.IsNullOrWhiteSpace(tbxNomUtilisateur.Text))
            {
                erreurNomUtilisateur.Text = "Le nom d'utilisateur est obligatoire.";
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(pbxMotDePasse.Password))
            {
                erreurMotDePasse.Text = "Le mot de passe est obligatoire.";
                valide = false;
            }

            if (valide)
            {
                 string nomUtilisateur = tbxNomUtilisateur.Text;
                 string motDePasse = pbxMotDePasse.Password;

                string resultat = SingletonAdmin.getInstance().Connecter(nomUtilisateur, motDePasse);

                if (resultat == "")
                {
                    MainWindow.mainWindow.MettreAJourConnexion();
                    Frame.Navigate(typeof(PageAfficherProjets));
                    Frame.Navigate(typeof(PageAfficherProjets));
                }
                else
                {
                    ContentDialog dialog = new ContentDialog
                    {
                        Title = "Erreur de connexion",
                        Content = resultat,
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                    await dialog.ShowAsync();

                    pbxMotDePasse.Password = "";
                }
            }

        }

        private async void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
            else
                Frame.Navigate(typeof(PageAfficherProjets));

        }

        


    }
}
