using Exercice02.utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Exercice02.Models
{
    public class Inscription
    {
        // Propriétés de la classe
        public string Name { get; set; }
        public string Email { get; set; }
        public int Level { get; set; }
        public string Password { get; set; }

        // Ajouter un étudiant
        public static ErrorManager AddStudent(Inscription student)
        {
            try
            {
                // Récupérer la connexion à la base de données
                SqlConnection connection = getDBConection();
                // Créer la requête d'insertion et l'objet de commande
                string query = "INSERT INTO [Student] (Name, Email, LevelId, Password) VALUES (@Name, @Email, @LevelId, @Password)";
                SqlCommand command = new SqlCommand(query, connection);
                // Ajouter les paramètres à la commande
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Email", student.Email);
                // Générer un mot de passe par défaut et garder une copie pour l'envoyer par email
                string password = generateDefaultPassword(student.Name);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@LevelId", student.Level);
                // Exécuter la commande et fermer la connexion
                command.ExecuteNonQuery();
                connection.Close();

                // Send email to student
                Email email = new Email(student.Email, "Inscription", "Votre compte a été créé avec succès. Voici vos informations\nEmail: " + student.Email + "\nMot de passe par defaut: " + password);
                return Mail.SendMail(email);
            }catch (Exception e)
            {
                return new ErrorManager(false, e.Message, null);
            }
        }
        // Générer un mot de passe par défaut
        private static string generateDefaultPassword(string name)
        {
            return name + RandomString(5);
        }
        // Générer une chaine de caractères aléatoire
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        // Récupérer les niveaux d'études
        public static List<dynamic> getLevels()
        {
            List<dynamic> levels = new List<dynamic>();
            try
            {
                SqlConnection connection = getDBConection();
                string query = "SELECT * FROM Level";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Créer un objet dynamique pour représenter chaque niveau
                    dynamic level = new System.Dynamic.ExpandoObject();
                    level.value = reader["Id"];
                    level.text = reader["Name"];
                    levels.Add(level);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while retrieving levels: " + e.Message);
            }
            return levels;
        }
        // Récupérer la connexion à la base de données
        private static SqlConnection getDBConection()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Elearning;Integrated Security=True;Connect Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}