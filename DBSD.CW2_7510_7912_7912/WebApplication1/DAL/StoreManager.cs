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
    public class StoreManager : DbConnection
    {
        public void Delete(int StoreId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Store_Delete", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@StoreId", StoreId);

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

        public void Update(int StoreId, Store Store)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Store_Update", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@StoreId", StoreId);
                command.Parameters.AddWithValue("@Name", Store.Name);
                command.Parameters.AddWithValue("@Square", Store.Square);
                command.Parameters.AddWithValue("@EmployeeId", Store.Employee.EmployeeId);

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

        public Store GetOne(int StoreId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Store_One_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StoreId", StoreId);

                conn.Open();
                Store r = new Store();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        r.StoreId = reader.GetInt32(reader.GetOrdinal("StoreId"));
                        r.Name = reader.GetString(reader.GetOrdinal("Name"));
                        r.Square = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Square")));

                        r.Employee = new Employee()
                        {
                            EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                            FullName = reader.GetString(reader.GetOrdinal("EmployeeName"))
                        };
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

        public List<Store> GetAll()
        {
            var conn = Connection;
            List<Store> Stores = new List<Store>();
            try
            {
                SqlCommand command = new SqlCommand("SP_Store_All_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Store r = new Store()
                        {
                            StoreId = reader.GetInt32(reader.GetOrdinal("StoreId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Square = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Square"))),
                            Employee = new Employee()
                            {
                                EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                FullName = reader.GetString(reader.GetOrdinal("EmployeeName"))
                            }

                        };
                        Stores.Add(r);
                    }
                }

                return Stores;
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
        
        public void Create(Store Store)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Store_Add", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", Store.Name);
                command.Parameters.AddWithValue("@Square", Store.Square);
                command.Parameters.AddWithValue("@EmployeeId", Store.Employee.EmployeeId);

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