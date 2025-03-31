//using EntityFrameworkCore.Triggered;
//using MyShopAPI.Core.IRepository;
//using MyShopAPI.Data.Entities;

//namespace MyShopAPI.Core.Triggers
//{
//    public class ModifyProductQuantity: IBeforeSaveTrigger<Cart>
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public ModifyProductQuantity(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }


//        async Task IBeforeSaveTrigger<Cart>.BeforeSave(ITriggerContext<Cart> context, CancellationToken cancellationToken)
//        {
//            if (context.ChangeType == ChangeType.Modified)
//            {
//                var productId = context.Entity.ProductId;
//                var quantity = context.Entity.Quantity;

//                var _product = await _unitOfWork.Products.Get(product => product.Id == productId);

//                var product = new Product
//                {
//                    Id = productId,
//                    Description = _product.Description,
//                };
//            }

//            return Task.CompletedTask;
//        }
//    }
//}
