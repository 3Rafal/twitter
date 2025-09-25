using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace TwitterClone.Api.Models;

public class Post
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid AuthorId { get; set; }

    [Required]
    [StringLength(280)]
    public string Content { get; set; } = string.Empty;

    public Guid? ReplyToPostId { get; set; }

    [Column(TypeName = "jsonb")]
    public string MediaUrls { get; set; } = "[]";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(AuthorId))]
    public virtual User Author { get; set; } = null!;

    [ForeignKey(nameof(ReplyToPostId))]
    public virtual Post? ReplyToPost { get; set; }

    public virtual ICollection<Post> Replies { get; set; } = new List<Post>();
    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public List<string> GetMediaUrls()
    {
        return string.IsNullOrEmpty(MediaUrls)
            ? new List<string>()
            : JsonSerializer.Deserialize<List<string>>(MediaUrls) ?? new List<string>();
    }

    public void SetMediaUrls(List<string> urls)
    {
        MediaUrls = JsonSerializer.Serialize(urls);
    }
}