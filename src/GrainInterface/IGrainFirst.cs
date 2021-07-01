using System;
using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IGrainFirst : IGrainWithIntegerKey
    {
        public Task Work();
    }
}
