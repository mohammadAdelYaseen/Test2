using System.ComponentModel.DataAnnotations;

namespace TestFinal5.Models
{
   public class Student
   {
      public int ID { get; set; }
      [Display(Name = "Name")]
      public string StName { get; set; }
      public string Gender { get; set;}
      [Display(Name ="Total Mark")]
      public int Total { get; set;}
      [Display(Name = "Rating")]
      public char Rate { get; set; }
      [DataType(DataType.EmailAddress)]
      public string Email { get; set; }
      [DataType(DataType.Password)]
      public string Passsword { get; set; }
        public string? Eimage { get; set; }
    }
}
