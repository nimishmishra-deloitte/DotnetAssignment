using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Assignment.Models;
namespace Dotnet_Assignment.Services
{
    public class ProjectService:IProjectService
    {
       private ProjectContext _context;
    public ProjectService(ProjectContext context) {
        _context = context;
    } 

   
    public ResponseModel DeleteProject(int projectId)
    {
        ResponseModel model = new ResponseModel();
        try {
            Project _temp = GetProjectDetailsById(projectId);
            if (_temp != null) {
                _context.Remove < Project > (_temp);
                _context.SaveChanges();
                model.IsSuccess = true;
                model.Messsage = "Project Deleted Successfully";
            } else {
                model.IsSuccess = false;
                model.Messsage = "Project Not Found";
            }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public Project GetProjectDetailsById(int projectId)
    {
        Project prj;
        try {
            prj = _context.Find < Project > (projectId);
        } catch (Exception) {
            throw;
        }
        return prj;
    }

    public List<Project> GetProjectsList()
    { 
         List < Project> proList;
        try {
            proList = _context.Set < Project> ().ToList();
        } catch (Exception) {
            throw;
        }
        return proList;
    }

    
  

    public ResponseModel CreateProject(ProjectReq projectModel)
    {
        ResponseModel model = new ResponseModel();
        try {
            //Employee _temp = GetEmployeeDetailsById(employeeModel.EmployeeId);
            // if (_temp != null) {
            //     _temp.Designation = employeeModel.Designation;
            //     _temp.EmployeeFirstName = employeeModel.EmployeeFirstName;
            //     _temp.EmployeeLastName = employeeModel.EmployeeLastName;
            //     _temp.Salary = employeeModel.Salary;
            //     _context.Update < Employee > (_temp);
            //     model.Messsage = "Employee Update Successfully";
            // } else {
                Project prj = new Project(){
                    Description = projectModel.Description,  
                    CreatorId=projectModel.CreatorId 
                };
                _context.Add < Project > (prj);
                model.Messsage = "Project Inserted Successfully";
           // }
            _context.SaveChanges();
            model.IsSuccess = true;
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }


    public ResponseModel UpdateProject(int id,ProjectReq projectModel)
    {
        ResponseModel model = new ResponseModel();
        try {
            Project _temp = GetProjectDetailsById(id);
            if (_temp != null) {
                _temp.Description = projectModel.Description;
                _temp.CreatorId=projectModel.CreatorId;
                _context.Update < Project > (_temp);
                model.Messsage = "Project Update Successfully";
                _context.SaveChanges();
                model.IsSuccess = true;
            } 
           
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Project Not Found";
        }
        return model;
    }


     public ResponseModel AddIssueToProject(int projectId, IssueReq issue)
    {
        var project = _context.Projects.Find(projectId);
        if (project == null)
        {
            throw new ArgumentException("Invalid project ID");
        }
        ResponseModel model = new ResponseModel();
        try {
            //Employee _temp = GetEmployeeDetailsById(employeeModel.EmployeeId);
            // if (_temp != null) {
            //     _temp.Designation = employeeModel.Designation;
            //     _temp.EmployeeFirstName = employeeModel.EmployeeFirstName;
            //     _temp.EmployeeLastName = employeeModel.EmployeeLastName;
            //     _temp.Salary = employeeModel.Salary;
            //     _context.Update < Employee > (_temp);
            //     model.Messsage = "Employee Update Successfully";
            // } else {
                Issue iss = new Issue(){
                   Type=issue.Type,
                   Title=issue.Title,
                   Description=issue.Description,
                   ReporterId=issue.ReporterId,
                   AssigneeId=issue.AssigneeId,
                   Status=issue.Status,
                   ProjectId=issue.ProjectId,

                };
                project.Issues.Add(iss);
                _context.SaveChanges();
                model.Messsage = "Issue Inserted Successfully";
           // }
           
            model.IsSuccess = true;
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
       return model;
    }

    public void RemoveIssueFromProject(int projectId, int issueId)
    {
        var project = _context.Projects.Find(projectId);
        var issue = project.Issues.FirstOrDefault(i => i.Id == issueId);
        if (issue != null)
        {
            project.Issues.Remove(issue);
            _context.SaveChanges();
        }
    }

    
    }
}