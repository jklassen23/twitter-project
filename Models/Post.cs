using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.VisualBasic;

namespace backend.Models;

public class Post 
{
    public int PostId { get; set; }
    public string? Username { get; set; }
    [Required]
    public string? Content { get; set; }
    public DateTime? DateTime { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    [JsonIgnore]
    public int? UserId { get; set; }
}