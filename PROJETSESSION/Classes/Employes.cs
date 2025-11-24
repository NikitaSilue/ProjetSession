using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PROJETSESSION.Classes
{
    internal class Employes
    {
        public string matricule;
        public string nom;
        public string prenom;
        public DateTime dateNaissance;
        public string email;
        public string adresse;
        public DateTime dateEmbauche;
        public decimal tauxHoraires;
        public string photo;
        public string statut;

        public Employes(string matricule, string nom, string prenom, DateTime dateNaissance, string email, string adresse, 
                        DateTime dateEmbauche, decimal tauxHoraires, string photo, string statut)
        {               
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.adresse = adresse;
            this.dateEmbauche = dateEmbauche;
            this.tauxHoraires = tauxHoraires;
            this.photo = photo;
            this.statut = statut;
        }               

        public string Matricule { get => matricule; set => matricule = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public DateTime DateEmbauche { get => dateEmbauche; set => dateEmbauche = value; }
        public decimal TauxHoraires { get => tauxHoraires; set => tauxHoraires = value; }
        public string Photo { get => photo; set => photo = value; }
        public string Statut { get => statut; set => statut = value; }

        public override string? ToString()
        {
            return $"Matricule: {matricule} \nNom: {nom} \nPrénom: {prenom} \nDate de naissance: {dateNaissance}" +
                   $"\nEmail: {email} \nAdresse: {adresse} \nDate d'embauche: {dateEmbauche} \nSalaire de base: {tauxHoraires}" +
                   $"\nPhoto: {photo} \nStatut: {statut}";
        }
    }                   
}                       
                        