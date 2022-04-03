using ProjektDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDB.ORM
{
    public class RequirementRepository
    {
        public static List<Requirement> FindAllRequirements()
        {
            List<Requirement> result = new List<Requirement>();
            DBConnection db = new DBConnection();

            db.Connect();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[requirement]", db.connection); //
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int i = -1;
                Requirement tmpReq = new Requirement();
                tmpReq.Id = reader.GetInt32(++i);
                tmpReq.Description = reader.GetString(++i);
                tmpReq.State = reader.GetString(++i);
                tmpReq.UserId = reader.GetInt32(++i);
                tmpReq.GameId = reader.GetInt32(++i);
                tmpReq.ReservationId = reader.GetInt32(++i);
                tmpReq.StockId = reader.GetInt32(++i);

                result.Add(tmpReq);
            }

            db.Close();
            return result;
        }
        public static int CreateRequirement(Requirement requirement)
        {
            DBConnection db = new DBConnection();

            db.Connect();

            SqlCommand cmdGame = new SqlCommand("INSERT INTO [dbo].[game] VALUES ( " +
                "@description, @state," +
                " @user_id, @game_id, @reservation_id, @stock_id)",
                db.connection);

            cmdGame.Parameters.AddWithValue("@description", requirement.Description);
            cmdGame.Parameters.AddWithValue("@state", requirement.State);
            cmdGame.Parameters.AddWithValue("@user_id", requirement.UserId);
            cmdGame.Parameters.AddWithValue("@game_id", requirement.GameId);
            cmdGame.Parameters.AddWithValue("@reservation_id", requirement.ReservationId);
            cmdGame.Parameters.AddWithValue("@stock_id", requirement.StockId);

            int newGame = (Int32)cmdGame.ExecuteScalar();

            db.Close();
            return newGame;
        }
        public static void CompleteReq(int id)
        {
            DBConnection db = new DBConnection();

            db.Connect();
            SqlCommand cmdUser = new SqlCommand("UPDATE [dbo].[requirement] " +
                "state= \"Zpracováno\" where id = @id",
                db.connection);

            cmdUser.Parameters.AddWithValue("@id", id);

            cmdUser.ExecuteNonQuery();

            db.Close();
        }
    }
}
