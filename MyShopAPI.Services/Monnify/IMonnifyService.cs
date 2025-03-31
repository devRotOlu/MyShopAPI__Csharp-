using MyShopAPI.Data.Entities;
using MyShopAPI.Services.Models.Monnify;
using MyShopAPI.Services.Models.Monnify.BankTransfer;
using MyShopAPI.Services.Models.Monnify.ChargeCard;
using MyShopAPI.Services.Models.Monnify.TransactionStatus;

namespace MyShopAPI.Services.Monnify
{
    public interface IMonnifyService
    {
        Task Authorization();
        Task<InitialTransactionResponse> InitilaizeTransaction(List<Cart> items, string cutomerEmail);
        Task<BankTransferResponse> GetBankTransferInfo(string bankCode,string transactionReference);
        Task<ChargeCardResponse> CardPayment(ChargeCardRequest chargeCard);
        Task<TransactionStatus> GetTransactionStatus(string transactionRef);
    }
}
