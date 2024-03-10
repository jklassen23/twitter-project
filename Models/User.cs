using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace backend.Models;

public class User 
{
    [JsonIgnore]
    public int UserId { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? Bio { get; set; }
    [Required]
    public string? Country { get; set; }
    [JsonIgnore]
    public IEnumerable<Post>? Posts { get; set;}
}