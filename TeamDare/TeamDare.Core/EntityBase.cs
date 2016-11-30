using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamDare.Core
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        public EntityBase()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
