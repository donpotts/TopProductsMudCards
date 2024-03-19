using System.ComponentModel.DataAnnotations;

namespace Topproducts.Shared.Models;

public class ApplicationUserWithRolesDto : ApplicationUserDto
{
    public List<string>? Roles { get; set; }
}
