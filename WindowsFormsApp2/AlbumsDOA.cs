using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp2
{
    internal class AlbumsDOA
    {
        //version 1 only contains fake data. No connection to actual db yet
        public List<Album> albums = new List<Album>();
        string connectionString = "datasource=localhost;port=3307;username=root;password=root;database=music2;";


        public List<Album> getAllAlbums()
        {
            //start with an empty list
            List<Album> returnThese = new List<Album>();

            //connect to the msql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //define the sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("SELECT `ID`, `ALBUM_TITLE`, `ARTIST`, `YEAR`, `IMAGE_NAME`, `DESCRIPTION` FROM `albums`", connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album a = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5),
                    };
                    returnThese.Add(a);
                }


            }
            connection.Close();

            return returnThese;
        }
        public List<Album> searchTitles(string searchTerm)
        {
            //start with an empty list
            List<Album> returnThese = new List<Album>();

            //connect to the msql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            String searchWildPhrase = "%" + searchTerm + "%";

            //define the sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("SELECT `ID`, `ALBUM_TITLE`, `ARTIST`, `YEAR`, `IMAGE_NAME`, `DESCRIPTION` FROM `albums` WHERE ALBUM_TITLE LIKE @search", connection);
            command.Parameters.AddWithValue("@search", searchWildPhrase);
            command.Connection = connection;


            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album a = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5),
                    };
                    returnThese.Add(a);
                }


            }
            connection.Close();

            return returnThese;
        }

        internal int addOneAlbum(Album album)
        {
            //connect to the msql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //define the sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("INSERT INTO `albums`( `ALBUM_TITLE`, `ARTIST`, `YEAR`, `IMAGE_NAME`, `DESCRIPTION`) VALUES (@albumtitle, @artist, @year, @imageURL, @description)", connection);

            command.Parameters.AddWithValue("albumtitle", album.AlbumName);

            command.Parameters.AddWithValue("@artist", album.ArtistName);

            command.Parameters.AddWithValue("@year", album.Year);

            command.Parameters.AddWithValue("@imageURL", album.ImageURL);

            command.Parameters.AddWithValue("@description", album.Description);
           
            int newRows = command.ExecuteNonQuery();
            
            connection.Close();


            return newRows;
        }
    }

   
}


