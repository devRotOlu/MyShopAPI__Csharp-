using EntityFrameworkCore.Triggered;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;

namespace MyShopAPI.Core.Triggers
{
    //public class ModifyProductQuantity : IAfterSaveTrigger<Cart>
    //{
    //    private readonly IUnitOfWork _unitOfWork;

    //    public ModifyProductQuantity(IUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //    } 

    //    public Task AfterSave(ITriggerContext<Cart> context, CancellationToken cancellationToken)
    //    {
    //        if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Deleted)
    //        {
    //            var quanity = ((int)context.ChangeType);
    //        }
    //    }
    //}
}
