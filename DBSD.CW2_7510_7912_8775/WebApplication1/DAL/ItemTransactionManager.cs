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
    public class ItemTransactionManager : DbConnection
    {
        public void Delete(int ItemTransactionId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemTransaction_Delete", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ItemTransactionId", ItemTransactionId);

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

        public void Update(int ItemTransactionId, ItemTransaction ItemTransaction)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemTransaction_Update", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ItemTransactionId", ItemTransactionId);
                command.Parameters.AddWithValue("@Notes", ItemTransaction.Notes);
                command.Parameters.AddWithValue("@EmployeeId", ItemTransaction.Employee.EmployeeId);
                command.Parameters.AddWithValue("@Operation", ItemTransaction.Operation);

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

        public ItemTransaction GetOne(int ItemTransactionId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemTransaction_One_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemTransactionId", ItemTransactionId);
                conn.Open();
                ItemTransaction r = new ItemTransaction();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        r.ItemTransactionId = reader.GetInt32(reader.GetOrdinal("ItemTransactionId"));
                        r.Operation = reader.GetInt32(reader.GetOrdinal("Operation"));
                        r.Notes = reader.GetString(reader.GetOrdinal("Notes"));
                        r.Employee = new Employee()
                        {
                            FullName = reader.GetString(reader.GetOrdinal("EmployeeName")),
                            EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                        };
                        r.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
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

        public List<ItemTransaction> GetAll()
        {
            var conn = Connection;
            List<ItemTransaction> ItemTransactions = new List<ItemTransaction>();
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemTransaction_All_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ItemTransaction r = new ItemTransaction()
                        {
                            ItemTransactionId = reader.GetInt32(reader.GetOrdinal("ItemTransactionId")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            Operation = reader.GetInt32(reader.GetOrdinal("Operation")),
                            Employee = new Employee() 
                            { 
                                EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                FullName = reader.GetString(reader.GetOrdinal("EmployeeName"))
                            } 
                        };
                        ItemTransactions.Add(r);
                    }
                }

                return ItemTransactions;
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
        
        public void Create(ItemTransaction ItemTransaction)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemTransaction_Add", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Notes", ItemTransaction.Notes);
                command.Parameters.AddWithValue("@EmployeeId", ItemTransaction.Employee.EmployeeId);
                command.Parameters.AddWithValue("@Operation", ItemTransaction.Operation);

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