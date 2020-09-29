namespace CaseEleva.Models
{
    public class DBFactory
    {
        private static DBFactory _instance;

        private DBFactory() { }

        public static DBFactory GetInstance()
        {
            if (_instance == null)
                _instance = new DBFactory();

            return _instance;
        }

        public CaseElevaEntities GetDb()
        {
            return new CaseElevaEntities();
        }

    }
}