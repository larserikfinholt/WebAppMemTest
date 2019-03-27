namespace WebAppMemTest.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    //Leak
    [Route("api/[controller]")]
    [ApiController]
    public class MemTestController : ControllerBase
    {
        private MemtestDbContext Context { get; }

        public MemTestController(MemtestDbContext context)
        {
            Context = context;
        }

        [HttpGet("test/{id}")]
        public async Task<IActionResult> Test(string id)
        {
            var fromDate = DateTime.UtcNow.AddDays(-1);
            var entities = await Context.MemTestItems.Where(x => x.Created >= fromDate).ToListAsync();

            List<MemTestDto> results = new List<MemTestDto>();
            foreach (var entity in entities)
            {
                results.Add(new MemTestDto()
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Message = entity.Message,
                    Created = entity.Created
                });
            }

            return new JsonResult(results);
        }
    }

    //No leaks
    /*[Route("api/[controller]")]
    [ApiController]
    public class MemTestController : ControllerBase
    {
        [HttpGet("test/{id}")]
        public async Task<IActionResult> Test(string id)
        {
            int[] test = new int[1048576];
            for (int i = 0; i < 1048576; ++i)
            {
                test[i] = i;
            }

            await Task.CompletedTask;
            return new JsonResult(test[1000000].ToString());
        }
    }*/
}
