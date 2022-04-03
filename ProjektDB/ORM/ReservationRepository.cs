using ProjektDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDB.ORM
{
    public class ReservationRepository
    {
        public static int CreateReservation(Reservation reservation)
        {
            DBConnection db = new DBConnection();

            db.Connect();

            SqlCommand cmdGame = new SqlCommand("INSERT INTO [dbo].[reservation] VALUES ( " +
                "@date, @Length," +
                " @add_info, @state, @game_id, @user_id)",
                db.connection);

            cmdGame.Parameters.AddWithValue("@date", reservation.Date);
            cmdGame.Parameters.AddWithValue("@Length", reservation.Length);
            cmdGame.Parameters.AddWithValue("@add_info", reservation.AddInfo);
            cmdGame.Parameters.AddWithValue("@state", reservation.State);
            cmdGame.Parameters.AddWithValue("@game_id", reservation.GameId);
            cmdGame.Parameters.AddWithValue("@user_id", reservation.UserId);

            int newGame = (Int32)cmdGame.ExecuteScalar();

            db.Close();
            return newGame;
        }
        public static int CreateRecieptForReservation(Reservation reservation)
        {
            DBConnection db = new DBConnection();

            db.Connect();
            Game game = GameRepository.FindGame(reservation.GameId);
            SqlCommand cmdGame = new SqlCommand("INSERT INTO [dbo].[reciept] VALUES ( " +
                "@date, @description," +
                " @price, @state, @user_id)",
                db.connection);

            cmdGame.Parameters.AddWithValue("@date", DateTime.Now);
            cmdGame.Parameters.AddWithValue("@description", "Účet za rezervaci");
            cmdGame.Parameters.AddWithValue("@price", game.Price);
            cmdGame.Parameters.AddWithValue("@state", "K zaplacení");
            cmdGame.Parameters.AddWithValue("@user_id", reservation.UserId);

            int newGame = (Int32)cmdGame.ExecuteScalar();

            db.Close();
            return newGame;
        }

        public static List<Reservation> FindAllReservations()
        {
            List<Reservation> result = new List<Reservation>();
            DBConnection db = new DBConnection();

            db.Connect();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[reservation]", db.connection); //
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int i = -1;
                Reservation tmpGame = new Reservation();
                tmpGame.Id = reader.GetInt32(++i);
                tmpGame.Date = reader.GetDateTime(++i);
                tmpGame.Length = reader.GetInt32(++i);
                tmpGame.AddInfo = reader.GetString(++i);
                tmpGame.GameId = reader.GetInt32(++i);
                tmpGame.UserId = reader.GetInt32(++i);

                result.Add(tmpGame);
            }

            db.Close();
            return result;
        }
        public static List<Reservation> FindAllReservationsByUserId(int id)
        {
            List<Reservation> result = new List<Reservation>();
            DBConnection db = new DBConnection();

            db.Connect();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[reservation] where id = @id", db.connection); //
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int i = -1;
                Reservation tmpGame = new Reservation();
                tmpGame.Id = reader.GetInt32(++i);
                tmpGame.Date = reader.GetDateTime(++i);
                tmpGame.Length = reader.GetInt32(++i);
                tmpGame.AddInfo = reader.GetString(++i);
                tmpGame.GameId = reader.GetInt32(++i);
                tmpGame.UserId = reader.GetInt32(++i);

                result.Add(tmpGame);
            }

            db.Close();
            return result;
        }
        public static void CancelReservation(Reservation reservation)
        {
            List<Reservation> result = new List<Reservation>();
            DBConnection db = new DBConnection();

            db.Connect();
            SqlCommand cmd = new SqlCommand("UPDATE reservation set state = \"Zrušeno\" where id = @id", db.connection); //
            cmd.Parameters.AddWithValue("@id", reservation.Id);
            cmd.ExecuteNonQuery();
        }
        public static void SetNotPaidReservation(Reservation reservation)
        {
            List<Reservation> result = new List<Reservation>();
            DBConnection db = new DBConnection();

            db.Connect();
            SqlCommand cmd = new SqlCommand("UPDATE reservation set state = \"Propadlá\" where id = @id", db.connection); //
            cmd.Parameters.AddWithValue("@id", reservation.Id);
            cmd.ExecuteNonQuery();
        }
    }
}
