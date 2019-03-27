using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Lab3_Client.Models
{
    [DataContract(IsReference = true)]
    public class ArtType
    {
        public ArtType()
        {
            this.Artworks = new HashSet<Artwork>();
        }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        [Display(Name = "Art Type")]
        [Required(ErrorMessage = "You cannot leave the art type name black.")]
        [StringLength(25, ErrorMessage = "Art type cannot be more than 25 characters long.")]
        public string Type { get; set; }

        public virtual ICollection<Artwork> Artworks { get; set; }
    }
}
