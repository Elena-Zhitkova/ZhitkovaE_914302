using System.ComponentModel.DataAnnotations;
namespace ScientificMagazine.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        
        public int MagazineId { get; set; }

        public string ArticleName { get; set; }
        public string ArticleAuthor { get; set; }
        public string ArticlePDF { get; set; }
        public string Keywords { get; set; }
        public string Annotation { get; set; }
        public int GradeQuoting { get; set; }
        public int GradeDowload { get; set; }
        public int GradeViews { get; set; }
    }
}
