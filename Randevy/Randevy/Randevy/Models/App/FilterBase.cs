namespace Randevy.Models.App
{
    public class FilterBase
    {
        public string Where { get; set; }
        public string Search { get; set; }
        public string Select { get; set; }
        public string OrderBy { get; set; }


        public int? Take { get; set; }
        public int? Skip { get; set; }
    }
}
