using MaxsPetCare.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MaxsPetCare.DAL
{
    public class AccountUtil
    {
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppInfo.DBName};Integrated Security=True");

        public bool UserLogin(string Email, string Pass)
        {
            // default Login failed
            bool result = false;
            try
            {
                string query = "SELECT * FROM users WHERE email = @email AND password = @password";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("email", Email));
                cmd.Parameters.Add(new SqlParameter("password", Pass));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    HttpContext.Current.Session["UserID"] = dt.Rows[0]["user_id"];
                    HttpContext.Current.Session["Name"] = dt.Rows[0]["name"];
                    result = true;
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public bool AdminLogin(string Email, string Pass)
        {
            bool result = false;
            try
            {
                string query = "SELECT * FROM admin WHERE email = @email AND password = @password";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("email", Email));
                cmd.Parameters.Add(new SqlParameter("password", Pass));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    result = true;
                    HttpContext.Current.Session["AdminID"] = dt.Rows[0]["admin_id"];
                    HttpContext.Current.Session["Name"] = dt.Rows[0]["name"];
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        internal bool AddUser(Users model)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO users (name, email, mobile, password, address, created_date) VALUES(@name, @email, @mobile, @password, @address, @created_date)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("email", model.Email));
                cmd.Parameters.Add(new SqlParameter("mobile", model.Mobile));
                cmd.Parameters.Add(new SqlParameter("password", model.Password));
                cmd.Parameters.Add(new SqlParameter("address", model.Address));
                cmd.Parameters.Add(new SqlParameter("created_date", model.DateTime));

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

        internal List<Users> AllUsers()
        {
            DataTable td = new DataTable();
            List<Users> list = new List<Users>();
            try
            {
                string sqlquery = "SELECT * FROM users ORDER BY user_id DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Users obj = new Users
                    {
                        ID = Convert.ToInt32(row["user_id"]),
                        Name = Convert.ToString(row["name"]),
                        Email = Convert.ToString(row["email"]),
                        Mobile = Convert.ToString(row["mobile"]),
                        Password = Convert.ToString(row["password"]),
                        Address = Convert.ToString(row["address"]),
                        DateTime = Convert.ToDateTime(row["created_date"])
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

        internal Users GetUserByID(int id)
        {
            DataTable td = new DataTable();
            Users obj = new Users();
            try
            {
                string sqlquery = "SELECT * FROM users where user_id = @id";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("id", id));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                Conn.Open();

                adp.Fill(td);

                obj.ID = Convert.ToInt32(td.Rows[0]["user_id"]);
                obj.Name = Convert.ToString(td.Rows[0]["name"]);
                obj.Email = Convert.ToString(td.Rows[0]["email"]);
                obj.Mobile = Convert.ToString(td.Rows[0]["mobile"]);
                obj.Password = Convert.ToString(td.Rows[0]["password"]);
                obj.Address = Convert.ToString(td.Rows[0]["address"]);
                obj.DateTime = Convert.ToDateTime(td.Rows[0]["created_date"]);
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return obj;

        }

        internal bool UpdateUser(Users model)
        {
            bool result = false;
            try
            {
                string query = "UPDATE users SET name = @name, email = @email, mobile = @mobile, address = @address WHERE user_id = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("email", model.Email));
                cmd.Parameters.Add(new SqlParameter("mobile", model.Mobile));
                cmd.Parameters.Add(new SqlParameter("address", model.Address));

                cmd.Parameters.Add(new SqlParameter("id", model.ID));

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

        internal bool UpdateUserPassword(string NewPassword, int ID)
        {
            bool result = false;
            try
            {
                string query = "UPDATE users SET password = @password WHERE user_id = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("password", NewPassword));

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

        internal List<Admin> AllAdmins(int IdNotInclude)
        {
            DataTable td = new DataTable();
            List<Admin> list = new List<Admin>();
            try
            {
                string sqlquery = "SELECT * FROM admin WHERE admin_id <> @admin_id ORDER BY admin_id DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("admin_id", IdNotInclude));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Admin obj = new Admin
                    {
                        ID = Convert.ToInt32(row["admin_id"]),
                        Name = Convert.ToString(row["name"]),
                        Email = Convert.ToString(row["email"]),
                        Mobile = Convert.ToString(row["mobile"]),
                        Password = Convert.ToString(row["password"])
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

        internal Admin GetAdminByID(int id)
        {
            DataTable td = new DataTable();
            Admin obj = new Admin();
            try
            {
                string sqlquery = "SELECT * FROM admin where admin_id = @id";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("id", id));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                Conn.Open();

                adp.Fill(td);

                obj.ID = Convert.ToInt32(td.Rows[0]["admin_id"]);
                obj.Name = Convert.ToString(td.Rows[0]["name"]);
                obj.Email = Convert.ToString(td.Rows[0]["email"]);
                obj.Mobile = Convert.ToString(td.Rows[0]["mobile"]);
                obj.Password = Convert.ToString(td.Rows[0]["password"]);
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return obj;

        }

        internal bool UpdateAdmin(Admin model)
        {
            bool result = false;
            try
            {
                string query = "UPDATE admin SET name = @name, email = @email, mobile = @mobile WHERE admin_id = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("email", model.Email));
                cmd.Parameters.Add(new SqlParameter("mobile", model.Mobile));

                cmd.Parameters.Add(new SqlParameter("id", model.ID));

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

        internal bool UpdateAdminPassword(string NewPassword, int ID)
        {
            bool result = false;
            try
            {
                string query = "UPDATE admin SET password = @password WHERE admin_id = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("password", NewPassword));

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