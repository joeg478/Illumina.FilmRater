using Illumina.CotentRater.Dal.Models;
using System.Collections.Generic;

namespace Illumina.CotentRater.Dal
{
    public interface IRepository<T> where T : class, IContent
    {
        IEnumerable<T> Get();

        T Save(T content);

        void Update(T content);
    }
}