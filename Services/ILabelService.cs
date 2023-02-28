using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Assignment.Models;

namespace Dotnet_Assignment.Services
{
    public interface ILabelService
    {
        List<Label> GetLabels();
        Label GetLabelById(int id);
         bool AddLabelToIssue(int issueId, int labelId);
        Label UpdateLabel(int id, LabelDto label);
        bool DeleteLabel(int id);
        ResponseModel AddLabel(LabelDto labelDto);
        List<Label> GetLabelsByIssueId(int issueId);
    }
}