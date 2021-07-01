
using System.Threading.Tasks;
using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Grains
{
    public class GrainFirst : Grain, IGrainFirst
    {
        private readonly ILogger logger;

        public GrainFirst(ILogger<GrainFirst> logger)
        {
            this.logger = logger;
        }

        public async Task Work()
        {
            logger.LogInformation($"The first grain {this.GetPrimaryKeyLong()} activates in this silo.");
            var second = this.GrainFactory.GetGrain<IGrainSecond>(this.GetPrimaryKeyLong());
            await second.Startup();
        }
    }

}
