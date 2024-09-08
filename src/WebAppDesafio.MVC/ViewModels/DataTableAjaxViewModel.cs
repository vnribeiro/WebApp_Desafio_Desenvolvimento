namespace WebAppDesafio.MVC.ViewModels
{
    public class DataTableAjaxViewModel
    {
        public DataTableAjaxViewModel()
        {
            Start = 0;
            Length = 10;
        }

        public int Start { get; set; }
        public int Length { get; set; }
        public object Data { get; set; }
    }
}
