using SV.Application.Dtos;
using SV.Application.Output;

namespace SV.Application.ServiceContract
{
    public interface IClassifyService
	{
		GetResults<ClassifyDto> GetAll();
	}
}