namespace FoodApp.ModuleDB
{
    public class Connection
    {
        private string connectionString = String.Empty;
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        public Connection(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
