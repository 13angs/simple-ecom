using SeBackend.Common.DTOs;

namespace product_sv.Interfaces
{
    public interface IProductCommentDataCollector
    {
        public ReportCommentModel? ToModel(string message);
        public bool Create(string message);
    }
}