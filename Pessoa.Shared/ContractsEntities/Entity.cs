using System;

namespace Pessoa.Shared.ContractsEntities
{
    public abstract class Entity
    {
        public string Id { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();   
        }
    }
}