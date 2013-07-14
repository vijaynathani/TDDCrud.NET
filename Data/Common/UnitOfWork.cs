using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace Data.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConcurrentDictionary<int, Entities2> _pool;

        public UnitOfWork()
        {
            _pool = new ConcurrentDictionary<int, Entities2>();
        }

        private int Key { get { return Thread.CurrentThread.ManagedThreadId; } }

        public Entities2 Current
        {
            get
            {
                Entities2 r;
                _pool.TryGetValue(Key, out r);
                return r;
            }
        }

        public void Perform(Action execute)
        {
            using (var ctx = new Entities2())
            {
                AddToPool(ctx);
                try
                {
                    execute();
                    ctx.SaveChanges(); //No commit if Exception.
                }
                finally
                {
                    RemoveFromPool();
                }
            }
        }

        private void RemoveFromPool()
        {
            Entities2 old;
            _pool.TryRemove(Key, out old);
        }

        private void AddToPool(Entities2 ctx)
        {
            _pool.TryAdd(Key, ctx);
        }
    }
}
