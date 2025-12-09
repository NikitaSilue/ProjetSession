using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PROJETSESSION.Boites_de_dialogue;
using PROJETSESSION.Classes;
using PROJETSESSION.Singletons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PROJETSESSION.Pages.ProjetPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAfficherProjets : Page
    {
        public PageAfficherProjets()
        {
            InitializeComponent();
            lvListeProjet.ItemsSource = SingletonProjet.getInstance().Liste;
            SingletonProjet.getInstance().getAllProjets();

            lvListeProjet.ContainerContentChanging += LvListeEmploye_BouttonsVisible;
            MainWindow.mainWindow.MettreAJourConnexion();
        }

        private void LvListeEmploye_BouttonsVisible(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.ItemContainer is GridViewItem item)
            {
                var root = item.ContentTemplateRoot as FrameworkElement;
                if (root == null)
                    return;

                var spBoutons = root.FindName("spBoutons") as StackPanel;
                var btnModifier = root.FindName("btnModifier") as Button;
                var btnSupprimer = root.FindName("btnSupprimer") as Button;

                bool estConnecte = SingletonAdmin.getInstance().EstConnecte;

                if (spBoutons != null)
                    spBoutons.Visibility = estConnecte ? Visibility.Visible : Visibility.Collapsed;
            }

        }





        private async void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Suppression";
            dialog.PrimaryButtonText = "Oui";
            dialog.SecondaryButtonText = "Non";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Secondary;
            dialog.Content = "Êtes-vous sûr de vouloir supprimer ce projet?";

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                Button button = sender as Button;
                Projets projet = button.DataContext as Projets;
                string noProjet = projet.noProjet;

                SingletonProjet.getInstance().supprimer(noProjet);
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

        private async void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Projets projet = button.DataContext as Projets;


            ///////////////////////////////////////////////////////////////////
            DialogueModifierProjet dialogue = new DialogueModifierProjet();
            dialogue.XamlRoot = this.XamlRoot;
            dialogue.PrimaryButtonText = "Modifier";
            dialogue.CloseButtonText = "Annuler";
            dialogue.Title = " Modification de projet ";
            dialogue.setProjets(projet);

            ContentDialogResult resultat = await dialogue.ShowAsync();
        }

        private void lvListeProjet_ItemClick(object sender, ItemClickEventArgs e)
        {
            Projets projet = e.ClickedItem as Projets;
            if (projet != null)
            {
                Frame.Navigate(typeof(PageDetailsProjet), projet);
            }
        }


    }
}
