using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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
    public sealed partial class PageDetailsProjet : Page
    {
        public PageDetailsProjet()
        {
            InitializeComponent();
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Projets projet = e.Parameter as Projets;
           
        
            if (projet != null)
            {
                tbxNoProjet.Text = projet.NoProjet;
                tbxTitre.Text = projet.Titre;
                tbxDateDebut.Text = projet.DateDebutString;
                tbxDesciption.Text = projet.Description;
                tbxBudjet.Text = Convert.ToString(projet.Budjet);
                tbxNbrEmploye.Text = Convert.ToString(projet.NbrEmploye);
                tbxTotalSalaire.Text = Convert.ToString(projet.TotalSalaire);
                tbxClient.Text = Convert.ToString(projet.NoClient);
                tbxStatut.Text = Convert.ToString(projet.Statut);

                
               
                
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();

        }

        
    }
}
