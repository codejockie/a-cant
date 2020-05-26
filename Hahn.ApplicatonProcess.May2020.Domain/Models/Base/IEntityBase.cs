using System;
namespace Hahn.ApplicatonProcess.May2020.Domain.Models.Base
{
    public interface IEntityBase<TId>
    {
        TId ID { get; }
    }
}
