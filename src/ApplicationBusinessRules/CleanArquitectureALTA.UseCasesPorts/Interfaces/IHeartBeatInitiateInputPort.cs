using Alta.DTOs;
using System.Threading.Tasks;

namespace Alta.UseCasesPorts.Interfaces
{
    public interface IHeartBeatInitiateInputPort
    {
        Task Handle(HeartBeatInitiateDTO heartBeatInitiateDTO);
    }
}
