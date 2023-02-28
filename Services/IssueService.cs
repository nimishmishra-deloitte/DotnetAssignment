using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Assignment.Services
{
    public class IssueService:IIssueService
    {
       private ProjectContext _context;
    public IssueService(ProjectContext context) {
        _context = context;
    } 

   
    public ResponseModel DeleteIssue(int issueId)
    {
        ResponseModel model = new ResponseModel();
        try {
            Issue _temp = GetIssueDetailsById(issueId);
            if (_temp != null) {
                _context.Remove < Issue > (_temp);
                _context.SaveChanges();
                model.IsSuccess = true;
                model.Messsage = "Issue Deleted Successfully";
            } else {
                model.IsSuccess = false;
                model.Messsage = "Issue Not Found";
            }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public Issue GetIssueDetailsById(int issueId)
    {
        Issue issue;
        try {
            return _context.Issues
            .Include(i => i.Assignee)
            .Include(i => i.Labels)
            .SingleOrDefault(i => i.Id == issueId);
        } catch (Exception) {
            throw;
        }
        return issue;
    }

    public List<Issue> GetIssuesList()
    { 
         List < Issue > issueList;
        try {
            issueList = _context.Set < Issue > ().ToList();
        } catch (Exception) {
            throw;
        }
        return issueList;
    }

    
  

    public ResponseModel CreateIssue(IssueReq issueModel)
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
                Issue issue = new Issue(){
                   Type=issueModel.Type,
                   Title=issueModel.Title,
                   Description=issueModel.Description,
                   ReporterId=issueModel.ReporterId,
                   AssigneeId=issueModel.AssigneeId,
                   Status=issueModel.Status,
                   ProjectId=issueModel.ProjectId,

                };
                _context.Add < Issue > (issue);
                model.Messsage = "Issue Inserted Successfully";
           // }
            _context.SaveChanges();
            model.IsSuccess = true;
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }


    public ResponseModel UpdateIssue(int id,IssueReq issueModel)
    {
        ResponseModel model = new ResponseModel();
        try {
            Issue _temp = GetIssueDetailsById(id);
            if (_temp != null) {
                   _temp.Type=issueModel.Type;
                   _temp.Title=issueModel.Title;
                   _temp.Description=issueModel.Description;
                   _temp.ReporterId=issueModel.ReporterId;
                   _temp.AssigneeId=issueModel.AssigneeId;
                   _temp.Status=issueModel.Status;
                   _temp.ProjectId=issueModel.ProjectId;
                _context.Update < Issue > (_temp);
                model.Messsage = "Issue Update Successfully";
                _context.SaveChanges();
                model.IsSuccess = true;
            } 
           
        } catch (Exception) {
            model.IsSuccess = false;
            model.Messsage = "Issue Not Found";
        }
        return model;
    }

public void UpdateIssueAfterRemovingLabel(Issue issue)
{
    _context.Entry(issue).State = EntityState.Modified;
    _context.SaveChanges();
}
    public List<Issue> GetIssuesByProjectId(int projectId)
{
    return _context.Issues.Where(issue => issue.ProjectId == projectId).ToList();
}
    

    public Issue GetIssueByIdAndProjectId(int issueId, int projectId)
{
    return _context.Issues
        .FirstOrDefault(i => i.Id == issueId && i.ProjectId == projectId);
}
public ResponseModel UpdateIssue(Issue issue)
{
    ResponseModel model = new ResponseModel();
    try {
           _context.Entry(issue).State = EntityState.Modified;
           model.Messsage="Issue Updated Successfully";
           model.IsSuccess=true;
        } catch (Exception) {
            model.IsSuccess=false;
        }
    return model;
}

public void Save()
{
    _context.SaveChanges();
}



public void DeleteIssue(Issue issue)
{
    _context.Issues.Remove(issue);
}


public List<Issue> Search(string query)
    {
        var issues = _context.Issues
            .Where(i => i.Title.Contains(query) || i.Description.Contains(query))
            .Select(i => new Issue
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Type = i.Type,
                Status = i.Status,
                ReporterId = i.ReporterId,
                AssigneeId = i.AssigneeId,
                ProjectId = i.ProjectId
            })
            .ToList();

        return issues;
    }
    public List<Issue> GetIssuesByAssigneeId(int assigneeId)
    {
        return _context.Issues
                       .Include(issue => issue.Labels)
                       .Where(issue => issue.AssigneeId == assigneeId)
                       .ToList();
    }
    
    }
}