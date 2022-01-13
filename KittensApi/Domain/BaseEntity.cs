using System.ComponentModel.DataAnnotations.Schema;

namespace KittensApi.Domain
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}