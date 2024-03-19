using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Topproducts.Shared.Models;

[DataContract]
public class Vendor
{
    [Key]
    [DataMember]
    public long? Id { get; set; }

    [DataMember]
    public string? Type { get; set; }

    [DataMember]
    public string? Company { get; set; }

    [Display(Name = "Contact Name")]
    [DataMember]
    public string? ContactName { get; set; }

    [Display(Name = "Phone Number")]
    [DataMember]
    public string? PhoneNumber { get; set; }

    [DataMember]
    public string? Email { get; set; }

    [Display(Name = "Street Address")]
    [DataMember]
    public string? StreetAddress { get; set; }

    [DataMember]
    public string? City { get; set; }

    [DataMember]
    public string? State { get; set; }

    [DataMember]
    public string? Zip { get; set; }

    [DataMember]
    public string? Country { get; set; }
}
