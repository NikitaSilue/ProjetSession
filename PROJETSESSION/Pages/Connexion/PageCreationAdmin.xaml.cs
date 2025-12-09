using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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
    public sealed partial class PageCreationAdmin : Page
    {
        public PageCreationAdmin()
        {
            InitializeComponent();
        }


        private async void btnCree_Click(object sender, RoutedEventArgs e)
        {

            bool valide = true;

            erreurNomUtilisateur.Text = "";
            erreurMotDePasse.Text = "";
            erreurConfirmation.Text = "";

            if (string.IsNullOrWhiteSpace(tbxNomUtilisateur.Text))
            {
                erreurNomUtilisateur.Text = "le nom d'utilisateur est obligatoire.";
                valide = false;
            }
            else if (tbxNomUtilisateur.Text.Trim().Length < 4)
            {
                erreurNomUtilisateur.Text = "Le nom d'utilisateur doit contenir au moins 4 caractères.";
                valide = false;

            }

            if (string.IsNullOrWhiteSpace(pbxMotDePasse.Password))
            {
                erreurMotDePasse.Text = "Le mot de passe est obligatoire.";
                valide = false;
            }
            else if (pbxMotDePasse.Password.Length < 6)
            {
                erreurMotDePasse.Text = "le mot de passe doit contenir au moins 6 caractères.";
                valide = false;
            }
            if (pbxMotDePasse.Password != pbxConfirmation.Password)
            {
                erreurConfirmation.Text = "Les mots de passe ne correspondent pas.";
                valide = false;
            }

            if (valide)
            {
                string nomUtilisateur =tbxNomUtilisateur.Text.Trim();
                string motDePasse = pbxMotDePasse.Password;

                string resultat = SingletonAdmin.getInstance().CreerCompteAdmin(nomUtilisateur, motDePasse);

                if (resultat == "")
                {
                    ContentDialog dialog = new ContentDialog
                    {
                        Title = "Succès",
                        Content = "Le compte administrateur a été créé avec succès! Vous pouvez maintenant vous connectez.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot

                    };
                    await dialog.ShowAsync();

                    Frame.Navigate(typeof(BlankPage1));
                }
                else
                {
                    ContentDialog dialog = new ContentDialog
                    {
                        Title = "Erreur",
                        Content = resultat,
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot

                    };
                    await dialog.ShowAsync();

                }

            }

        }

        
    }
}
