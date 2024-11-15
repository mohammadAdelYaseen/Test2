using System.ComponentModel.DataAnnotations;

namespace TestFinal5.Models
{
   public class StudentView
   {
      public int ID { get; set; }
      [Display(Name = "Name")]
      [Required(ErrorMessage ="Required")]
      [StringLength(8,MinimumLength =3,ErrorMessage ="3 - 8")]
      public string StName { get; set; }
      public string Gender { get; set; }
      [Display(Name = "First Exame")]
      [Required(ErrorMessage ="required")]
      [Range(0,25,ErrorMessage ="0-25")]
      public int FirstExame { get; set; }
      [Display(Name = "Second Exame")]
      [Required(ErrorMessage = "required")]
      [Range(0, 25, ErrorMessage = "0-25")]
      public int SecondExame { get; set; }
      [Display(Name = "Final Exame")]
      [Required(ErrorMessage = "required")]
      [Range(0, 50, ErrorMessage = "0-50")]
      public int FinalExame { get; set; }
      
      [DataType(DataType.EmailAddress)]
      [Required(ErrorMessage = "required")]
      public string Email { get; set; }
      [DataType(DataType.Password)]
      [Required(ErrorMessage = "required")]
      public string Passsword { get; set; }
      public string? Eimage { get; set; }
   }
}
