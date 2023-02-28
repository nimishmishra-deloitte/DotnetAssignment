using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Assignment.Models;
namespace Dotnet_Assignment.Services
{
    public interface IIssueService
    {
       List<Issue> GetIssuesList();
     
        List<Issue> GetIssuesByProjectId(int id);
        
        Issue GetIssueDetailsById(int issueId);

       
        ResponseModel CreateIssue(IssueReq issueModel);

         ResponseModel UpdateIssue(int id,IssueReq issueModel);
        ResponseModel DeleteIssue(int issueId);
        void DeleteIssue(Issue issue);
        Issue GetIssueByIdAndProjectId(int issueId, int projectId);
        ResponseModel UpdateIssue(Issue issue);
        
        List<Issue> Search(string query);

        void UpdateIssueAfterRemovingLabel(Issue issue);
        List<Issue> GetIssuesByAssigneeId(int assigneeId);
        void Save();
    }
}