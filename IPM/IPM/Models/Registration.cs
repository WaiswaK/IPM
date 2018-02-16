namespace IPM.Models
{
    class Registration
    {
        public string Message { get; set; }
        public ModelState Modelstate { get; set; }

    }
    class ModelState
    {
        public string Password { get; set; }
        public string Empty { get; set; }
    }
}
