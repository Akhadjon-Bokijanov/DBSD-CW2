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
    public class ItemBindManager : DbConnection
    {
        public void Delete(int ItemBindId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemBind_Delete", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ItemBindId", ItemBindId);

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

        public void Update(int ItemBindId, ItemBind ItemBind)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemBind_Update", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ItemBindId", ItemBindId);
                command.Parameters.AddWithValue("@ChildAmount", ItemBind.ChildAmount);
                command.Parameters.AddWithValue("@ChildId", ItemBind.Child.ItemId);
                command.Parameters.AddWithValue("@ParentId", ItemBind.Parent.ItemId);

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

        public ItemBind GetOne(int ItemBindId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemBind_One_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemBindId", ItemBindId);
                conn.Open();
                ItemBind r = new ItemBind();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        r.ItemBindId = reader.GetInt32(reader.GetOrdinal("ItemBindId"));
                        r.ChildAmount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ChildAmount")));
                            r.Child = new Item()
                            {   
                                ItemId = reader.GetInt32(reader.GetOrdinal("ParentItemId")),
                                Unit = new Unit()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("ChildUnit"))
                                },
                                LocalName = reader.GetString(reader.GetOrdinal("ChildLocalName")),
                                GlobalName = reader.GetString(reader.GetOrdinal("ChildGlobalName")),
                                MadeOf = reader.GetString(reader.GetOrdinal("ChildMadeOf"))
                            };
                        r.Parent = new Item()
                        {
                            ItemId = reader.GetInt32(reader.GetOrdinal("ChildItemId")),
                            Unit = new Unit()
                            {
                                Name = reader.GetString(reader.GetOrdinal("ParentUnit"))
                            },
                            LocalName = reader.GetString(reader.GetOrdinal("ParentLocalName")),
                            GlobalName = reader.GetString(reader.GetOrdinal("ParentGlobalName")),
                            MadeOf = reader.GetString(reader.GetOrdinal("ParentMadeOf"))
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

        public List<ItemBind> GetAll()
        {
            var conn = Connection;
            List<ItemBind> ItemBinds = new List<ItemBind>();
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemBind_All_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ItemBind r = new ItemBind()
                        {
                            ItemBindId = reader.GetInt32(reader.GetOrdinal("ItemBindId")),
                            ChildAmount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ChildAmount"))),
                            Child = new Item()
                            {
                                Unit = new Unit()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("ChildUnit"))
                                },
                                LocalName = reader.GetString(reader.GetOrdinal("ChildLocalName")),
                                GlobalName = reader.GetString(reader.GetOrdinal("ChildGlobalName")),
                                MadeOf = reader.GetString(reader.GetOrdinal("ChildMadeOf"))
                            },
                            Parent = new Item()
                            {
                                Unit = new Unit()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("ParentUnit"))
                                },
                                LocalName = reader.GetString(reader.GetOrdinal("ParentLocalName")),
                                GlobalName = reader.GetString(reader.GetOrdinal("ParentGlobalName")),
                                MadeOf = reader.GetString(reader.GetOrdinal("ParentMadeOf"))
                            },
                        };
                        
                        ItemBinds.Add(r);
                    }
                }

                return ItemBinds;
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
        
        public void Create(ItemBind ItemBind)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_ItemBind_Add", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ChildAmount", ItemBind.ChildAmount);
                command.Parameters.AddWithValue("@ChildId", ItemBind.Child.ItemId);
                command.Parameters.AddWithValue("@ParentId", ItemBind.Parent.ItemId);

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