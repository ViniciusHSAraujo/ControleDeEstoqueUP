using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoqueUP.Data {
    class SingletonContext {
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
