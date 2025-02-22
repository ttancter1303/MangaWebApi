using System.ComponentModel.DataAnnotations;

namespace MangaWeb.Domain.Abstractions
{
    public abstract class DomainEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }

    }
}
