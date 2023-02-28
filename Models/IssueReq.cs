using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Assignment.Enums;

namespace Dotnet_Assignment.Models
{
    public class IssueReq
    {
        public string Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ReporterId { get; set; }
    public int AssigneeId { get; set; }
    public string Status { get; set; }
    public int ProjectId {get; set;}
    }
}