using API.Base;
using API.Model;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesController : BaseController<Priority, PriorityRepository, int>
    {
        private readonly PriorityRepository priorityRepository;
        public PrioritiesController(PriorityRepository priorityRepository) : base(priorityRepository)
        {
            this.priorityRepository = priorityRepository;
        }
    }
}
