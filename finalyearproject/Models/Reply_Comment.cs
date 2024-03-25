using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class Reply_Comment
    {
        public int reply_id { get; set; }
        public int user_id {  get; set; }
        [ForeignKey("user_id")]
        public User user { get; set; }
        public string reply_content {  get; set; }
        public int comment_id {  get; set; }
        [ForeignKey("comment_id")]
        public Comment Comment { get; set; }
        public DateTime date_reply {  get; set; } 
    }
}
