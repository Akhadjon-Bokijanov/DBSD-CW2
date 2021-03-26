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
    public class  ItemTransactionListManager : DbConnection
    {
        public void Delete(int  ItemTransactionListId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemTransactionList_Delete", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ItemTransactionListId",  ItemTransactionListId);

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

        public void Update(int  ItemTransactionListId,  ItemTransactionList  ItemTransactionList)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemTransactionList_Update", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ItemTransactionListId",  ItemTransactionListId);
                command.Parameters.AddWithValue("@Amount", ItemTransactionList.Amount);
                command.Parameters.AddWithValue("@ItemTransactionId", ItemTransactionList.ItemTransaction.ItemTransactionId);
                command.Parameters.AddWithValue("@ItemId", ItemTransactionList.Item.ItemId);

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

        public  ItemTransactionList GetOne(int  ItemTransactionListId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemTransactionList_One_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemTransactionListId",  ItemTransactionListId);
                conn.Open();
                 ItemTransactionList r = new  ItemTransactionList();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        r. ItemTransactionListId = reader.GetInt32(reader.GetOrdinal("ItemTransactionListId"));
                        r.Amount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Amount")));
                        r.Item = new Item()
                        {
                            ItemId = reader.GetInt32(reader.GetOrdinal("ItemId")),
                            LocalName = reader.GetString(reader.GetOrdinal("LocalName")),
                            GlobalName = reader.GetString(reader.GetOrdinal("GlobalName")),
                            Unit = new Unit()
                            {
                                Name = reader.GetString(reader.GetOrdinal("UnitName")),
                            }
                        };
                        r.ItemTransaction = new ItemTransaction()
                        {
                            Notes = reader.GetString(reader.GetOrdinal("Notes")),
                            Operation = reader.GetInt32(reader.GetOrdinal("Operation"))
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

        public List<ItemTransactionList> GetAll()
        {
            var conn = Connection;
            List<ItemTransactionList>  ItemTransactionLists = new List<ItemTransactionList>();
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemTransactionList_All_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        ItemTransactionList r = new ItemTransactionList()
                        {
                            ItemTransactionListId = reader.GetInt32(reader.GetOrdinal("ItemTransactionListId")),
                            Amount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Amount"))),
                            Item = new Item()
                            {
                                ItemId = reader.GetInt32(reader.GetOrdinal("itemId")),
                                LocalName = reader.GetString(reader.GetOrdinal("LocalName")),
                                GlobalName = reader.GetString(reader.GetOrdinal("GlobalName")),
                                Unit = new Unit()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("UnitName")),
                                }
                            },
                             ItemTransaction = new ItemTransaction() {
                                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                Operation = reader.GetInt32(reader.GetOrdinal("Operation"))
                             }
                             
                        };
                         ItemTransactionLists.Add(r);
                    }
                }

                return  ItemTransactionLists;
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
        
        public void Create( ItemTransactionList  ItemTransactionList)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemTransactionList_Add", conn);
                
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Amount", ItemTransactionList.Amount);
                command.Parameters.AddWithValue("@ItemTransactionId", ItemTransactionList.ItemTransaction.ItemTransactionId);
                command.Parameters.AddWithValue("@ItemId", ItemTransactionList.Item.ItemId);

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