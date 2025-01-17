﻿namespace MachineTestABM.DTO
{
    public class StudentCreateDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string? Image { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public IFormFile? ImageUp {  get; set; }
    }

    public class StudentEditDTO
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string? Image { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public IFormFile? ImageUp { get; set; }
    }
}
