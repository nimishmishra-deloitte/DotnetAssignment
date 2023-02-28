using System;
using Microsoft.AspNetCore.Mvc;
using Dotnet_Assignment.Services;
using Dotnet_Assignment.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Dotnet_Assignment.Controllers
{
    [ApiController]
    [Route ("[controller]")]
    public class ProjectController:ControllerBase
    {
     IProjectService _projectService;
     IIssueService _issueService;
      public ProjectController(IProjectService service,IIssueService issueService) {
        _projectService = service;
        _issueService=issueService;
    }


    [HttpGet("project")]
    [Authorize(Roles="Admin")]
    public IActionResult GetAllProjects() {
        try {
            var projects = _projectService.GetProjectsList();
            if (projects == null) return NotFound();
            return Ok(projects);
        } catch (Exception) {
            return BadRequest();
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("[action]/id")]
    public IActionResult GetProjectsById(int id) {
        try {
            var project = _projectService.GetProjectDetailsById(id);
            if (project == null) return NotFound();
            List<Issue> issues = _issueService.GetIssuesByProjectId(id);
            project.Issues=issues;
            return Ok(project);
        } catch (Exception) {
            return BadRequest();
        }
    }
    
    [Authorize(Roles = "ProjectManager")]
    [HttpPost]
    [Route("[action]")]
    public IActionResult CreateProjects(ProjectReq projectModel) {
        try {
            var model = _projectService.CreateProject(projectModel);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("[action]")]
    public IActionResult UpdateProject(int id,ProjectReq projectModel) {
        try {
            var model = _projectService.UpdateProject(id,projectModel);
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
    public IActionResult DeleteProject(int id) {
        try {
            var model = _projectService.DeleteProject(id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("[action]")]
    public IActionResult AddIssueToProject(int projectId, IssueReq issue)
    {
        var model=_projectService.AddIssueToProject(projectId, issue);
        return Ok(model);
    }

    }
}