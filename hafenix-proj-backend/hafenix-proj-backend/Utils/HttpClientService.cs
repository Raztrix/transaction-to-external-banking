using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using hafenix_proj_backend.DTO.DTOHelpers;
using hafenix_proj_backend.DTO.DTORes;

namespace hafenix_proj_backend.Utils
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OpenBankingGetTokenDTORes> OpenBankingGetToken(OpenBankingGetTokenDTO dto)
        {
            if (String.IsNullOrWhiteSpace(dto.URL))
            {
                return new OpenBankingGetTokenDTORes() { StatusCode = StatusCode.Failed, Token = "", ErrorType = ErrorType.WrongURL };
            }

            if (String.IsNullOrWhiteSpace(dto.UserId))
            {
                return new OpenBankingGetTokenDTORes() { StatusCode = StatusCode.Failed, Token = "", ErrorType = ErrorType.WrongUserId };
            }
            // stored it in enviroment vriable for portability.
            var SecretId = Environment.GetEnvironmentVariable("SecretId");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SecretId", SecretId);

            var json = JsonSerializer.Serialize(new { dto.UserId });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //var response = await _httpClient.PostAsync(dto.URL, content); // need to fake the call so...

            //response.EnsureSuccessStatusCode();
            //return await response.Content.ReadAsStringAsync();
            
            OpenBankingGetTokenDTORes res = new OpenBankingGetTokenDTORes() {StatusCode = StatusCode.Success, Token = "12345" };

            return res;
        }

        public async Task<OpenBankingDepositOrWithdrawDTORes> OpenBankingDepositOrWithdraw(OpenBankingDepositOrWithdrawDTO data)
        {
            if (String.IsNullOrWhiteSpace(data.Token))
            {
                return new OpenBankingDepositOrWithdrawDTORes() { StatusCode = StatusCode.Failed };
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", data.Token);
            var TransactionAction = new
            {
                AccountNumber = data.AccountNumber,
                Amount = data.Amount,
            };
            var json = JsonSerializer.Serialize(TransactionAction);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //var response = await _httpClient.PostAsync(data.Token, content); // need to fake the call so...

            //response.EnsureSuccessStatusCode();
            //return await response.Content.ReadAsStringAsync();

            OpenBankingDepositOrWithdrawDTORes res = new OpenBankingDepositOrWithdrawDTORes() { StatusCode = StatusCode.Success, AccountNumber = TransactionAction.AccountNumber, Amount = TransactionAction.Amount };

            return res;
        }
    }
}
