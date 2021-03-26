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
    public class UnitManager : DbConnection
    {
        public void Delete(int UnitId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Unit_Delete", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UnitId", UnitId);

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

        public void Update(int UnitId, Unit Unit)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Unit_Update", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UnitId", UnitId);
                command.Parameters.AddWithValue("@Name", Unit.Name);
                command.Parameters.AddWithValue("@ShortName", Unit.ShortName);

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

        public Unit GetOne(int UnitId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Unit_One_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UnitId", UnitId);
                conn.Open();
                Unit r = new Unit();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        r.UnitId = reader.GetInt32(reader.GetOrdinal("UnitId"));
                        r.Name = reader.GetString(reader.GetOrdinal("Name"));
                        r.ShortName = reader.GetString(reader.GetOrdinal("ShortName"));
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

        public List<Unit> GetAll()
        {
            var conn = Connection;
            List<Unit> Units = new List<Unit>();
            try
            {
                SqlCommand command = new SqlCommand("SP_Unit_All_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Unit r = new Unit()
                        {
                            UnitId = reader.GetInt32(reader.GetOrdinal("UnitId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ShortName = reader.GetString(reader.GetOrdinal("ShortName"))
                        };
                        Units.Add(r);
                    }
                }

                return Units;
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
        
        public void Create(Unit Unit)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Unit_Add", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", Unit.Name);
                command.Parameters.AddWithValue("@ShortName", Unit.ShortName);

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