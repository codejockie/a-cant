using System;
namespace Hahn.ApplicatonProcess.May2020.Domain.Entities.Base
{
    public interface IEntityBase<TId>
    {
        TId ID { get; }
    }
}
