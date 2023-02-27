using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace ScientificMagazine.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public string Name { get; set; } = "";
        [AllowNull]
        public string Author { get; set; } = "";

        [AllowNull] public string Email { get; set; } = "";
        [AllowNull] public string Assignee { get; set; } = "";
        [AllowNull] public string Status { get; set; } = "";
        [AllowNull] public string CriticReviewFile { get; set; } = "";
        [AllowNull] public string RedactorReviewFile { get; set; } = "";
        [AllowNull] public string KeyWords { get; set; } = "";
        [AllowNull] public string ArticleFile { get; set; } = "";
        [AllowNull] public string GradeAntiplagiat { get; set; } = "";
        [AllowNull] public string AuthorUserId { get; set; } = "";
        [AllowNull] public string Annotation { get; set; } = "";


    }
}
