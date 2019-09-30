namespace ControleDeEstoqueUP.Data {
    internal class SingletonContext {
        private SingletonContext() { }
        private static ApplicationDbContext database;

        public static ApplicationDbContext GetInstance() {
            if (database == null) {
                database = new ApplicationDbContext();
            }
            return database;
        }
    }
}
