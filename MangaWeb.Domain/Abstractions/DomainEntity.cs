using System.ComponentModel.DataAnnotations;

namespace DemoApp.Domain.Abstractions
{
    public abstract class DomainEntity <TKey>
    {
        [Key]
        public TKey Id { get; set; }
       
    }
}
