using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcc
{
    /*
    try A
    try B
    transaction Confirm(confirm A,confirm B)
    */
    class Program
    {
        static RPC.RPCHttpClient rpcClient = new RPC.RPCHttpClient();
        static string OrderBaseUrl = "http://localhost:63733/Order/";
        static string InventoryBaseUrl = "http://localhost:63908/Inventory/";
        static string TsBaseUrl = "http://localhost:63994/TsCoordinator/";
        static void Main(string[] args)
        {
            try
            {
                var tsRequest = new Request<List<ParticipantLinkDTO>>() { Data = new List<ParticipantLinkDTO>() };

                //try A
                Console.WriteLine("A Begin...");
                var resA = rpcClient.PostAsync<Response<ParticipantLinkDTO>>(OrderBaseUrl + "TryAdd").Result;
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(resA));
                Console.WriteLine();
                tsRequest.Data.Add(resA.Data);

                //try B
                Console.WriteLine("B Begin...");
                var resB = rpcClient.PostAsync<Response<ParticipantLinkDTO>>(InventoryBaseUrl + "TryAdd").Result;
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(resB));
                Console.WriteLine();
                tsRequest.Data.Add(resB.Data);

                //confirm ts(confirm A,confirm B)
                Console.WriteLine("Ts Begin...");
                var resTs = rpcClient.PutAsync<Request<List<ParticipantLinkDTO>>, List<Response<bool>>>(TsBaseUrl + "Confirm", tsRequest).Result;
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(resTs));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}