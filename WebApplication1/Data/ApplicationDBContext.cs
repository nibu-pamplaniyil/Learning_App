﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;
public class ApplicationDBContext : IdentityDbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }
    public DbSet<Register> Registers { get; set; }
    public DbSet<Skills> Skills { get; set; }
    public DbSet<Profile> Profile{get;set;}
    public DbSet<Contact> Contact{get;set;}
    public DbSet<SubSkills> SubSkills{get; set;}
}
