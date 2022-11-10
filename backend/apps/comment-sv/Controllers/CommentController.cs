using Microsoft.AspNetCore.Mvc;
using SeBackend.Common.Models;

namespace comment_sv.Controllers
{
    [ApiController]
    [Route("api/comment/comments")]
    public class CommentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Comment>> Get()
        {
            IList<Comment> comments = new List<Comment>(){
                new Comment{
                    CommentId=1,
                    ProductId=1,
                    Text="First comment",
                    NLikes=10
                }
            };
            return Ok(comments);
        }
    }
}