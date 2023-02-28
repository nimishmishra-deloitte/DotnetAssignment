using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Assignment.Services
{
    public class LabelService : ILabelService
    {
    private readonly ProjectContext _context;

    public LabelService(ProjectContext context)
    {
        _context = context;
    }

    public List<Label> GetLabels()
    {
        return _context.Labels.ToList();
    }

    public Label GetLabelById(int id)
    {
        return _context.Labels.Find(id);
    }

  
    public ResponseModel AddLabel(LabelDto labelDto)
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
                Label prj = new Label(){
                    Name = labelDto.Name,  
                };
                _context.Add < Label > (prj);
                model.Messsage = "Label Inserted Successfully";
           // }
            _context.SaveChanges();
            model.IsSuccess = true;
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    
    public Label UpdateLabel(int id, LabelDto label)
    {
        var existingLabel = _context.Labels.Find(id);

        if (existingLabel == null)
        {
            return null;
        }

        existingLabel.Name = label.Name;

        _context.SaveChanges();

        return existingLabel;
    }

    public bool DeleteLabel(int id)
    {
        var existingLabel = _context.Labels.Find(id);

        if (existingLabel == null)
        {
            return false;
        }

        _context.Labels.Remove(existingLabel);
        _context.SaveChanges();

        return true;
    
}

public bool AddLabelToIssue(int issueId, int labelId)
{
    var issue = _context.Issues.Find(issueId);
    if (issue == null) return false;

    var label = _context.Labels.Find(labelId);
    if (label == null) return false;

    issue.Labels.Add(label);
     _context.SaveChanges();

    return true;
}
public List<Label> GetLabelsByIssueId(int issueId)
{
    var issue = _context.Issues.Include(i => i.Labels).FirstOrDefault(i => i.Id == issueId);

    if (issue == null)
    {
        throw new ArgumentException($"Issue with ID {issueId} not found.");
    }

    return issue.Labels.ToList();
}
 
    }
}