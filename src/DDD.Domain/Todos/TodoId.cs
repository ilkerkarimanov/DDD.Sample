using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Domain.Todos
{
    public class TodoId: DDD.Core.Domain.Identity
    {
        public TodoId()
        {

        }

        public TodoId(string id): base(id)
        {

        }
    }
}
