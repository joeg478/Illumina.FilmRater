using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Illumina.CotentRater.Dal.Models
{
    public class Film : IVideoContent
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Director { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Rating { get; set; }
        
        public Film()
        {
            Id = Guid.Empty;
        }
    }
}
