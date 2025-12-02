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
    internal class SingletonClient
    {

        string connectionString;
        ObservableCollection<Clients> listeClient;

        static SingletonClient instance = null;

        private SingletonClient()
        {
            connectionString = "Server= cours.cegep3r.info;Database=a2025_420335-345ri_greq30;Uid=2335631;Pwd=2335631;";
            listeClient = new ObservableCollection<Clients>();
        }

        public static SingletonClient getInstance()
        {
            if (instance == null)
                instance = new SingletonClient();
            return instance;
        }
        public ObservableCollection<Clients> Liste { get => listeClient; }

        public void getAllClients()
        {
            listeClient.Clear(); //permet de vider la liste avant de la recharger
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = con.CreateCommand();
                commande.CommandText = "Select * from clients";
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    int identifiant = r.GetInt32("identifiant");
                    string nom = r.GetString("nom");
                    string adresse = r.GetString("adresse");
                    string numeroTelephone = r.GetString("numeroTelephone");
                    string email = r.GetString("email");
                    Clients unClient = new Clients(identifiant, nom, adresse, numeroTelephone, email);
                    listeClient.Add(unClient);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public bool ajouter(string nom, string adresse, string numeroTelephone, string email)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into clients(nom, adresse, numeroTelephone, email) values(@nom, @adresse, @numeroTelephone, @email) ";
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@numeroTelephone", numeroTelephone);
                commande.Parameters.AddWithValue("@email", email);
               
                con.Open();
                int i = commande.ExecuteNonQuery();
                using MySqlCommand commande2 = new MySqlCommand();
                commande2.Connection = con;
                commande2.CommandText = "select LAST_INSERT_ID() ";
                var res = commande2.ExecuteScalar();
                getAllClients();
                return true;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        //modifie l’Email d’un client
        public bool modifier(int identifiant, string nom, string adresse, string numeroTelephone ,string email)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update clients set nom = @nom, adresse = @adresse, numeroTelephone = @numeroTelephone, email = @email where identifiant = @identifiant";
                commande.Parameters.AddWithValue("@identifiant", identifiant);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@email", email);
                commande.Parameters.AddWithValue("@numeroTelephone", numeroTelephone);
                commande.Parameters.AddWithValue("@adresse", adresse);
                con.Open();
                int i = commande.ExecuteNonQuery();

                getAllClients();
                return true;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public void supprimer(int identifiant)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "delete from clients where identifiant = @identifiant";
                commande.Parameters.AddWithValue("@matricule", identifiant);
                con.Open();
                int i = commande.ExecuteNonQuery();

                getAllClients(); //permet de recharger la liste des employes après un ajout
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}
