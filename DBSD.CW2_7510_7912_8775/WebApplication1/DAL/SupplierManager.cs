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
    public class SupplierManager : DbConnection
    {
        public void Delete(int SupplierId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Supplier_Delete", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SupplierId", SupplierId);

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

        public void Update(int SupplierId, Supplier Supplier)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Supplier_Update", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SupplierId", SupplierId);
                command.Parameters.AddWithValue("@Name", Supplier.Name);
                command.Parameters.AddWithValue("@Address", Supplier.Address);

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

        public Supplier GetOne(int SupplierId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Supplier_One_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SupplierId", SupplierId);
                conn.Open();
                Supplier r = new Supplier();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        r.SupplierId = reader.GetInt32(reader.GetOrdinal("SupplierId"));
                        r.Name = reader.GetString(reader.GetOrdinal("Name"));
                        r.Address = reader.GetString(reader.GetOrdinal("Address"));
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

        public List<Supplier> GetAll()
        {
            var conn = Connection;
            List<Supplier> Suppliers = new List<Supplier>();
            try
            {
                SqlCommand command = new SqlCommand("SP_Supplier_All_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Supplier r = new Supplier()
                        {
                            SupplierId = reader.GetInt32(reader.GetOrdinal("SupplierId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                        };
                        Suppliers.Add(r);
                    }
                }

                return Suppliers;
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
        
        public void Create(Supplier Supplier)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Supplier_Add", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", Supplier.Name);
                command.Parameters.AddWithValue("@Address", Supplier.Address);

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