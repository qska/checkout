using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaymentGateway.Banking.Contracts;

namespace PaymentGateway.Banking
{
    public class BankingGateway : IBankingGateway
    {
        public async Task<BankingProcessPaymentResponse> ProcessPaymentAsync(BankingProcessPaymentRequest request)
        {
            HttpClient client = new HttpClient();
            
            // reusing "our" object for brevity, in reality we'd have used the provider's data format.
            try
            {
                // TODO: move the banking service stub details to a config file
                var result = await client.PostAsync("http://localhost:64936/api/confirm",
                    new StringContent(JsonConvert.SerializeObject(request)));

                var resultPlainString = await result.Content.ReadAsStringAsync();
                var confirmResult = JsonConvert.DeserializeObject<ConfirmResult>(resultPlainString);
                return new BankingProcessPaymentResponse()
                {
                    Status = StatusEnum.Success, PaymentTranscationId = confirmResult.TransactionId
                };
            }
            catch (Exception ex)
            {
                return new BankingProcessPaymentResponse() { Status = StatusEnum.Failure, PaymentTranscationId = Guid.Empty };
            }
        }
    }
}
