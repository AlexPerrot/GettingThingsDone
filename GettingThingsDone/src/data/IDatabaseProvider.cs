using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace GettingThingsDone.src.data
{
    public interface IDatabaseProvider
    {
        DataClassesDataContext Database { get;}
    }
}
