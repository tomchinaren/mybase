using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Order.Controllers
{
    public class OrderController : ApiController
    {
        static string domain = "http://localhost:63733/";
        static List<long> ids { get; set; } = new List<long>();
        static long NewId()
        {
            var newid = (ids.Count > 0 ? ids.Max() : 0) + 1;
            ids.Add(newid);
            return newid;
        }

        [HttpGet]
        public string test()
        {
            return "123";
        }

        [HttpPost]
        public Response<ParticipantLinkDTO> TryAdd()
        {
            var newid = NewId();

            return new Response<ParticipantLinkDTO>() {
                Code = 1,
                Data = new ParticipantLinkDTO()
                {
                    uri = domain + "Order/ConfirmAdd/" + newid,
                    expires = DateTime.Now.AddMinutes(3)
                },
                Message = "Order TryAdd"
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
                Message = "put Order ConfirmAdd"
            };
        }
    }


}
