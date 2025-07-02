using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DataBase
{
    internal class DBConn
    {
        public static OnlineStoreEntities connection = new OnlineStoreEntities();
    }
}
