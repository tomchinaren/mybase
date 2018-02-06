using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace TsCoordinator.Controllers
{
    public class TsCoordinatorController : ApiController
    {
        static List<Ts> TsList { get; set; } = new List<Ts>();

        [HttpPut]
        public async Task<List<Response<bool>>> Confirm(Request<List<ParticipantLinkDTO>> request)
        {
            var resList = new List<Response<bool>>();
            foreach (var one in request.Data)
            {
                var res = await ConfirmOne(one);
                resList.Add(res);
            }

            return resList;
        }

        private Task<Response<bool>> ConfirmOne(ParticipantLinkDTO part)
        {
            var client = new RPC.RPCHttpClient();
            var request = new Request<ParticipantLinkDTO>() { Data = part };
            return client.PutAsync<Request<ParticipantLinkDTO>, Response<bool>>(part.uri, request);
        }
    }

    public class Ts
    {
        public long ID { get; set; }
        public List<ParticipantLinkDTO> Parts { get; set; }
        public List<int> PartStates { get; set; }
        public int State { get; set; }

    }

}
