using System;

namespace md.Data.Entites
{
    public class Consumer
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        // Date of birth
        public string Dob { get; set; }
        public string Email { get; set; }
    }
}
