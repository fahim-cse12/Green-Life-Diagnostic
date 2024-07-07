using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class IdNotFoundException<T> : NotFoundException
    {
        public IdNotFoundException(Guid id) : base($"This {typeof(T).Name} with id: {id} doesn't exist in the database")
        { }
    }
}
