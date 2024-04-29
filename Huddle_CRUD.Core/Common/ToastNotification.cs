using Huddle_CRUD.Core.Enum;

namespace Huddle_CRUD.Core.Common
{
    public class ToastNotification
    {
        public string Message { get; set; }
        public ToastLevel ToastLevel { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
