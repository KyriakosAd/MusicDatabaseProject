using Microsoft.Data.Sqlite;

namespace MusicApp
{
    internal class MusicDAO
    {
        string connectionString = "Data Source=../../../MusicDB.db";

        public List<object> findPublisher(string inputTerm)
        {
            List<object> returnList = new List<object>();

            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            SqliteCommand command = new SqliteCommand(
                "SELECT p.Name FROM Album a JOIN Publisher p ON a.PublisherID = p.PublisherID WHERE a.Title = @input",
                connection);
            command.Parameters.AddWithValue("@input", inputTerm);
            command.Connection = connection;

            using (SqliteDataReader reader = command.ExecuteReader()) {
                while(reader.Read())
                {
                    object a = new
                    {
                        Name = reader.GetString(0)
                    };

                    returnList.Add(a);
                }
            }
            connection.Close();

            return returnList;
        }

        public List<object> mostCollaborations()
        {
            List<object> returnList = new List<object>();

            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            SqliteCommand command = new SqliteCommand(
                "SELECT s.Title, COUNT(c.ArtistID) AS num_artists FROM Song s JOIN Creates c ON s.SongID = c.SongID GROUP BY s.Title ORDER BY num_artists DESC, s.Title asc LIMIT 1",
                connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    object a = new
                    {
                        Title = reader.GetString(0),
                        num_artists = reader.GetInt32(1)
                    };

                    returnList.Add(a);
                }
            }
            connection.Close();

            return returnList;
        }

        public List<object> findProducers(string inputTerm)
        {
            List<object> returnList = new List<object>();

            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            SqliteCommand command = new SqliteCommand(
                "SELECT DISTINCT p.Name FROM Album a JOIN Song s ON a.AlbumID = s.AlbumID JOIN ProducedBy pb ON s.SongID = pb.SongID JOIN Producer p ON pb.ProducerID = p.ProducerID WHERE a.Title = @input",
                connection);
            command.Parameters.AddWithValue("@input", inputTerm);
            command.Connection = connection;

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    object a = new
                    {
                        Name = reader.GetString(0)
                    };

                    returnList.Add(a);
                }
            }
            connection.Close();

            return returnList;
        }

        public List<object> searchByAlbum(string inputTerm)
        {
            List<object> returnList = new List<object>();

            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            SqliteCommand command = new SqliteCommand(
                "SELECT COUNT(*) FROM Album a JOIN Song s ON a.AlbumID = s.AlbumID WHERE a.Title = @input",
                connection);
            command.Parameters.AddWithValue("@input", inputTerm);
            command.Connection = connection;

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    object a = new
                    {
                        num_songs = reader.GetInt32(0)
                    };

                    returnList.Add(a);
                }
            }
            connection.Close();

            return returnList;
        }

        public List<object> findAlbumsOfArtist(string inputTerm)
        {
            List<object> returnList = new List<object>();

            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            SqliteCommand command = new SqliteCommand(
                "SELECT DISTINCT a.Title FROM Album a JOIN Song s ON a.AlbumID = s.AlbumID JOIN Creates c ON s.SongID = c.SongID JOIN Artist ar ON c.ArtistID = ar.ArtistID WHERE ar.Name = @input",
                connection);
            command.Parameters.AddWithValue("@input", inputTerm);
            command.Connection = connection;

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    object a = new
                    {
                        Title = reader.GetString(0)
                    };

                    returnList.Add(a);
                }
            }
            connection.Close();

            return returnList;
        }

        public List<object> findAlbumsWithMostSongs()
        {
            List<object> returnList = new List<object>();

            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            SqliteCommand command = new SqliteCommand(
                "SELECT a.Title, COUNT(s.SongID) AS num_songs FROM Album a JOIN Song s ON a.AlbumID = s.AlbumID GROUP BY a.Title ORDER BY num_songs DESC",
                connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    object a = new
                    {
                        Title = reader.GetString(0),
                        num_songs = reader.GetInt32(1)
                    };

                    returnList.Add(a);
                }
            }
            connection.Close();

            return returnList;
        }

        public List<object> discoverNewReleases()
        {
            List<object> returnList = new List<object>();

            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            SqliteCommand command = new SqliteCommand(
                "SELECT a.Title, a.ReleaseDate FROM Album a WHERE strftime('%Y', a.ReleaseDate) = '2022'",
                connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    object a = new
                    {
                        Title = reader.GetString(0),
                        ReleaseDate = reader.GetDateTime(1)
                    };

                    returnList.Add(a);
                }
            }
            connection.Close();

            return returnList;
        }

        public List<object> searchByArtistAndProducer(string inputTerm)
        {
            List<object> returnList = new List<object>();
            if (!inputTerm.Contains(','))
            {
                return returnList;
            }

            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            SqliteCommand command = new SqliteCommand(
                "SELECT s.Title FROM Song s JOIN Creates c ON s.SongID = c.SongID JOIN Artist a ON c.ArtistID = a.ArtistID JOIN ProducedBy pb ON s.SongID = pb.SongID JOIN Producer p ON pb.ProducerID = p.ProducerID WHERE a.Name = @input1 AND p.Name = @input2",
                connection);

            inputTerm = string.Join("", inputTerm.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            string inputTerm1 = inputTerm.Split(',')[0];
            string inputTerm2 = inputTerm.Split(',')[1];
            command.Parameters.AddWithValue("@input1", inputTerm1);
            command.Parameters.AddWithValue("@input2", inputTerm2);
            command.Connection = connection;

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    object a = new
                    {
                        Title = reader.GetString(0)
                    };

                    returnList.Add(a);
                }
            }
            connection.Close();

            return returnList;
        }

        public List<object> findDurationOfAlbum(string inputTerm)
        {
            List<object> returnList = new List<object>();

            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            SqliteCommand command = new SqliteCommand(
                "SELECT SUM(s.Duration) FROM Album a JOIN Song s ON a.AlbumID = s.AlbumID WHERE a.Title = @input",
                connection);
            command.Parameters.AddWithValue("@input", inputTerm);
            command.Connection = connection;

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    object a = new
                    {
                        album_duration = reader.GetInt32(0)
                    };

                    returnList.Add(a);
                }
            }
            connection.Close();

            return returnList;
        }

        public List<object> findLastAlbumReleaseDate(string inputTerm)
        {
            List<object> returnList = new List<object>();

            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            SqliteCommand command = new SqliteCommand(
                "SELECT a.Title, a.ReleaseDate FROM Album a JOIN Song s ON a.AlbumID = s.AlbumID JOIN Creates c ON s.SongID = c.SongID JOIN Artist ar ON c.ArtistID = ar.ArtistID WHERE ar.Name = @input ORDER BY a.ReleaseDate DESC LIMIT 1",
                connection);
            command.Parameters.AddWithValue("@input", inputTerm);
            command.Connection = connection;

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    object a = new
                    {
                        Title = reader.GetString(0),
                        ReleaseDate = reader.GetDateTime(1)
                    };

                    returnList.Add(a);
                }
            }
            connection.Close();

            return returnList;
        }
    }
}
