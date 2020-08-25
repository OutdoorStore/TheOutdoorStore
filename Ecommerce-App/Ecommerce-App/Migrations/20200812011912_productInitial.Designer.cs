﻿// <auto-generated />
using Ecommerce_App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ecommerce_App.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20200812011912_productInitial")]
    partial class productInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ecommerce_App.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Sku")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A big brother to the do-it-all, 18-liter Luzon, the Cotopaxi Luzon Del Dia 24-liter pack offers everything you love about the Luzon, plus added capacity and a few extra features.",
                            Image = "https://www.rei.com/media/a38ca266-913f-4eb0-b225-9cf1529e78b8?size=784x588",
                            Name = "Cotopaxi Luzon 24L Pack - Del Dia",
                            Price = 75.00m,
                            Sku = "UGG-BB-PUR-01"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Attention to detail, maximum functionality, minimalist design and unrivaled craftsmanship combine in the fast, light, hard-wearing Arc'teryx Alpha FL 45 pack for climbers and ski alpinists.",
                            Image = "https://www.rei.com/media/742eeb03-ad67-47a5-ab0a-a4833e5201c1?size=784x588",
                            Name = "Arc'teryx Alpha FL 45 Pack",
                            Price = 289.00m,
                            Sku = "UGG-BB-PUR-02"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Built on decades of experience in alpine terrain, the Mammut Trion 50 is a big crag pack that brings can't do without features to ice and alpine climbing, or even lightweight backpacking adventures.",
                            Image = "https://www.rei.com/media/84ed0547-3596-4cf5-9623-846352be57de?size=784x588",
                            Name = "Mammut Trion 50 Pack - Men's",
                            Price = 209.00m,
                            Sku = "UGG-BB-PUR-03"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Bridging the gap between a pack and a feature-rich rope bag, the Mammut Neon Gear 45 pack is a worthy crag carrier with duffel-style backpack straps to help you move quickly between routes.",
                            Image = "https://www.rei.com/media/29395346-1cce-4ead-aef3-8fda32bef05a?size=784x588",
                            Name = "Mammut Neon Gear 45 Pack",
                            Price = 159.00m,
                            Sku = "UGG-BB-PUR-04"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Transport your climbing shoes, chalk and post-gym essentials in The North Face Cinder 28 pack. This burly, easy-access pack is sure to be the one you grab when you're headed to the wall.",
                            Image = "https://www.rei.com/media/8f5003a2-faf5-4844-9043-fd882267597c?size=784x588",
                            Name = "The North Face Cinder 28 Pack",
                            Price = 99.00m,
                            Sku = "UGG-BB-PUR-05"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Trad climbers, peak baggers and off-trail scramblers count on the Mountain Hardwear Scrambler 25 pack to do the job. Durable and light, it also doubles as a dependable pack for day hikers.",
                            Image = "https://www.rei.com/media/27ba41a9-b8d7-4ee7-a50d-f2efcf71629d?size=784x588",
                            Name = "Mountain Hardwear Scrambler 25 Pack",
                            Price = 140.00m,
                            Sku = "UGG-BB-PUR-06"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Fight to secure that key cam placement, but don't fight to get the gear in and out of your pack. The Patagonia Cragsmith 32L pack allows instant access via the zippered back panel and zippered top.",
                            Image = "https://www.rei.com/media/2e3b2ccd-8c77-4e9d-9b54-63e05e0eb801?size=784x588",
                            Name = "Patagonia Cragsmith 32L Pack",
                            Price = 169.00m,
                            Sku = "UGG-BB-PUR-07"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Built tough for multi-pitch rock, alpine and ice climbing, the Arc'teryx Alpha AR 20 pack features high-tenacity nylon fabric with a liquid crystal polymer ripstop grid that stands up to abuse.",
                            Image = "https://www.rei.com/media/a0c8ea6a-fcbe-40bb-b32e-7a8498c2881c?size=784x588",
                            Name = "Arc'teryx Alpha AR 20 Pack",
                            Price = 129.00m,
                            Sku = "UGG-BB-PUR-08"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Designed for done-in-a-day alpine missions or for multi-pitch climbs, the Osprey Mutant 22 pack is great for the vertical enthusiast looking for a streamlined, svelte pack.",
                            Image = "https://www.rei.com/media/1707aacc-8b50-4368-9e85-ce4806a2a1da?size=784x588",
                            Name = "Osprey Mutant 22 Pack",
                            Price = 100.00m,
                            Sku = "UGG-BB-PUR-09"
                        },
                        new
                        {
                            Id = 10,
                            Description = "A do-it-all combo of a duffel and BD creek pack, the Black Diamond Crag 40 pack is a one-and-done hauler for sport climbers, boulderers and trad climbers to port essential gear, guidebooks and snacks.",
                            Image = "https://www.rei.com/media/3b2cce38-c6e7-4579-a152-f8c8334a0c33?size=784x588",
                            Name = "Black Diamond Crag 40 Pack",
                            Price = 99.00m,
                            Sku = "UGG-BB-PUR-10"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
