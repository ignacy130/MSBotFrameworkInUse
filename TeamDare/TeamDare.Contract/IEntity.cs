using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamDare.Contract
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
