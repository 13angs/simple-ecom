using AutoMapper;
using comment_sv.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using SeBackend.Common.DTOs;
using SeBackend.Common.Interfaces;
using SeBackend.Common.Models;
using SeBackend.Common.Services;

namespace comment_sv.Services
{
  public class CommentService : ControllerBase, ICommentService
  {
    private readonly ILogger<CommentService> logger;
    private readonly IMessagePublisher publisher;
    private readonly IConfiguration configuration;
    private readonly IMapper mapper;

    private readonly IMongoCollection<Comment?> _comment;

    public CommentService(ILogger<CommentService> logger, 
        IMessagePublisher publisher, IConfiguration configuration, IMapper mapper)
    {
      this.logger = logger;
      this.publisher = publisher;
      this.configuration = configuration;
      this.mapper = mapper;
      this.publisher.Connect(
        configuration["RabbitMQ:HostName"],
        "report_se_exchange"
      );
      _comment = MongodbCollectionService.GetCollection<Comment?>(configuration);
    }
    public async Task<ActionResult> Post(Comment comment)
    {
      try
      {
        await _comment.InsertOneAsync(comment);
        logger.LogInformation("--> Comment created!");
        ReportCommentModel commentModel = new ReportCommentModel();
        mapper.Map<Comment, ReportCommentModel>(comment, commentModel);
        commentModel.EventName=EventNameStore.Comment.Create;
        publisher.Publish(JsonConvert.SerializeObject(commentModel), "report.se", null);
        return StatusCode(StatusCodes.Status201Created);
      }
      catch (Exception e)
      {
        logger.LogError(e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }

    }

    public ActionResult<IEnumerable<Comment>> Get()
    {
      IEnumerable<Comment?> comments = _comment.Find(c => true).ToList();
      return Ok(comments);
    }
  }
}