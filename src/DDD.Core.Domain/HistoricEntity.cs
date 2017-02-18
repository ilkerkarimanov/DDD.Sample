using DDD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Core.Domain
{
    public abstract class HistoricEntity : Entity, IHasHistory
    {

        public DateTime? Created
        {
            get;
            private set;
        }

        public DateTime? Modified
        {
            get;
            private set;
        }

        public void SetAsCreated()
        {
            Created = DateTimeProvider.Now;
            Modified = DateTimeProvider.Now;
        }

        public void SetAsModified()
        {
            Modified = DateTimeProvider.Now;
        }
    }
}
