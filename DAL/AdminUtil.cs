using MaxsPetCare.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MaxsPetCare.DAL
{
    public class AdminUtil
    {
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppInfo.DBName};Integrated Security=True");

        internal void UpdateStatusToSeen(string TableName)
        {
            string query = $"UPDATE {TableName} SET seen = 1 WHERE seen = 0";
            SqlCommand cmd = new SqlCommand(query, Conn);
            Conn.Open();
            cmd.ExecuteNonQuery();
            Conn.Close();
        }

        internal List<ContactUs> AllContactUs()
        {
            DataTable td = new DataTable();
            List<ContactUs> list = new List<ContactUs>();
            try
            {
                string sqlquery = "SELECT * FROM contact_us ORDER BY id DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    ContactUs obj = new ContactUs
                    {
                        ID = Convert.ToInt32(row["id"]),
                        Name = Convert.ToString(row["name"]),
                        Email = Convert.ToString(row["email"]),
                        Mobile = Convert.ToString(row["mobile"]),
                        Subject = Convert.ToString(row["subject"]),
                        Message = Convert.ToString(row["address"]),
                        DateTime = Convert.ToDateTime(row["message"])
                    };

                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return list;
        }

        // Packages operations starts here
        internal bool AddPackage(TrainingPackages obj)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO packages (name, status, type) VALUES(@name, @status, @type)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("name", obj.Name));
                cmd.Parameters.Add(new SqlParameter("status", obj.Status));
                cmd.Parameters.Add(new SqlParameter("type", obj.Type));

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

        internal List<TrainingPackages> AllPackages()
        {
            DataTable td = new DataTable();
            List<TrainingPackages> list = new List<TrainingPackages>();
            try
            {
                string sqlquery = "SELECT * FROM packages ORDER BY package_id DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    TrainingPackages obj = new TrainingPackages
                    {
                        ID = Convert.ToInt32(row["package_id"]),
                        Name = Convert.ToString(row["name"]),
                        Status = Convert.ToInt32(row["status"]),
                        Type = Convert.ToInt32(row["type"]),
                    };

                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return list;
        }

        internal TrainingPackages GetPackageByID(int id)
        {
            DataTable td = new DataTable();
            TrainingPackages obj = new TrainingPackages();
            try
            {
                string sqlquery = "SELECT * FROM packages where package_id = @id";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("id", id));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                Conn.Open();

                adp.Fill(td);

                obj.ID = Convert.ToInt32(td.Rows[0]["package_id"]);
                obj.Name = Convert.ToString(td.Rows[0]["name"]);
                obj.Status = Convert.ToInt32(td.Rows[0]["status"]);
                obj.Type = Convert.ToInt32(td.Rows[0]["type"]);
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return obj;

        }

        internal bool UpdatePackage(TrainingPackages obj)
        {
            bool result = false;
            try
            {
                string query = "UPDATE packages SET name = @name, status = @status, type = @type WHERE package_id = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("name", obj.Name));
                cmd.Parameters.Add(new SqlParameter("status", obj.Status));
                cmd.Parameters.Add(new SqlParameter("type", obj.Type));

                cmd.Parameters.Add(new SqlParameter("id", obj.ID));

                Conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return result;
        }
        // Packages operations ends here

        // Boarding operations starts here
        internal List<Boarding> AllBoardings()
        {
            DataTable td = new DataTable();
            List<Boarding> list = new List<Boarding>();

            AccountUtil accountUtil = new AccountUtil();
            try
            {
                string sqlquery = "SELECT * FROM boarding ORDER BY boarding_id DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
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
                    obj.UserName = accountUtil.GetUserByID(obj.UserID).Name;
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

        internal void UpdateBoardingsToSeen()
        {
            string query = "UPDATE boarding SET seen = 1 WHERE seen = 0";
            SqlCommand cmd = new SqlCommand(query, Conn);
            Conn.Open();
            cmd.ExecuteNonQuery();
            Conn.Close();
        }
        // Boarding operations ends here
        
        internal List<Consulting> AllConsulting()
        {
            DataTable td = new DataTable();
            List<Consulting> list = new List<Consulting>();

            try
            {
                string sqlquery = "SELECT * FROM consulting ORDER BY consult_id DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);

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

        internal bool UpdateConsulting(int ID, int Status)
        {
            bool result = false;
            try
            {
                string query = "UPDATE consulting SET status = @status WHERE consult_id = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("status", Status));
                cmd.Parameters.Add(new SqlParameter("id", ID));

                Conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return result;
        }
    }
}