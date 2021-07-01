using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Placement;

namespace Grains
{
    [PreferLocalPlacement]
    public class GrainSecond: Grain, IGrainSecond
    {
        private readonly ILogger logger;

        public GrainSecond(ILogger<GrainSecond> logger)
        {
            this.logger = logger;
        }

        public Task Startup()
        {
            logger.LogInformation($"The second grain {this.GetPrimaryKeyLong()} activates in this silo.");
            return Task.CompletedTask;
        }

        public Task<string> Echo(string input)
        {
            logger.LogInformation($"I'm second grain {this.GetPrimaryKeyLong()}, I'm here.");
            return Task.FromResult($"Hello, {input}");
        }
    }
}
