using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class OrderInProgressViewComponent : ViewComponent
    {
        private readonly IOrderService _manager;

        public OrderInProgressViewComponent(IOrderService manager)
        {
            _manager = manager;
        }
        public string Invoke()
        {
            return _manager
                .NumberOfInProcess
                .ToString();
        }
    }
}
