using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone.Api.Models;

public class Follow
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid FollowerId { get; set; }

    [Required]
    public Guid FollowedId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(FollowerId))]
    public virtual User Follower { get; set; } = null!;

    [ForeignKey(nameof(FollowedId))]
    public virtual User Followed { get; set; } = null!;
}