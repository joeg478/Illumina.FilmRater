namespace Illumina.CotentRater.Dal.Models
{
    public interface IVideoContent : IContent
    {
        string Director { get; set; }
    }
}