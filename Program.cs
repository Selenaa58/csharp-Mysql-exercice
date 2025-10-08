// etablir la connexion à la base de données
using System.Linq.Expressions;
using MySqlConnector;

string connectionString = "Server=localhost;Database=csharp;User ID=root;Password=;";
using (var connection = new MySqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connection réussie !"); //utiliser la connexion ici

        // 1ere requete pour recuperer la liste de tout les produits dans la table food 
        MySqlCommand command = new MySqlCommand("SELECT * FROM food", connection);

        // execute la requete (reader: une boite qui contient la liste de TOUT les éléments de la base)
        MySqlDataReader reader = command.ExecuteReader();

        // boucle pour parcourir chaque element de la réponse 
        while (reader.Read())
        {
            string title = reader.GetString("title");
            int amount = reader.GetInt32("amount");
            Console.WriteLine($"le produit {title} est dispo en {amount} exemplaires");
        }

        reader.Close(); //fin de la lecture 

        // inserer un nouveau produit dans la table food 
        String createQuery = "INSERT INTO food(title, amount, description)VALUES(@title, @amount, @description)";
        MySqlCommand createCommand = new MySqlCommand(createQuery, connection);
        createCommand.Parameters.AddWithValue("@title", "Sunday Vanille");
        createCommand.Parameters.AddWithValue("@amount", "17000");
        createCommand.Parameters.AddWithValue("@description", "Petite fraicheur des printemps");
        createCommand.ExecuteNonQuery(); // declancher la requete 



    }
    catch (MySqlException ex)
    {
        Console.WriteLine($"Erreur de connexion {ex.Message}");
        return;
    }
    finally
    {
        connection.Close();
    }
}




