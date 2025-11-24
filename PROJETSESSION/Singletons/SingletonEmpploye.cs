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
            connectionString = "Server= cours.cegep3r.info;Database=a2025_420345ri_gr2_2335631-goua-myriam-clothilde-silue;Uid=2335631;Pwd=2335631;";
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
                    Employes unMeuble = new Employes(matricule, nom, prenom, dateNaissance, email, adresse, dateEmbauche, tauxHoraire, photo, statut);
                    listeEmployes.Add(unMeuble);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void ajouter( string nom, string prenom, DateTime dateNaissance, string email, string adresse,
                        DateTime dateEmbauche, decimal tauxHoraires, string photo, string statut)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into employes values( @nom, @prenom, @dateNaissance, @email, @adresse, @dateEmbauche, @tauxHoraire, @photo, @statut) ";
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@date", dateNaissance);
                commande.Parameters.AddWithValue("@emaNaissance", email);
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
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        //modifie l’Email d’un client
        public void modifier(int id, string email)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update employes set email = @email where id = @id";
                commande.Parameters.AddWithValue("@id", id);
                commande.Parameters.AddWithValue("@email", email);
                con.Open();
                int i = commande.ExecuteNonQuery();

                getAllEmployes(); 
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }





    }
}
