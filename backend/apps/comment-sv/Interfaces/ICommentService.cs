using Microsoft.AspNetCore.Mvc;
using SeBackend.Common.Models;

namespace comment_sv.Interfaces
{
    public interface ICommentService
    {
        public Task<ActionResult> Post(Comment comment);
        public ActionResult<IEnumerable<Comment>> Get();
    }
}