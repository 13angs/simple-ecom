using AutoMapper;
using SeBackend.Common.DTOs;
using SeBackend.Common.Models;

namespace comment_sv.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, ReportCommentModel>()
                .ReverseMap();
        }
    }
}