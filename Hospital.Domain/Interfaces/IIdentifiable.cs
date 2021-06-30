using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.Interfaces
{
    public interface IIdentifiable<ID>
    {
        ID GetId();
    }
}
