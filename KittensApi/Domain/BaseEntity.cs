using System.ComponentModel.DataAnnotations.Schema;

namespace KittensApi.Domain
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}