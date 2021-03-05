using DBSD_CW2_7510_8775_7912.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DBSD_CW2_7510_8775_7912.DAL
{
    public class RoleManager : DbConnection
    {
        public void Delete(int RoleId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Role_Delete", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@RoleId", RoleId);

                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public void Update(int RoleId, Role role)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Role_Update", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@RoleId", RoleId);
                command.Parameters.AddWithValue("@Name", role.Name);

                conn.Open();
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public Role GetOne(int RoleId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Role_One_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoleId", RoleId);
                conn.Open();
                Role r = new Role();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        r.RoleId = reader.GetInt32(reader.GetOrdinal("RoleId"));
                        r.Name = reader.GetString(reader.GetOrdinal("Name"));
                    }
                }
                return r;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public List<Role> GetAll()
        {
            var conn = Connection;
            List<Role> roles = new List<Role>();
            try
            {
                SqlCommand command = new SqlCommand("SP_Role_All_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Role r = new Role()
                        {
                            RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };
                        roles.Add(r);
                    }
                }

                return roles;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        
        public void Create(Role role)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Role_Add", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", role.Name);

                conn.Open();
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
    }
}