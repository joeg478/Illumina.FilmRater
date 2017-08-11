using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Illumina.CotentRater.Dal.Models
{
    public interface IContent
    {
        Guid Id { get; set; }

        string Title { get; set; }

        DateTime ReleaseDate { get; set; }

        int Rating { get; set; }
    }
}
