using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Topproducts.Data;
using Topproducts.Shared.Models;

namespace Topproducts.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[EnableRateLimiting("Fixed")]
public class VendorController(ApplicationDbContext ctx) : ControllerBase
{
    [HttpGet("")]
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IQueryable<Vendor>> Get()
    {
        return Ok(ctx.Vendor);
    }

    [HttpGet("{key}")]
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Vendor>> GetAsync(long key)
    {
        var vendor = await ctx.Vendor.FirstOrDefaultAsync(x => x.Id == key);

        if (vendor == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(vendor);
        }
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Vendor>> PostAsync(Vendor vendor)
    {
        await ctx.Vendor.AddAsync(vendor);

        await ctx.SaveChangesAsync();

        return Created($"/vendor/{vendor.Id}", vendor);
    }

    [HttpPut("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Vendor>> PutAsync(long key, Vendor update)
    {
        var vendor = await ctx.Vendor.FirstOrDefaultAsync(x => x.Id == key);

        if (vendor == null)
        {
            return NotFound();
        }

        ctx.Entry(vendor).CurrentValues.SetValues(update);

        await ctx.SaveChangesAsync();

        return Ok(vendor);
    }

    [HttpPatch("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Vendor>> PatchAsync(long key, Delta<Vendor> delta)
    {
        var vendor = await ctx.Vendor.FirstOrDefaultAsync(x => x.Id == key);

        if (vendor == null)
        {
            return NotFound();
        }

        delta.Patch(vendor);

        await ctx.SaveChangesAsync();

        return Ok(vendor);
    }

    [HttpDelete("{key}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(long key)
    {
        var vendor = await ctx.Vendor.FindAsync(key);

        if (vendor != null)
        {
            ctx.Vendor.Remove(vendor);
            await ctx.SaveChangesAsync();
        }

        return NoContent();
    }
}
