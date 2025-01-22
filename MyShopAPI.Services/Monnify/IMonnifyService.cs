using MyShopAPI.Data.Entities;
using MyShopAPI.Services.Models.Monnify.BankTransfer;
using MyShopAPI.Services.Models.Monnify.ChargeCard;

namespace MyShopAPI.Services.Monnify
{
    public interface IMonnifyService
    {
        Task Authorization();
        Task<string> InitilaizeTransaction(List<CartAndWishlist> items, string cutomerEmail);
        Task<BankTransferResponse> GetBankTransferInfo(string bankCode,string transactionReference);
        Task<ChargeCardResponse> CardPayment(ChargeCardRequest chargeCard);
    }
}
