using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank
{
    public interface IDataBaseConnection<T>
    {

        void OpenConnection();

        void CloseConnection();

        T GetConnection();

    }
}
