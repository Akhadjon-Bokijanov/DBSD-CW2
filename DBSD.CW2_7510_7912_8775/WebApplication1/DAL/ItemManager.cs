using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBSD_CW2_7510_8775_7912.Models;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DBSD_CW2_7510_8775_7912.DAL
{
    public class ItemManager : DbConnection
    {

        public void Delete(int ItemId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Item_Delete", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ItemId", ItemId);

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

        public Item GetOne(int id) {
            var conn = Connection;
            Item i = new Item();
            try
            {
                SqlCommand command = new SqlCommand("SP_Item_Select_One", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemId", id);

                conn.Open();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        i.ItemId = reader.GetInt32(reader.GetOrdinal("ItemId"));
                        i.LocalName = reader.GetString(reader.GetOrdinal("LocalName"));
                        i.GlobalName = reader.GetString(reader.GetOrdinal("GlobalName"));
                        i.ItemUID = reader.GetInt32(reader.GetOrdinal("ItemUID"));
                        i.UsageStartedAt = reader.IsDBNull(reader.GetOrdinal("UsageStartedAt"))
                                            ? (DateTime?)null
                                            : reader.GetDateTime(reader.GetOrdinal("UsageStartedAt"));
                        i.IsEchangeble = reader.GetBoolean(reader.GetOrdinal("IsEchangeble"));
                        i.MadeOf = reader.IsDBNull(reader.GetOrdinal("MadeOf"))
                                    ? "N/A"
                                    : reader.GetString(reader.GetOrdinal("MadeOf"));
                        i.TransactionCount = reader.GetInt32(reader.GetOrdinal("TransactionCount"));
                        i.NumParent = reader.GetInt32(reader.GetOrdinal("NumParent"));
                        i.AvgAmountUsagePerParent = reader.IsDBNull(reader.GetOrdinal("AvgAmountUsagePerParent"))
                                                    ? 0
                                                    : reader.GetDecimal(reader.GetOrdinal("AvgAmountUsagePerParent"));
                        i.Image = reader.IsDBNull(reader.GetOrdinal("Image"))
                            ? null
                            : (byte[])reader["Image"];
                        i.Unit = new Unit()
                        {
                            Name = reader.GetString(reader.GetOrdinal("UnitName"))
                        };
                        i.Store = new Store()
                        {
                            Name = reader.GetString(reader.GetOrdinal("StoreName"))
                        };
                        i.Supplier = new Supplier()
                        {
                            Name = reader.GetString(reader.GetOrdinal("SupplierName"))
                        };

                    }
                }

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

            return i;
        }


        public List<Item> GetAll(ItemFilter filter=null)
        {
            List<Item> items = new List<Item>();
            var conn = Connection;

            try
            {
                SqlCommand command = new SqlCommand("SP_ITEM_ALL_SELECT", conn);
                command.CommandType = CommandType.StoredProcedure;

                if (!String.IsNullOrEmpty(filter.GlobalName))
                {
                    command.Parameters.AddWithValue("@GlobalName", filter.GlobalName);
                }

                if (!String.IsNullOrEmpty(filter.LocalName))
                {
                    command.Parameters.AddWithValue("@LocalName", filter.LocalName);
                }
                
                if (!String.IsNullOrEmpty(filter.SupplierName))
                {
                    command.Parameters.AddWithValue("@SupplierName", filter.SupplierName);
                }
                if (!String.IsNullOrEmpty(filter.StoreName))
                {
                    command.Parameters.AddWithValue("@StoreName", filter.StoreName);
                }
                if (!String.IsNullOrEmpty(filter.MadeOf))
                {
                    command.Parameters.AddWithValue("@MadeOf", filter.MadeOf);
                }
                if (filter.ItemUID > 0)
                {
                    command.Parameters.AddWithValue("@ItemUID", filter.ItemUID);
                }
                if (!String.IsNullOrEmpty(filter.SortCase))
                {
                    command.Parameters.AddWithValue("@SortCase", filter.SortCase);
                }
                if(filter.SortIndex>=1 && filter.SortIndex <= 5)
                {
                    command.Parameters.AddWithValue("@SortIndex", filter.SortIndex);
                }
                if(filter.PageSize > 0 && filter.PageNumber > 0)
                {
                    command.Parameters.AddWithValue("@PageSize", filter.PageSize);
                    command.Parameters.AddWithValue("@PageNumber", filter.PageNumber-1);
                }
                
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var i = new Item()
                        {
                            ItemId =reader.GetInt32(reader.GetOrdinal("ItemId")),
                            LocalName = reader.GetString(reader.GetOrdinal("LocalName")),
                            GlobalName = reader.GetString(reader.GetOrdinal("GlobalName")),
                            ItemUID = reader.GetInt32(reader.GetOrdinal("ItemUID")),
                            UsageStartedAt = reader.IsDBNull(reader.GetOrdinal("UsageStartedAt"))
                                                ? (DateTime?)null
                                                : reader.GetDateTime(reader.GetOrdinal("UsageStartedAt")),
                            IsEchangeble = reader.GetBoolean(reader.GetOrdinal("IsEchangeble")),
                            MadeOf = reader.IsDBNull(reader.GetOrdinal("MadeOf"))
                                        ? "N/A"
                                        : reader.GetString(reader.GetOrdinal("MadeOf")),
                            TransactionCount = reader.GetInt32(reader.GetOrdinal("TransactionCount")),
                            NumParent = reader.GetInt32(reader.GetOrdinal("NumParent")),
                            AvgAmountUsagePerParent = reader.IsDBNull(reader.GetOrdinal("AvgAmountUsagePerParent"))
                                                        ? 0
                                                        : reader.GetDecimal(reader.GetOrdinal("AvgAmountUsagePerParent")),
                            Unit = new Unit()
                            {
                                Name = reader.GetString(reader.GetOrdinal("UnitName"))
                            },
                            Store = new Store()
                            {
                                Name = reader.GetString(reader.GetOrdinal("StoreName"))
                            },
                            Supplier = new Supplier()
                            {
                                Name = reader.GetString(reader.GetOrdinal("SupplierName"))
                            }

                        };

                    items.Add(i);
                    }
                }

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


            return items;
        }

        public void Create(Item item)
        {
            var conn = Connection;
            try
            {
                
                SqlCommand command = new SqlCommand("SP_Item_Add", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@GlobalName", item.GlobalName);
                command.Parameters.AddWithValue("@LocalName", item.LocalName);
                command.Parameters.AddWithValue("@UnitId", item.Unit.UnitId);
                command.Parameters.AddWithValue("@StoreId", item.Store.StoreId);
                command.Parameters.AddWithValue("@MadeOf", item.MadeOf);
                command.Parameters.AddWithValue("@SupplierId", item.Supplier.SupplierId);
                command.Parameters.AddWithValue("@UsageStartedAt", (object)item.UsageStartedAt ?? DBNull.Value);
                command.Parameters.AddWithValue("@Image", (object)item.Image ?? SqlBinary.Null);
                command.Parameters.AddWithValue("@IsEchangeble", item.IsEchangeble);
                command.Parameters.AddWithValue("@ItemUID", item.ItemUID);

                conn.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

        }

        public void Update(int ItemId, Item item)
        {
            var conn = Connection;
            try
            {

                SqlCommand command = new SqlCommand("SP_Item_Update", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ItemId", ItemId);
                command.Parameters.AddWithValue("@GlobalName", item.GlobalName);
                command.Parameters.AddWithValue("@LocalName", item.LocalName);
                command.Parameters.AddWithValue("@UnitId", item.Unit.UnitId);
                command.Parameters.AddWithValue("@StoreId", item.Store.StoreId);
                command.Parameters.AddWithValue("@MadeOf", item.MadeOf);
                command.Parameters.AddWithValue("@SupplierId", item.Supplier.SupplierId);
                command.Parameters.AddWithValue("@UsageStartedAt", (object)item.UsageStartedAt ?? DBNull.Value);
                command.Parameters.AddWithValue("@Image", (object)item.Image ?? SqlBinary.Null);
                command.Parameters.AddWithValue("@IsEchangeble", item.IsEchangeble);
                command.Parameters.AddWithValue("@ItemUID", item.ItemUID);

                conn.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
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