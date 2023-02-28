using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet_Assignment.Models
{
    public class Project
    {
    [Key]
    public int Id { get; set; }
    public string Description { get; set; }
    public int CreatorId { get; set; }
    public List<Issue> Issues { get; set; }

    public Project()
    {
        Issues = new List<Issue>();
    }
    }
}