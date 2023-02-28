using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet_Assignment.Models
{
    public static class IssueStatusHelper
{
    private static readonly Dictionary<string, HashSet<string>> ValidTransitions = new Dictionary<string, HashSet<string>>
    {
        {"Open", new HashSet<string>{"InProgress"}},
        {"InProgress", new HashSet<string>{"InReview", "CodeComplete"}},
        {"InReview", new HashSet<string>{"InProgress", "CodeComplete"}},
        {"CodeComplete", new HashSet<string>{"QATesting"}},
        {"QATesting", new HashSet<string>{"CodeComplete", "Done"}},
        {"Done", new HashSet<string>()}
    };
    
    public static bool IsValidTransition(string currentStatus, string newStatus)
    {
        if (!ValidTransitions.ContainsKey(currentStatus))
        {
            return false;
        }

        return ValidTransitions[currentStatus].Contains(newStatus);
    }
}
}