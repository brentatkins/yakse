using System;

namespace Yakse.Core
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; private set; }
    }
}