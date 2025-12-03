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
    internal class SingletonProjet
    {
        string connectionString;
        ObservableCollection<Projets> listeProjets;

        static SingletonProjet instance = null;

        private SingletonProjet()
        {
            connectionString = "Server= cours.cegep3r.info;Database=a2025_420335-345ri_greq30;Uid=2335631;Pwd=2335631;";
            listeProjets = new ObservableCollection<Projets>();
        }

        public static SingletonProjet getInstance()
        {
            if (instance == null)
                instance = new SingletonProjet();
            return instance;
        }
        public ObservableCollection<Projets> Liste { get => listeProjets; }

        public void getAllProjets()
        {
            listeProjets.Clear(); //permet de vider la liste avant de la recharger
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = con.CreateCommand();
                commande.CommandText = "Select * from projets";
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string noProjet = r.GetString("noProjet");
                    string titre = r.GetString("titre");
                    DateTime dateDebut = r.GetDateTime("dateDebut");
                    string description = r.GetString("description");
                    decimal budjet = r.GetDecimal("budjet");
                    int nbEmploye = r.GetInt32("nbEmploye");
                    decimal totalSalaire = r.GetDecimal("totalSalaire");
                    int noClient = r.GetInt32("noClient");
                    string statut = r.GetString("statut");

                    Projets unProjet = new Projets(noProjet, titre, dateDebut, description, budjet, nbEmploye, totalSalaire, noClient, statut);
                    listeProjets.Add(unProjet);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        public bool ajouter(string titre, DateTime dateDebut, string description, decimal budjet, int nbEmploye, decimal totalSalaire, int noClient, string statut)
        {

            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into projets(titre, dateDebut, description, budjet, nbEmploye, totalSalaire, noClient, statut)" +
                                    " values( @titre, @dateDebut, @description, @budjet, @nbEmploye, @totalSalaire, @noClient, @statut) ";
                commande.Parameters.AddWithValue("@titre", titre);
                commande.Parameters.AddWithValue("@dateDebut", dateDebut);
                commande.Parameters.AddWithValue("@description", description);
                commande.Parameters.AddWithValue("@budjet", budjet);
                commande.Parameters.AddWithValue("@nbEmploye", nbEmploye);
                commande.Parameters.AddWithValue("@totalSalaire", totalSalaire);
                commande.Parameters.AddWithValue("@noClient", noClient);
                commande.Parameters.AddWithValue("@statut", statut);
                con.Open();
                int i = commande.ExecuteNonQuery();
                using MySqlCommand commande2 = new MySqlCommand();
                commande2.Connection = con;
                commande2.CommandText = "select LAST_INSERT_ID() ";
                var res = commande2.ExecuteScalar();
                getAllProjets();

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool modifier( string noProjet, string titre, string description, decimal budjet, int nbEmploye, decimal totalSalaire, string statut)
        {
            try
            {

                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update projets set titre = @titre, description = @description, budjet = @budjet, " +
                                        "nbEmploye = @nbEmploye, totalSalaire = @totalSalaire, statut = @statut where noProjet = @noProjet";
                commande.Parameters.AddWithValue("@titre", titre);
                commande.Parameters.AddWithValue("@description", description);
                commande.Parameters.AddWithValue("@budjet", budjet);
                commande.Parameters.AddWithValue("@nbEmploye", nbEmploye);
                commande.Parameters.AddWithValue("@totalSalaire", totalSalaire);
                commande.Parameters.AddWithValue("@statut", statut);
                commande.Parameters.AddWithValue("@noProjet", noProjet);
                con.Open();
                int i = commande.ExecuteNonQuery();

                getAllProjets();

                return true;
            }
            catch (MySqlException ex)
            {
                return false;
                Debug.WriteLine(ex.Message);
            }
        }

        public void supprimer(string noProjet)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "delete from projets where noProjet = @noProjet";
                commande.Parameters.AddWithValue("@noProjet", noProjet);
                con.Open();
                int i = commande.ExecuteNonQuery();

                getAllProjets(); 
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public List<Employes> GetEmployesLibres()
        {
            List<Employes> liste = new List<Employes>();

            using MySqlConnection con = new MySqlConnection(connectionString);
            using MySqlCommand commande = con.CreateCommand();

            commande.CommandText =
                "SELECT * FROM employes " +
                "WHERE matricule NOT IN (" +
                    "SELECT noEmploye FROM projetEmploye" +
                ");";

            con.Open();
            using MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                liste.Add(new Employes(r));
            }

            return liste;
        }


        public bool AssignerEmployes(string noProjet, List<Employes> employes, decimal heuresTravaille)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;

                foreach (var emp in employes)
                { 
                    decimal tauxHoraire = 0;
                    commande.CommandText = "SELECT tauxHoraires FROM employes WHERE matricule=@matricule";
                    commande.Parameters.AddWithValue("@matricule", emp.Matricule);
                    
                    
                        commande.CommandText = @"INSERT INTO projetEmploye (noEmploye, noProjet, heuresTravaille, tauxHoraires) 
                                          VALUES (@noEmploye, @noProjet, @heuresTravaille, @tauxHoraires)";
                        commande.Parameters.AddWithValue("@noEmploye", emp.Matricule);
                        commande.Parameters.AddWithValue("@noProjet", noProjet);
                        commande.Parameters.AddWithValue("@heuresTravaille", heuresTravaille);
                        commande.Parameters.AddWithValue("@tauxHoraires", tauxHoraire);
                        con.Open();
                        int i = commande.ExecuteNonQuery();
                        using MySqlCommand commande2 = new MySqlCommand();
                        commande2.Connection = con;
                        commande2.CommandText = "select LAST_INSERT_ID() ";
                        var res = commande2.ExecuteScalar();
                        getAllProjets();

                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }




    }
}
