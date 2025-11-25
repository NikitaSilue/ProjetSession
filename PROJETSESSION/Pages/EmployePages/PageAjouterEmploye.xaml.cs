using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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

namespace PROJETSESSION.Pages.EmployePages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjouterEmploye : Page
    {
        public AjouterEmploye()
        {
            InitializeComponent();

        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            bool valide = true;
            errorNom.Text = string.Empty;
            errorPrenom.Text = string.Empty;
            errorEmail.Text = string.Empty;
            errorAdresse.Text = string.Empty;
            errorTauxHoraires.Text = string.Empty;
            errorPhoto.Text = string.Empty;
            errorStatut.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(tbxNom.Text))
            {
                errorNom.Text = "Le nom est obligatoire svp.";
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(tbxPrenom.Text))
            {
                errorPrenom.Text = "Le prenom est obligatoire svp.";
                valide = false;
            }
            if (string.IsNullOrWhiteSpace(tbxEmail.Text))
            {
                errorEmail.Text = "L'email est obligatoire svp.";
                valide = false;
            }
            if (string.IsNullOrWhiteSpace(tbxAdresse.Text))
            {
                errorAdresse.Text = "L'adresse est obligatoire svp.";
                valide = false;
            }
            if (nbrTauxHoraire.Value == 0)
            {
                errorTauxHoraires.Text = "Entrer un taux horaire validesvp.";
                valide = false;
            }
            if (!Uri.IsWellFormedUriString(tbxPhoto.Text, UriKind.Absolute))
            {
                errorPhoto.Text = "Veuillez entrer un lien valide";
            }
            if (cmbxStatut.SelectedItem == null)
            {
                errorStatut.Text = "Veuillez sélectionner un statut.";
            }
            if (dpkDateEmbauche == null)
            {
                errorNaissance.Text = "Choisissez une date d'embauche pour l'employé";
            }
            if (dpkDateNaissance == null)
            {
                errorNaissance.Text = "Choisissez une date de naissance pour l'employé";
            }




            if (valide)
            {
                string nom = tbxNom.Text;
                string prenom = tbxPrenom.Text;
                string email = tbxEmail.Text;
                string adresse = tbxAdresse.Text;
                decimal tauxHoraires = (decimal)nbrTauxHoraire.Value;
                string photo = tbxPhoto.Text;
                DateTime dateEmbauche = dpkDateEmbauche.Date.DateTime;
                DateTime dateNaissance = dpkDateNaissance.Date.DateTime;
                string statut = cmbxStatut.SelectedItem?.ToString();

                SingletonEmpploye.getInstance().ajouter(nom, prenom, dateNaissance, email, adresse, dateEmbauche, tauxHoraires, photo, statut);

                Frame.Navigate(typeof(AfficherEmployes));
            }

        }
    }
}
