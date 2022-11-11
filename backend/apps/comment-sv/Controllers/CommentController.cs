using comment_sv.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SeBackend.Common.Models;

namespace comment_sv.Controllers
{
    [ApiController]
    [Route("api/comment/comments")]
    public class CommentController : ControllerBase
    {
    private readonly ICommentService commentService;

    public CommentController(ICommentService commentService)
        {
      this.commentService = commentService;
    }
        [HttpGet]
        public ActionResult<IEnumerable<Comment>> Get()
        {
            return commentService.Get();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Comment comment)
        {
            return await commentService.Post(comment);
        }
    }
}