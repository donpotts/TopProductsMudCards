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
public class ProductController(ApplicationDbContext ctx) : ControllerBase
{
    [HttpGet("")]
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IQueryable<Product>> Get()
    {
        return Ok(ctx.Product.Include(x => x.Vendor));
    }

    [HttpGet("{key}")]
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetAsync(long key)
    {
        var product = await ctx.Product.Include(x => x.Vendor).FirstOrDefaultAsync(x => x.Id == key);

        if (product == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(product);
        }
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Product>> PostAsync(Product product)
    {
        await ctx.Product.AddAsync(product);

        await ctx.SaveChangesAsync();

        return Created($"/product/{product.Id}", product);
    }

    [HttpPut("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> PutAsync(long key, Product update)
    {
        var product = await ctx.Product.FirstOrDefaultAsync(x => x.Id == key);

        if (product == null)
        {
            return NotFound();
        }

        ctx.Entry(product).CurrentValues.SetValues(update);

        await ctx.SaveChangesAsync();

        return Ok(product);
    }

    [HttpPatch("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> PatchAsync(long key, Delta<Product> delta)
    {
        var product = await ctx.Product.FirstOrDefaultAsync(x => x.Id == key);

        if (product == null)
        {
            return NotFound();
        }

        delta.Patch(product);

        await ctx.SaveChangesAsync();

        return Ok(product);
    }

    [HttpDelete("{key}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(long key)
    {
        var product = await ctx.Product.FindAsync(key);

        if (product != null)
        {
            ctx.Product.Remove(product);
            await ctx.SaveChangesAsync();
        }

        return NoContent();
    }
}
