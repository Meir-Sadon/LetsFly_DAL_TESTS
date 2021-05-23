using LetsFly_DAL.Objects.Poco_s;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    internal class MaintenanceDAOMSSQL : IMaintenanceDAO
    {
        // Add New Log Row To Log Table.
        public void WriteToLog(Log logRow)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand($"Insert Into Log(CreateDateTime, Categories, MethodName, Url, Request, IsSucceed, Response, MethodDuration) Values(" +
                        $"'{DateTime.Now}'," +
                        $" '{logRow.Categories}'," +
                        $" '{logRow.MethodName}'," +
                        $" '{logRow.Url.Replace("'", "")}'," +
                        $" '{logRow.Request.Replace("'", "")}'," +
                        $" '{logRow.IsSucceed}'," +
                        $" '{logRow.Response.Replace("'", "")}'," +
                        $"  {logRow.MethodDuration})", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand($"Insert Into Log(CreateDateTime, Categories, MethodName, Url, Request, IsSucceed, Response, MethodDuration) Values(" +
                            $"'{DateTime.Now}'," +
                            $" '-1'," +
                            $" 'InsertToLog'," +
                            $" '-1'," +
                            $" '{ex.Message.Replace("'", "")}'," +
                            $" '-1'," +
                            $" 'Failed To Insert New Log Row'," +
                            $"0)", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        // Move All Old Tickets And Flights To History.
        public void UpdateTicketsAndFlightsHistory()
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"RemovePastTicketsAndFlights", conn))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                }
            }
        }
    }
}
