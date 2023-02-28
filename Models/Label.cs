using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dotnet_Assignment.Models
{
    public class Label
    {
        public Label(){
            this.Issues=new HashSet<Issue>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Issue> Issues {get; set;}
    
    }
}