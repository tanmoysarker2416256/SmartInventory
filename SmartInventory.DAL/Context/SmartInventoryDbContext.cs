using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.DAL.Context;

public class SmartInventoryDbContext : IdentityDbContext<ApplicationUser , IdentityRole , String>
{
    public SmartInventoryDbContext(DbContextOptions<SmartInventoryDbContext>options) : base(options)
    {      
    }

   public DbSet<Product> Products { get; set; }

}
