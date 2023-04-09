using System.Collections.Generic;
using System.Linq;

namespace Nezumimeshi.Basis
{
    public static class IdCreator
    {
        static readonly Queue<int> idQueue = new Queue<int>(Enumerable.Range(0, 1000));

        public static int Get()
        {
            return idQueue.Dequeue();
        }

        public static void Return(int id)
        {
            idQueue.Enqueue(id);
        }
    }
}