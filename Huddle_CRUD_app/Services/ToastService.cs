using Huddle_CRUD.Core.Enum;

namespace Huddle_CRUD_app.Services
{
    public class ToastService
    {
        public event Action<string, ToastLevel, TimeSpan> OnShow = delegate { };

        public void ShowToast(string message, ToastLevel level, TimeSpan duration)
        {
            OnShow.Invoke(message, level,duration);
        }
    }

    
}
