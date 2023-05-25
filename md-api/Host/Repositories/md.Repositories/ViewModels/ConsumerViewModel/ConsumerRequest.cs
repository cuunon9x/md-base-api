using System;

namespace md.Repositories.ViewModels.ConsumerViewModel
{
    public class ConsumerRequest
    {
        public string FullName { get; set; }
        // Date of birth
        public DateTime Dob { get; set; }
        public string Email { get; set; }
    }
}
