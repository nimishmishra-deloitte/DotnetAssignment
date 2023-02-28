using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Assignment.Models;
using Dotnet_Assignment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dotnet_Assignment.Controllers
{
    [ApiController]
[Route("api/labels")]
public class LabelsController : ControllerBase
{
    private readonly ILabelService _labelService;

    public LabelsController(ILabelService labelService)
    {
        _labelService = labelService;
    }


    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult GetLabels()
    {
        var labels = _labelService.GetLabels();
        return Ok(labels);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public IActionResult GetLabelById(int id)
    {
        var label = _labelService.GetLabelById(id);
        if (label == null)
        {
            return NotFound();
        }
        return Ok(label);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateIssues(LabelDto label) {
        try {
            var model = _labelService.AddLabel(label);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult UpdateLabel(int id, [FromBody] LabelDto label)
    {
        var labels = _labelService.GetLabelById(id);
        if (labels == null)
        {
            return NotFound();
        }
        _labelService.UpdateLabel(id, label);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult DeleteLabel(int id)
    {
        var label =  _labelService.GetLabelById(id);
        if (label == null)
        {
            return NotFound();
        }
        _labelService.DeleteLabel(id);
        return NoContent();
    }
   

   [Authorize(Roles = "Admin")]
   [HttpPost("api/issues/{issueId}/labels/{labelId}")]
public IActionResult AddLabelToIssue(int issueId, int labelId)
{
    var result =_labelService.AddLabelToIssue(issueId, labelId);

    if (!result) return NotFound();

    return Ok();
}

}
}