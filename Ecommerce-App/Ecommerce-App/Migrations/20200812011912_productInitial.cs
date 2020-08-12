using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_App.Migrations
{
    public partial class productInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sku = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Image", "Name", "Price", "Sku" },
                values: new object[,]
                {
                    { 1, "A big brother to the do-it-all, 18-liter Luzon, the Cotopaxi Luzon Del Dia 24-liter pack offers everything you love about the Luzon, plus added capacity and a few extra features.", "https://www.rei.com/media/a38ca266-913f-4eb0-b225-9cf1529e78b8?size=784x588", "Cotopaxi Luzon 24L Pack - Del Dia", 75.00m, "UGG-BB-PUR-01" },
                    { 2, "Attention to detail, maximum functionality, minimalist design and unrivaled craftsmanship combine in the fast, light, hard-wearing Arc'teryx Alpha FL 45 pack for climbers and ski alpinists.", "https://www.rei.com/media/742eeb03-ad67-47a5-ab0a-a4833e5201c1?size=784x588", "Arc'teryx Alpha FL 45 Pack", 289.00m, "UGG-BB-PUR-02" },
                    { 3, "Built on decades of experience in alpine terrain, the Mammut Trion 50 is a big crag pack that brings can't do without features to ice and alpine climbing, or even lightweight backpacking adventures.", "https://www.rei.com/media/84ed0547-3596-4cf5-9623-846352be57de?size=784x588", "Mammut Trion 50 Pack - Men's", 209.00m, "UGG-BB-PUR-03" },
                    { 4, "Bridging the gap between a pack and a feature-rich rope bag, the Mammut Neon Gear 45 pack is a worthy crag carrier with duffel-style backpack straps to help you move quickly between routes.", "https://www.rei.com/media/29395346-1cce-4ead-aef3-8fda32bef05a?size=784x588", "Mammut Neon Gear 45 Pack", 159.00m, "UGG-BB-PUR-04" },
                    { 5, "Transport your climbing shoes, chalk and post-gym essentials in The North Face Cinder 28 pack. This burly, easy-access pack is sure to be the one you grab when you're headed to the wall.", "https://www.rei.com/media/8f5003a2-faf5-4844-9043-fd882267597c?size=784x588", "The North Face Cinder 28 Pack", 99.00m, "UGG-BB-PUR-05" },
                    { 6, "Trad climbers, peak baggers and off-trail scramblers count on the Mountain Hardwear Scrambler 25 pack to do the job. Durable and light, it also doubles as a dependable pack for day hikers.", "https://www.rei.com/media/27ba41a9-b8d7-4ee7-a50d-f2efcf71629d?size=784x588", "Mountain Hardwear Scrambler 25 Pack", 140.00m, "UGG-BB-PUR-06" },
                    { 7, "Fight to secure that key cam placement, but don't fight to get the gear in and out of your pack. The Patagonia Cragsmith 32L pack allows instant access via the zippered back panel and zippered top.", "https://www.rei.com/media/2e3b2ccd-8c77-4e9d-9b54-63e05e0eb801?size=784x588", "Patagonia Cragsmith 32L Pack", 169.00m, "UGG-BB-PUR-07" },
                    { 8, "Built tough for multi-pitch rock, alpine and ice climbing, the Arc'teryx Alpha AR 20 pack features high-tenacity nylon fabric with a liquid crystal polymer ripstop grid that stands up to abuse.", "https://www.rei.com/media/a0c8ea6a-fcbe-40bb-b32e-7a8498c2881c?size=784x588", "Arc'teryx Alpha AR 20 Pack", 129.00m, "UGG-BB-PUR-08" },
                    { 9, "Designed for done-in-a-day alpine missions or for multi-pitch climbs, the Osprey Mutant 22 pack is great for the vertical enthusiast looking for a streamlined, svelte pack.", "https://www.rei.com/media/1707aacc-8b50-4368-9e85-ce4806a2a1da?size=784x588", "Osprey Mutant 22 Pack", 100.00m, "UGG-BB-PUR-09" },
                    { 10, "A do-it-all combo of a duffel and BD creek pack, the Black Diamond Crag 40 pack is a one-and-done hauler for sport climbers, boulderers and trad climbers to port essential gear, guidebooks and snacks.", "https://www.rei.com/media/3b2cce38-c6e7-4579-a152-f8c8334a0c33?size=784x588", "Black Diamond Crag 40 Pack", 99.00m, "UGG-BB-PUR-10" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
