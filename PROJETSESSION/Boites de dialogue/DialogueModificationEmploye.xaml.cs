using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PROJETSESSION.Classes;
using PROJETSESSION.Pages.EmployePages;
using PROJETSESSION.Singletons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PROJETSESSION.Boites_de_dialogue;

public sealed partial class DialogueModificationEmploye : ContentDialog
{

    Employes employes;
    public DialogueModificationEmploye()
    {
        InitializeComponent();
    }

    public void setEmploye(Employes employe)
    {
        this.employes = employe;
        tbxNom.Text = employe.nom;
        tbxPrenom.Text = employe.prenom;
        tbxEmail.Text = employe.email;
        tbxAdresse.Text = employe.adresse;
        tbxPhoto.Text = employe.photo;
        cmbStatut.SelectedItem = employe.statut;
        nbrTauxHoraire.Value = Convert.ToDouble(employe.TauxHoraires);
    }

    private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        bool valide = true;
        errorNom.Text = string.Empty;
        errorPrenom.Text = string.Empty;
        errorEmail.Text = string.Empty;
        errorAdresse.Text = string.Empty;
        errorTauxHoraire.Text = string.Empty;
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
        if (nbrTauxHoraire.Value == null)
        {
            errorTauxHoraire.Text = "Le taux horaire est obligatoire.";
            valide = false;
        }
        else if (nbrTauxHoraire.Value <= 0)
        {
            errorTauxHoraire.Text = "Entrer un taux horaire valide svp.";
            valide = false;
        }
        else if (nbrTauxHoraire.Value < 15)
        {
            errorTauxHoraire.Text = "Le taux horaire doit être d'au moins 15 $.";
            valide = false;
        }
        //PHOTO
        if (!Uri.IsWellFormedUriString(tbxPhoto.Text, UriKind.Absolute))
        {
            errorPhoto.Text = "Veuillez entrer un lien valide";
        }
        //STATUT
        if (cmbStatut.SelectedItem == null)
        {
            errorStatut.Text = "Veuillez sélectionner un statut.";
        }


        if (valide)
        {
            string nom = tbxNom.Text;
            string prenom = tbxPrenom.Text;
            string email = tbxEmail.Text;
            string adresse = tbxAdresse.Text;
            decimal tauxHoraires = (decimal)nbrTauxHoraire.Value;
            string photo = tbxPhoto.Text;
            string statut = cmbStatut.SelectedItem?.ToString();
           
            if (SingletonEmpploye.getInstance().modifier(employes.Matricule, nom, prenom, email, adresse, tauxHoraires, photo, statut) == false)
            {
                args.Cancel = true;
            }
            else
                args.Cancel = false;

        }
        else
        {
            args.Cancel = true;
        }
    }
}
