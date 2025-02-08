namespace StudentCRUD.Models;
using System.ComponentModel.DataAnnotations;

    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        public string? StudentName { get; set; }

        [Required]
        public string? StudentAddress { get; set; }
    }
