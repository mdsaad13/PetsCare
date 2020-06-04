using System;
using MaxsPetCare.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MaxsPetCare.DAL
{
    public class HomeUtil
    {
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppInfo.DBName};Integrated Security=True");

        internal bool AddContactUs(ContactUs model)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO contact_us (name, email, mobile, subject, message, seen, datetime) VALUES(@name, @email, @mobile, @subject, @message, @seen, @datetime)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("email", model.Email));
                cmd.Parameters.Add(new SqlParameter("mobile", model.Mobile));
                cmd.Parameters.Add(new SqlParameter("subject", model.Subject));
                cmd.Parameters.Add(new SqlParameter("message", model.Message));
                cmd.Parameters.Add(new SqlParameter("seen", model.Seen));
                cmd.Parameters.Add(new SqlParameter("datetime", model.DateTime));

                Conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception exp)
            {

            }
            finally
            {
                Conn.Close();
            }
            return result;
        }

        internal bool AddBoarding(Boarding obj)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO boarding (user_id, pet_type, pet_breed, address, pickupdate, dropdate, amount, seen, datetime) VALUES(@user_id, @pet_type, @pet_breed, @address, @pickupdate, @dropdate, @amount, @seen, @datetime)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("user_id", obj.UserID));
                cmd.Parameters.Add(new SqlParameter("pet_type", obj.PetType));
                cmd.Parameters.Add(new SqlParameter("pet_breed", obj.PetBreed));
                cmd.Parameters.Add(new SqlParameter("address", obj.PickUpAddress));
                cmd.Parameters.Add(new SqlParameter("pickupdate", obj.PickUpDate));
                cmd.Parameters.Add(new SqlParameter("dropdate", obj.DropDate));
                cmd.Parameters.Add(new SqlParameter("amount", obj.Amount));
                cmd.Parameters.Add(new SqlParameter("seen", obj.Seen));
                cmd.Parameters.Add(new SqlParameter("datetime", obj.DateTime));

                Conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception exp)
            {

            }
            finally
            {
                Conn.Close();
            }
            return result;
        }
        
        internal List<Boarding> AllBoardingsByUser(int ID)
        {
            DataTable td = new DataTable();
            List<Boarding> list = new List<Boarding>();

            try
            {
                string sqlquery = "SELECT * FROM boarding WHERE user_id = @user_id ORDER BY boarding_id DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("user_id", ID));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                Conn.Close();
                foreach (DataRow row in td.Rows)
                {
                    Boarding obj = new Boarding
                    {
                        ID = Convert.ToInt32(row["boarding_id"]),
                        UserID = Convert.ToInt32(row["user_id"]),
                        PetType = Convert.ToString(row["pet_type"]),
                        PetBreed = Convert.ToString(row["pet_breed"]),
                        PickUpAddress = Convert.ToString(row["address"]),
                        PickUpDate = Convert.ToDateTime(row["pickupdate"]),
                        DropDate = Convert.ToDateTime(row["dropdate"]),
                        Amount = Convert.ToInt64(row["amount"]),
                        DateTime = Convert.ToDateTime(row["datetime"]),
                    };
                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            finally
            {

            }
            return list;
        }

        internal void DeleteBoarding(int BoardingID, int UserID)
        {
            try
            {
                string query = "DELETE from boarding where boarding_id = @boarding_id AND user_id = @user_id";
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("boarding_id", BoardingID));
                cmd.Parameters.Add(new SqlParameter("user_id", UserID));

                Conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Conn.Close();
            }
        }

        internal bool AddConsulting(Consulting obj)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO consulting (user_id, pet_type, pet_breed, address, pickupdate, issue, status, seen, datetime) VALUES(@user_id, @pet_type, @pet_breed, @address, @pickupdate, @issue, @status, @seen, @datetime)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("user_id", obj.UserID));
                cmd.Parameters.Add(new SqlParameter("pet_type", obj.PetType));
                cmd.Parameters.Add(new SqlParameter("pet_breed", obj.PetBreed));
                cmd.Parameters.Add(new SqlParameter("address", obj.PickUpAddress));
                cmd.Parameters.Add(new SqlParameter("pickupdate", obj.PickUpDate));
                cmd.Parameters.Add(new SqlParameter("issue", obj.Issue));
                cmd.Parameters.Add(new SqlParameter("status", obj.Status));
                cmd.Parameters.Add(new SqlParameter("seen", obj.Seen));
                cmd.Parameters.Add(new SqlParameter("datetime", obj.DateTime));

                Conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception exp)
            {

            }
            finally
            {
                Conn.Close();
            }
            return result;
        }

        internal List<Consulting> AllConsultingsByUser(int ID)
        {
            DataTable td = new DataTable();
            List<Consulting> list = new List<Consulting>();

            try
            {
                string sqlquery = "SELECT * FROM consulting WHERE user_id = @user_id ORDER BY consult_id DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("user_id", ID));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                Conn.Close();
                foreach (DataRow row in td.Rows)
                {
                    Consulting obj = new Consulting
                    {
                        ID = Convert.ToInt32(row["consult_id"]),
                        UserID = Convert.ToInt32(row["user_id"]),
                        PetType = Convert.ToString(row["pet_type"]),
                        PetBreed = Convert.ToString(row["pet_breed"]),
                        PickUpAddress = Convert.ToString(row["address"]),
                        PickUpDate = Convert.ToDateTime(row["pickupdate"]),
                        Issue = Convert.ToString(row["issue"]),
                        Status = Convert.ToInt32(row["status"]),
                        DateTime = Convert.ToDateTime(row["datetime"]),
                    };
                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            finally
            {

            }
            return list;
        }

        internal bool AddTraining(Training obj)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO training (user_id, pet_type, pet_breed, address, pickupdate, package_id, seen, datetime) VALUES(@user_id, @pet_type, @pet_breed, @address, @pickupdate, @package_id, @seen, @datetime)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("user_id", obj.UserID));
                cmd.Parameters.Add(new SqlParameter("pet_type", obj.PetType));
                cmd.Parameters.Add(new SqlParameter("pet_breed", obj.PetBreed));
                cmd.Parameters.Add(new SqlParameter("address", obj.PickUpAddress));
                cmd.Parameters.Add(new SqlParameter("pickupdate", obj.PickUpDate));
                cmd.Parameters.Add(new SqlParameter("dropdate", obj.PackageID));
                cmd.Parameters.Add(new SqlParameter("seen", obj.Seen));
                cmd.Parameters.Add(new SqlParameter("datetime", obj.DateTime));

                Conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception exp)
            {

            }
            finally
            {
                Conn.Close();
            }
            return result;
        }
    }
}