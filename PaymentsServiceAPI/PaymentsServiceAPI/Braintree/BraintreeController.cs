using Braintree;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestPaymentAPI.Models;

namespace TestPaymentAPI.Controllers
{
    [Route("api/[Controller]")]
    [Produces("application/json")]
    public class BraintreeController : Controller
    {
        private BraintreeGateway _Gateway { get; set; }

        public BraintreeController()
        {
            _Gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "x4g6yxg36ypds2rv",
                PublicKey = "r4k9gn8zy6vgd6n3",
                PrivateKey = "1d4028fb0f72c72109b6602f20e0fda6"
            };
        }

        [Route("token")]
        [HttpGet]
        public ActionResult GetToken()
        {
            var ct = new ClientToken(_Gateway.ClientToken.Generate());
            var res = JsonConvert.SerializeObject(ct);
            return Content(res);
        }

        [Route("buy")]
        [HttpPost]
        public ActionResult CreatePurchase([FromBody] Nonce nonce)
        {
            var request = new TransactionRequest
            {
                Amount = nonce.chargeAmount,
                PaymentMethodNonce = nonce.nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            var res = JsonConvert.SerializeObject(request);

            Result<Transaction> result = _Gateway.Transaction.Sale(request);

            if (result.IsSuccess())
            {
                var transaction = result.Target;

                return Content(JsonConvert.SerializeObject(transaction));
            }
            else if (result.Transaction != null)
            {
                return NotFound();
            }
            else
            {
                string errorMessages = "";
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
                }
                TempData["Flash"] = errorMessages;
                return BadRequest(errorMessages);
            }
        }
    }
}