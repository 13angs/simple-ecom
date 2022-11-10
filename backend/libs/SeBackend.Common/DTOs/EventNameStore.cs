namespace SeBackend.Common.DTOs
{
    public static class EventNameStore
    {
        public static CommentEvent Comment = new CommentEvent
        {
            Create="create_comment",
            Update="update_comment",
            Remove="remove_comment"
        };
    }

    public class CommentEvent
    {
        public string? Create { get; set; }
        public string? Update { get; set; }
        public string? Remove { get; set; }
    }
}