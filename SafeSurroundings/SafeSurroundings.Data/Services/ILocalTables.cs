using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSurroundings.Data.Services
{
    public interface ILocalTables
    {    
        void Update(int id);
        void Delete(int id);
    }
}
