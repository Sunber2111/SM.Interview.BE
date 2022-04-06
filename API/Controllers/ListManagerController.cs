using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ListManagement.Command;
using Common.StatusAction;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ListManagerController : BaseController
    {
        [HttpPost]
        public async Task<ActionSucessWithData<List<int>>> SortArray(SortElementInList.Command command)
            => await Mediator.Send(command);

    }
}