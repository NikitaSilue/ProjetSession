using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using PROJETSESSION.Classes;
using PROJETSESSION.Pages.Connexion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PROJETSESSION.Singletons
{
    internal class SingletonAdmin
    {

        private static SingletonAdmin instance = null;
        private string nomUtilisateur;
        private string motDePasseHash;
        private bool connecte;
        private string connectionString;
        ObservableCollection<Admin> listeAdmin;

        private SingletonAdmin() 
        {

            connectionString = "Server= cours.cegep3r.info;Database=a2025_420335-345ri_greq30;Uid=2335631;Pwd=2335631;";
            listeAdmin = new ObservableCollection<Admin>();
            connecte = false;
        }

       
        public static SingletonAdmin getInstance()
        {
            if (instance == null)

                instance = new SingletonAdmin();
            return instance;
        }

      

        public bool EstConnecte
        {
            get => connecte;
        }


        public bool connexion(string utilisateur, string motDePasse)
        {
            if (nomUtilisateur == null)
                return false; ;
            if (utilisateur == nomUtilisateur && VerifierMotDePasse(motDePasse, motDePasseHash)) 
            {
                connecte = true;
                return true;
            }
            return false;
        }

        
        private bool VerifierMotDePasse(string motDePasse, string hash)
        {
            string hashInput = CrypterMotDePasse(motDePasse);
            return hashInput == hash;
        }

        private string CrypterMotDePasse(string motDePasse)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(motDePasse));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }


        public bool CompteAdminExiste()
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "Select COUNT(*) from admin";
                con.Open();
                int count = Convert.ToInt32(commande.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }


       


        public string Connecter(string nomUtilisateur, string motDePasse)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nomUtilisateur))
                    return "Le nom d'utilisateur est obligatoire.";

                if (string.IsNullOrWhiteSpace(motDePasse))
                    return "Le mot de passe est obligatoire.";

                string motDePasseCrypte = CrypterMotDePasse(motDePasse);
                Debug.WriteLine("Hash calculé : " + motDePasseCrypte);

                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "Select COUNT(*) From admin WHERE nomUtilisateur = @nomUtilisateur AND motDePasse = @motDePasse";
                commande.Parameters.AddWithValue("@nomUtilisateur", nomUtilisateur);
                commande.Parameters.AddWithValue("@motDePasse", motDePasseCrypte);
                con.Open();
                int count = Convert.ToInt32(commande.ExecuteScalar());

                if (count == 1)
                {
                    connecte = true;
                    
                    return "";

                }
                else
                {
                    return "Nom d'utilisateur ou mot de passe incorrect. Veuillez réesseyer svp.";
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                return "Erreur lors de la connexion: " + ex.Message;
            }
        }

        public string CreerCompteAdmin(string nomUtilisateur, string motDePasse)
        {
            try
            {
               
                string motDePasseCrypte = CrypterMotDePasse(motDePasse);

                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into admin(nomUtilisateur,motDePasse) VALUES (@nomUtilisateur,@motDePasse)";
                commande.Parameters.AddWithValue("@nomUtilisateur", nomUtilisateur);
                commande.Parameters.AddWithValue("@motDePasse", motDePasseCrypte);
                con.Open();
                commande.ExecuteNonQuery();

                return "";

            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                return "Erreur lors de la création du compte:" + ex.Message;
            }
        }

        public void Deconnecter()
        {
            connecte = false;
        }




    }
}

