using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Assignment.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dotnet_Assignment.Models
{
    public class Issue
    {
    [Key]
    public int Id { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ReporterId { get; set; }
    public int? AssigneeId { get; set; }
    public virtual User Assignee {get;set;}
    public string Status { get; set; }
    public int ProjectId { get; set; }
    [JsonIgnore]
    public ICollection<Label> Labels { get; set; }
    public Issue()
    {
        Labels = new HashSet<Label>();
    }
    
    }
}