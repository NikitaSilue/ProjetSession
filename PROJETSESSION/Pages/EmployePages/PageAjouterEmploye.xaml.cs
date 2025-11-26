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
using System.Text.RegularExpressions;
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

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            bool valide = true;
            errorNom.Text = string.Empty;
            errorPrenom.Text = string.Empty;
            errorEmail.Text = string.Empty;
            errorAdresse.Text = string.Empty;
            errorTauxHoraires.Text = string.Empty;
            errorPhoto.Text = string.Empty;
            errorStatut.Text = string.Empty;

            //VALIDATION 
            //NOM
            if (string.IsNullOrWhiteSpace(tbxNom.Text))
            {
                errorNom.Text = "Le nom est obligatoire svp.";
                valide = false;
            }
            //PRENOM
            if (string.IsNullOrWhiteSpace(tbxPrenom.Text))
            {
                errorPrenom.Text = "Le prenom est obligatoire svp.";
                valide = false;
            }
            //EMAIL
            if (string.IsNullOrWhiteSpace(tbxEmail.Text))
            {
                errorEmail.Text = "L'email est obligatoire svp.";
                valide = false;
            }
            if (!Regex.IsMatch(tbxEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorEmail.Text = "Format email invalide (ex: exemple@gmail.com).";
                valide = false;
            }

            //ADRESSE
            if (string.IsNullOrWhiteSpace(tbxAdresse.Text))
            {
                errorAdresse.Text = "L'adresse est obligatoire svp.";
                valide = false;
            }

            //TAUX HORAIRE
            if (nbrTauxHoraire.Value == 0)
            {
                errorTauxHoraires.Text = "Entrer un taux horaire valide svp.";
                valide = false;
            }
            if (nbrTauxHoraire.Value < 15)
            {
                errorTauxHoraires.Text = "Le taux horaire doit être d'au moins 15 $.";
                valide = false;
            }
            //PHOTO
            if (!Uri.IsWellFormedUriString(tbxPhoto.Text, UriKind.Absolute))
            {
                errorPhoto.Text = "Veuillez entrer un lien valide";
            }
            //STATUT
            if (cmbxStatut.SelectedItem == null)
            {
                errorStatut.Text = "Veuillez sélectionner un statut.";
            }

            //DATES
            DateTime dateNaissance = dpkDateNaissance.Date.DateTime;
            DateTime dateEmbauche = dpkDateEmbauche.Date.DateTime;
            int age = DateTime.Now.Year - dateNaissance.Date.Year;
            if (dateEmbauche == null)
            {
                errorEmbauche.Text = "Choisissez une date d'embauche pour l'employé";
                valide = false;
            }
            if (dateEmbauche > DateTime.Now)
            {
                errorEmbauche.Text = "La date d'embauche ne peut pas être dans le futur.";
                valide = false;
            }
            if (dateNaissance == null)
            {
                errorNaissance.Text = "Choisissez une date de naissance pour l'employé";
            }
            if (dateNaissance > DateTime.Now)
            {
                errorNaissance.Text = "La date de naissance ne peut pas être dans le futur.";
                valide = false;
            }
            if (dateNaissance == dateEmbauche)
            {
                errorEmbauche.Text = "La date d'embauche ne peut pas être égale à la date de naissance.";
                valide = false;
            }
            if (dateNaissance.Date > DateTime.Now.AddYears(-age)) age--;

            if (age < 18)
            {
                errorNaissance.Text = "L'employé doit avoir au moins 18 ans.";
                valide = false;
            }
            else if (age > 65)
            {
                errorNaissance.Text = "L'employé ne peut pas avoir plus de 65 ans.";
                valide = false;
            }




            if (valide)
            {
                string nom = tbxNom.Text;
                string prenom = tbxPrenom.Text;
                string email = tbxEmail.Text;
                string adresse = tbxAdresse.Text;
                decimal tauxHoraires = (decimal)nbrTauxHoraire.Value;
                string photo = tbxPhoto.Text;
                DateTime dateEmbauches = dpkDateEmbauche.Date.DateTime;
                DateTime dateNaissances = dpkDateNaissance.Date.DateTime;
                string statut = cmbxStatut.SelectedItem?.ToString();

                if(SingletonEmpploye.getInstance().ajouter(nom, prenom, dateNaissances, email, adresse, dateEmbauches, tauxHoraires, photo, statut))
                    Frame.Navigate(typeof(AfficherEmployes));
                else
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = this.XamlRoot;
                    dialog.Title = "Échec de l'ajout";
                    dialog.PrimaryButtonText = "Ok";
                    dialog.CloseButtonText = "Annuler";
                    dialog.DefaultButton = ContentDialogButton.Secondary;
                    dialog.Content = "L'employé n'a pas pu être ajouté. \nVeuillez réesseyer";

                    ContentDialogResult resultat = await dialog.ShowAsync();
                    if (resultat == ContentDialogResult.Primary)
                    {
                        Debug.WriteLine("Nouvelle tentative");
                    }
                    else
                    {
                        Debug.WriteLine("Annulé");
                    }

                }
            }

        }
    }
}
