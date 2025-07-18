using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPCoreWebAPICRUD.Models;

public partial class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }  // ❌ Don't make this nullable

    [Required]
    public string StudentName { get; set; } = string.Empty;

    [Required]
    public string StudentGender { get; set; } = string.Empty;

    [Required]
    public int Age { get; set; }

    public int? Standard { get; set; }  // Standard can be optional

    public string? FatherName { get; set; }
}

