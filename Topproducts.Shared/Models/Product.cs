using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Topproducts.Shared.Models;

[DataContract]
public class Product
{
    [DataMember]
    public string? Photo { get; set; }

    [DataMember]
    public string? Name { get; set; }

    [DataMember]
    public string? Description { get; set; }

    [DataMember]
    public decimal? Price { get; set; }

    [DataMember]
    public decimal? Rating { get; set; }

    [Display(Name = "Vendor Id")]
    [DataMember]
    public long? VendorId { get; set; }

    [Key]
    [DataMember]
    public long? Id { get; set; }

    [DataMember]
    public Vendor? Vendor { get; set; }
}
