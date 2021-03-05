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
    public class EmployeeManager : DbConnection
    {
        public void Delete(int EmployeeId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Employee_Delete", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmployeeId", EmployeeId);

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

        public void Update(int EmployeeId, Employee Employee)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Employee_Update", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                command.Parameters.AddWithValue("@FullName", Employee.FullName);
                command.Parameters.AddWithValue("@RoleId", Employee.Role.RoleId);
                command.Parameters.AddWithValue("@DoB", (object)Employee.DoB);
                command.Parameters.AddWithValue("@Address", Employee.Address);
                command.Parameters.AddWithValue("@Password", Employee.Password);
                command.Parameters.AddWithValue("@Salary", Employee.Salary);

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

        public Employee GetOne(int EmployeeId)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Employee_One_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                conn.Open();
                Employee r = new Employee();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        r.EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId"));
                        r.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                        r.Address = reader.GetString(reader.GetOrdinal("Address"));
                        r.Password = reader.GetString(reader.GetOrdinal("Password"));
                        r.DoB = reader.GetDateTime(reader.GetOrdinal("DoB"));
                        r.Salary = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Salary")));
                        r.Role = new Role()
                        {
                            RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                            Name = reader.GetString(reader.GetOrdinal("Role")),
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

        public List<Employee> GetAll()
        {
            var conn = Connection;
            List<Employee> Employees = new List<Employee>();
            try
            {
                SqlCommand command = new SqlCommand("SP_Employee_All_Select", conn);
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee r = new Employee()
                        {
                            EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                            FullName = reader.GetString(reader.GetOrdinal("FullName")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            DoB = reader.GetDateTime(reader.GetOrdinal("DoB")),
                            Salary = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Salary"))),
                            Role = new Role() 
                            { 
                                RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                                Name = reader.GetString(reader.GetOrdinal("Role")),
                            } 
                        };
                        Employees.Add(r);
                    }
                }

                return Employees;
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
        
        public void Create(Employee Employee)
        {
            var conn = Connection;
            try
            {
                SqlCommand command = new SqlCommand("SP_Employee_Add", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FullName", Employee.FullName);
                command.Parameters.AddWithValue("@RoleId", Employee.Role.RoleId);
                command.Parameters.AddWithValue("@DoB", (object)Employee.DoB);
                command.Parameters.AddWithValue("@Address", Employee.Address);
                command.Parameters.AddWithValue("@Password", Employee.Password);
                command.Parameters.AddWithValue("@Salary", Employee.Salary);

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