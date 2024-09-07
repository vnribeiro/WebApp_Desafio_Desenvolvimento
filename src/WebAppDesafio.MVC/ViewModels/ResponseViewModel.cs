using WebAppDesafio.MVC.ViewModels.Enums;

namespace WebAppDesafio.MVC.ViewModels
{
    public class ResponseViewModel : ErrorViewModel
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public AlertTypes Type { get; set; }
        public string Message { get; set; }

        public ResponseViewModel() { }

        public ResponseViewModel(
            string message,
            AlertTypes type,
            string controller = "",
            string action = "")
        {
            Message = message;
            Type = type;
            Controller = controller;
            Action = action;
        }

        public ResponseViewModel(Exception exception)
        {
            Type = AlertTypes.error;
            Action = "Error";
            Message = exception.Message;
        }
    }
}
