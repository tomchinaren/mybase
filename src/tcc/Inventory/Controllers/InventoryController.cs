using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inventory.Controllers
{
    public class InventoryController : ApiController
    {
        static string domain = "http://localhost:63908/";
        static List<long> ids { get; set; } = new List<long>();
        static long NewId()
        {
            var newid = (ids.Count > 0 ? ids.Max() : 0) + 1;
            ids.Add(newid);
            return newid;
        }

        [HttpPost]
        public Response<ParticipantLinkDTO> TryAdd()
        {
            var newid = NewId();

            return new Response<ParticipantLinkDTO>()
            {
                Code = 1,
                Data = new ParticipantLinkDTO()
                {
                    uri = domain + "Inventory/ConfirmAdd/" + newid,
                    expires = DateTime.Now.AddMinutes(3)
                },
                Message = "Inventory TryAdd"
            };
        }

        [HttpPut]
        public Response<bool> ConfirmAdd(long id)
        {
            var has = ids.Exists(t => t == id);
            return new Response<bool>()
            {
                Code = 1,
                Data = has,
                Message = "put Inventory ConfirmAdd"
            };
        }
    }
}
