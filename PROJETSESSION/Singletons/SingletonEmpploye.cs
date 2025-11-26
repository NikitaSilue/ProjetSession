using Microsoft.WindowsAppSDK.Runtime.Packages;
using MySql.Data.MySqlClient;
using PROJETSESSION.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJETSESSION.Singletons
{
    internal class SingletonEmpploye
    {
        string connectionString;
        ObservableCollection<Employes> listeEmployes;

        static SingletonEmpploye instance = null;

        private SingletonEmpploye()
        {
            connectionString = "Server= cours.cegep3r.info;Database=a2025_420335-345ri_greq30;Uid=2335631;Pwd=2335631;";
            listeEmployes = new ObservableCollection<Employes>();
        }

        public static SingletonEmpploye getInstance()
        {
            if (instance == null)
                instance = new SingletonEmpploye();
            return instance;
        }
        public ObservableCollection<Employes> Liste { get => listeEmployes; }

        public void getAllEmployes()
        {
            listeEmployes.Clear(); //permet de vider la liste avant de la recharger
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = con.CreateCommand();
                commande.CommandText = "Select * from employes";
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string matricule = r.GetString("matricule");
                    string nom = r.GetString("nom");
                    string prenom = r.GetString("prenom");
                    DateTime dateNaissance = r.GetDateTime("dateNaissance");
                    string email = r.GetString("email");
                    string adresse = r.GetString("adresse");
                    DateTime dateEmbauche = r.GetDateTime("dateEmbauche");
                    decimal tauxHoraire = r.GetDecimal("tauxHoraire");
                    string photo = r.GetString("photo");
                    string statut = r.GetString("statut");
                    Employes unEmploye = new Employes(matricule, nom, prenom, dateNaissance, email, adresse, dateEmbauche, tauxHoraire, photo, statut);
                    listeEmployes.Add(unEmploye);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public bool ajouter( string nom, string prenom, DateTime dateNaissance, string email, string adresse,
                        DateTime dateEmbauche, decimal tauxHoraires, string photo, string statut)
        {

            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into employes(nom, prenom, dateNaissance, email, adresse, dateEmbauche, tauxHoraire, photo, statut)" +
                                    " values( @nom, @prenom, @dateNaissance, @email, @adresse, @dateEmbauche, @tauxHoraire, @photo, @statut) ";
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@dateNaissance", dateNaissance);
                commande.Parameters.AddWithValue("@email", email);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@dateEmbauche", dateEmbauche);
                commande.Parameters.AddWithValue("@tauxHoraire", tauxHoraires);
                commande.Parameters.AddWithValue("@photo", photo);
                commande.Parameters.AddWithValue("@statut", statut);
                con.Open();
                int i = commande.ExecuteNonQuery();
                using MySqlCommand commande2 = new MySqlCommand();
                commande2.Connection = con;
                commande2.CommandText = "select LAST_INSERT_ID() ";
                var res = commande2.ExecuteScalar();
                getAllEmployes();

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

        }

        public bool modifier(string matricule, string nom, string prenom, string email, string adresse, decimal tauxHoraire, string photo, string statut)
        {
            try
            {
                
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update employes set nom = @nom, prenom = @prenom, email = @email, adresse = @adresse, " +
                                        "tauxHoraire = @tauxHoraire, photo = @photo, statut = @statut where matricule = @matricule";
                commande.Parameters.AddWithValue("@matricule", matricule);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@email", email);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@tauxHoraire", tauxHoraire);
                commande.Parameters.AddWithValue("@photo", photo);
                commande.Parameters.AddWithValue("@statut", statut);
                con.Open();
                int i = commande.ExecuteNonQuery();

                getAllEmployes();

                return true;
            }
            catch (MySqlException ex)
            {
                return false;
                Debug.WriteLine(ex.Message);
            }
        }

        public void supprimer(string matricule)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "delete from employes where matricule = @matricule";
                commande.Parameters.AddWithValue("@matricule", matricule);
                con.Open();
                int i = commande.ExecuteNonQuery();

                getAllEmployes(); //permet de recharger la liste des employes après un ajout
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }





    }
}
