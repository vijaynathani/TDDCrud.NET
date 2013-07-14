using System;

namespace Data.Common
{
    public interface IUnitOfWork
    {
        Entities2 Current { get; }
        void Perform(Action execute);
    }
}