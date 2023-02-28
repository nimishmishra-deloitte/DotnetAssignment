using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Assignment.Models;
namespace Dotnet_Assignment.Services
{
    public interface IProjectService
    {
       List<Project> GetProjectsList();
     

        
        Project GetProjectDetailsById(int projectId);

       
        ResponseModel CreateProject(ProjectReq projectModel);

         ResponseModel UpdateProject(int id,ProjectReq projectModel);
       
        ResponseModel DeleteProject(int projectId);

        ResponseModel AddIssueToProject(int projectId, IssueReq issue);
    }
}