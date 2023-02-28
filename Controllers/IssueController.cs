using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Assignment.Models;
using Dotnet_Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
namespace Dotnet_Assignment.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class IssueController : ControllerBase
    {
            IIssueService _issueService;
            IUserService _userService;
            ILabelService _labelService;
      public IssueController(IIssueService service,IUserService userService,ILabelService labelService) {
        _issueService = service;
        _userService=userService;
        _labelService=labelService;
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("issues")]
    //[Authorize(Roles="admin")]
    public IActionResult GetAllIssues() {
        try {
            var issues = _issueService.GetIssuesList();
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception) {
            return BadRequest();
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("[action]/id")]
    public IActionResult GetIssuesById(int id) {
        try {
            var issue = _issueService.GetIssueDetailsById(id);
            if (issue == null) return NotFound();
            var labels = _labelService.GetLabelsByIssueId(id);
            issue.Labels = labels;
            return Ok(issue);
        } catch (Exception) {
            return BadRequest();
        }
    }
    

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("[action]")]
    public IActionResult CreateIssues(IssueReq issueModel) {
        try {
            var model = _issueService.CreateIssue(issueModel);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("[action]")]
    public IActionResult UpdateIssue(int id,IssueReq issueModel) {
        try {
            var model = _issueService.UpdateIssue(id,issueModel);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    /// <summary>
    /// delete employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("[action]")]
    public IActionResult DeleteIssue(int id) {
        try {
            var model = _issueService.DeleteIssue(id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("projects/{projectId}/issues")]
public IActionResult GetIssuesByProjectId(int projectId)
{
    var issues = _issueService.GetIssuesByProjectId(projectId);
    return Ok(issues);
}


[Authorize(Roles = "Admin")]
[HttpGet("{projectId}/issues/{issueId}")]
public ActionResult<Issue> GetIssueByIdAndProjectId(int projectId, int issueId)
{
    Issue issue = _issueService.GetIssueByIdAndProjectId(issueId, projectId);

    if (issue == null)
    {
        return NotFound();
    }

    return issue;
}


[Authorize(Roles = "Admin")]
[HttpPut("{projectId}/issues/{issueId}")]
public IActionResult UpdateIssue(int projectId, int issueId, [FromBody] Issue updatedIssue)
{
    Issue issue = _issueService.GetIssueByIdAndProjectId(issueId, projectId);

    if (issue == null)
    {
        return NotFound();
    }

    issue.Title = updatedIssue.Title;
    issue.Description = updatedIssue.Description;
    issue.Type = updatedIssue.Type;
    issue.AssigneeId = updatedIssue.AssigneeId;
    issue.Status = updatedIssue.Status;

    _issueService.UpdateIssue(issue);
    _issueService.Save();

    return NoContent();
}


[Authorize(Roles = "Admin")]
[HttpDelete("{projectId}/issues/{issueId}")]
public IActionResult DeleteIssue(int projectId, int issueId)
{
    Issue issue = _issueService.GetIssueByIdAndProjectId(issueId, projectId);

    if (issue == null)
    {
        return NotFound();
    }

    _issueService.DeleteIssue(issue);
    _issueService.Save();

    return NoContent();
}


[Authorize(Roles = "Admin")]
[HttpPatch("{projectId}/issues/{issueId}/assignee")]
public IActionResult AssignIssueToUser(int projectId, int issueId, int userId)
{
    Issue issue = _issueService.GetIssueByIdAndProjectId(issueId, projectId);
    User user=_userService.GetUserDetailsById(userId);
    if (issue == null || user==null)
    {
        return NotFound();
    }
    if(issue.ReporterId != userId){
    issue.AssigneeId = userId;
    }else{
        return null;
    }
    _issueService.UpdateIssue(issue);
    _issueService.Save();

    return Ok(issue);
}


[Authorize(Roles = "Admin")]
[HttpPatch("{projectId}/issues/{issueId}/status")]
public IActionResult UpdateIssueStatus(int projectId, int issueId, [FromBody] string newStatus)
{
    Issue issue = _issueService.GetIssueByIdAndProjectId(issueId, projectId);

    if (issue == null)
    {
        return NotFound();
    }

    bool isValidTransition = IssueStatusHelper.IsValidTransition(issue.Status, newStatus);
    if (!isValidTransition)
    {
        return BadRequest("Invalid status transition.");
    }

    issue.Status = newStatus;
    _issueService.UpdateIssue(issue);
    _issueService.Save();

    return Ok(issue);
}



[Authorize(Roles = "Admin")]
[HttpGet]
    [Route("search")]
    public IActionResult Search(string query)
    {
        var result = _issueService.Search(query);
        return Ok(result);
    }



[Authorize(Roles = "Admin")]
 [HttpGet("api/issues/{issueId}/labels")]
public IActionResult GetLabelsByIssueId(int issueId)
{
    try
    {
        var labels = _labelService.GetLabelsByIssueId(issueId);
        return Ok(labels);
    }
    catch (ArgumentException ex)
    {
        return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
}

[Authorize(Roles = "Admin")]
[HttpDelete("{issueId}/labels/{labelId}")]
public IActionResult RemoveLabelFromIssue(int issueId, int labelId)
{
    var issue = _issueService.GetIssueDetailsById(issueId);

    if (issue == null)
    {
        return NotFound();
    }

    var label = _labelService.GetLabelById(labelId);

    if (label == null)
    {
        return NotFound();
    }

    if (!issue.Labels.Contains(label))
    {
        return BadRequest("Issue does not have specified label.");
    }

    issue.Labels.Remove(label);
    _issueService.UpdateIssueAfterRemovingLabel(issue);

    return Ok();
}

[Authorize (Roles="Admin")]
[HttpGet("assignee/{assigneeId}")]
    public IActionResult GetIssuesByAssigneeId(int assigneeId)
    {
        var issues = _issueService.GetIssuesByAssigneeId(assigneeId);
        
        if (issues == null || issues.Count == 0)
            return NotFound("No issues found for the given assignee ID");

        return Ok(issues);
    }
    }
}