using AutoMapper;
using comment_sv.Interfaces;
using comment_sv.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SeBackend.Common.DTOs;
using SeBackend.Common.Interfaces;
using SeBackend.Common.Models;

namespace comment_sv.Services
{
  public class CommentService : ControllerBase, ICommentService
  {
    private readonly CommentContext context;
    private readonly ILogger<CommentService> logger;
    private readonly IMessagePublisher publisher;
    private readonly IConfiguration configuration;
    private readonly IMapper mapper;

    public CommentService(CommentContext context, ILogger<CommentService> logger, 
        IMessagePublisher publisher, IConfiguration configuration, IMapper mapper)
    {
      this.context = context;
      this.logger = logger;
      this.publisher = publisher;
      this.configuration = configuration;
      this.mapper = mapper;
      this.publisher.Connect(
        configuration["RabbitMQ:HostName"],
        "report_se_exchange"
      );
    }
    public async Task<ActionResult> Post(Comment comment)
    {
      try
      {
        context.Comments.Add(comment);
        int result = await context.SaveChangesAsync();
        if(result>0)
        {
            logger.LogInformation("--> Comment created!");
            ReportCommentModel commentModel = new ReportCommentModel();
            mapper.Map<Comment, ReportCommentModel>(comment, commentModel);
            commentModel.EventName=EventNameStore.Comment.Create;
            publisher.Publish(JsonConvert.SerializeObject(commentModel), "report.se", null);
            return StatusCode(StatusCodes.Status201Created);
        }
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      catch (Exception e)
      {
        logger.LogError(e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }

    }

    public ActionResult<IEnumerable<Comment>> Get()
    {
      IEnumerable<Comment> comments = context.Comments;
      return Ok(comments);
    }
  }
}