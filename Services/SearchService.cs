using System;
using System.Collections.ObjectModel;
using System.Data.OleDb;

namespace RaycastForWindows.Services
{
    public class SearchService
    {
        private const string ConnectionString = "Provider=Search.CollatorDSO;Extended Properties='Application=Windows';";

        public ObservableCollection<string> PerformSearch(string searchTerm)
        {
            var results = new ObservableCollection<string>();

            string queryText = $"SELECT System.ItemNameDisplay FROM SystemIndex WHERE CONTAINS('{searchTerm}')";

            try
            {
                using (var connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = new OleDbCommand(queryText, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(reader["System.ItemNameDisplay"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error or handle it appropriately
                results.Add($"Error: {ex.Message}");
            }

            return results;
        }
    }
}
