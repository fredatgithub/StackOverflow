using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ConsoleApplication;

namespace AspNetCoreSqlite.Migrations
{
    [DbContext(typeof(AlgaeDbContext))]
    [Migration("20160414234803_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc3-20565");

            modelBuilder.Entity("ConsoleApplication.Sandbox", b =>
                {
                    b.Property<int>("SandboxId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("SandboxId");

                    b.ToTable("Sandboxes");
                });
        }
    }
}
