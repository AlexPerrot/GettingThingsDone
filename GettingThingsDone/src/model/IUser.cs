using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model
{
    public interface IUser
    {
        String Username { get; set; }
        String Password { get; set; }
        String Mail { get; set; }
    }
}
