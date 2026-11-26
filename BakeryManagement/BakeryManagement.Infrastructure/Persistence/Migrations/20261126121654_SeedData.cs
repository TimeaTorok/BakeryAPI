using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BakeryManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "ana.popescu@crumbandco.example", "Ana", "Popescu", "+40 700 000 001" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "mihai.ionescu@crumbandco.example", "Mihai", "Ionescu", "+40 700 000 002" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "elena.marin@crumbandco.example", "Elena", "Marin", null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "andrei.dumitrescu@crumbandco.example", "Andrei", "Dumitrescu", "+40 700 000 004" },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "ioana.radu@crumbandco.example", "Ioana", "Radu", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Bread", "Naturally leavened loaf with a crisp crust and open crumb.", true, "Country Sourdough Loaf", 19.50m },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Bread", "Soft sandwich loaf baked with unbleached wheat flour.", true, "Classic White Loaf", 12.00m },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Bread", "Hearty loaf made with whole wheat flour and a tender crumb.", true, "Whole Wheat Loaf", 14.50m },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Bread", "Multigrain bread topped with sunflower, pumpkin, and sesame seeds.", true, "Seeded Multigrain Loaf", 18.00m },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Bread", "Dense, gently tangy rye bread with a chewy crust.", true, "Rustic Rye Loaf", 17.00m },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Bread", "Long, crisp baguette with a light and airy interior.", true, "French Baguette", 9.00m },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Bread", "Open-crumb ciabatta folded with Kalamata olives.", true, "Olive Ciabatta", 15.50m },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Bread", "Olive-oil focaccia finished with rosemary and flaky salt.", true, "Rosemary Focaccia", 16.00m },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Bread", "Soft focaccia topped with roasted garlic and garden herbs.", true, "Garlic Herb Focaccia", 17.00m },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Bread", "Rich, golden brioche made with butter and eggs.", true, "Butter Brioche Loaf", 20.00m },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "Bread", "Golden braided bread with a soft, lightly sweet crumb.", true, "Braided Challah", 18.50m },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "Bread", "Dark rye loaf with a deep, malty flavor.", true, "Pumpernickel Loaf", 18.00m },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "Bread", "Soft breakfast bread filled with raisins and cinnamon.", true, "Cinnamon Raisin Bread", 16.50m },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "Bread", "Rustic loaf baked with spelt flour and a crisp crust.", true, "Spelt Country Loaf", 19.00m },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "Bread", "Naturally leavened sourdough with toasted walnut pieces.", true, "Walnut Sourdough Loaf", 23.00m },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "Pastry", "Laminated pastry with a crisp shell and buttery layers.", true, "Butter Croissant", 9.00m },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "Pastry", "Flaky butter croissant filled with dark chocolate.", true, "Chocolate Croissant", 11.00m },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "Pastry", "Croissant filled with pistachio cream and topped with chopped pistachios.", true, "Pistachio Croissant", 14.50m },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "Pastry", "Butter croissant filled with almond cream and sliced almonds.", true, "Almond Croissant", 13.00m },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "Pastry", "Laminated pastry spiraled with vanilla custard and raisins.", true, "Pain aux Raisins", 12.00m },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "Pastry", "Crisp pastry filled with cinnamon-spiced apples.", true, "Apple Turnover", 11.50m },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "Pastry", "Flaky Danish pastry with vanilla cream and cherry filling.", true, "Cherry Danish", 12.50m },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "Pastry", "Golden Danish pastry filled with lightly sweetened cream cheese.", true, "Cheese Danish", 12.00m },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "Pastry", "Caramelized Breton pastry with crisp, buttery layers.", true, "Kouign-Amann", 14.00m },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "Pastry", "Laminated pastry with raspberry filling and a light glaze.", true, "Raspberry Danish", 12.50m },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "Pastry", "Individual buttery tart shell filled with lemon cream.", true, "Lemon Cream Tartlet", 15.00m },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "Pastry", "Crisp, caramelized puff pastry cookies with a delicate crunch.", true, "Palmiers", 8.50m },
                    { new Guid("00000000-0000-0000-0000-000000000028"), "Pastry", "Flaky pastry horn filled with vanilla pastry cream.", true, "Vanilla Cream Horn", 11.00m },
                    { new Guid("00000000-0000-0000-0000-000000000029"), "Pastry", "Buttery Danish pastry topped with apricot and vanilla glaze.", true, "Apricot Danish", 12.50m },
                    { new Guid("00000000-0000-0000-0000-000000000030"), "Pastry", "Laminated bun rolled in cinnamon sugar.", true, "Cinnamon Morning Bun", 11.50m },
                    { new Guid("00000000-0000-0000-0000-000000000031"), "Cake", "Cocoa sponge with a soft crumb and cream cheese frosting.", true, "Red Velvet Cake Slice", 18.00m },
                    { new Guid("00000000-0000-0000-0000-000000000032"), "Cake", "Dark chocolate cake layered with smooth fudge frosting.", true, "Chocolate Fudge Cake Slice", 19.50m },
                    { new Guid("00000000-0000-0000-0000-000000000033"), "Cake", "Vanilla sponge layered with vanilla bean buttercream.", true, "Vanilla Bean Cake Slice", 17.00m },
                    { new Guid("00000000-0000-0000-0000-000000000034"), "Cake", "Spiced carrot cake with walnuts and cream cheese frosting.", true, "Carrot Cake Slice", 18.50m },
                    { new Guid("00000000-0000-0000-0000-000000000035"), "Cake", "Chocolate sponge layered with cherries and whipped cream.", true, "Black Forest Cake Slice", 20.00m },
                    { new Guid("00000000-0000-0000-0000-000000000036"), "Cake", "Moist lemon cake finished with a bright citrus drizzle.", true, "Lemon Drizzle Cake Slice", 16.50m },
                    { new Guid("00000000-0000-0000-0000-000000000037"), "Cake", "Vanilla sponge with fresh strawberries and whipped cream.", true, "Strawberry Shortcake Slice", 19.00m },
                    { new Guid("00000000-0000-0000-0000-000000000038"), "Cake", "Tender coconut cake with coconut cream frosting.", true, "Coconut Layer Cake Slice", 18.50m },
                    { new Guid("00000000-0000-0000-0000-000000000039"), "Cake", "Chocolate cake with toasted coconut and pecan filling.", true, "German Chocolate Cake Slice", 20.00m },
                    { new Guid("00000000-0000-0000-0000-000000000040"), "Cake", "Vanilla and chocolate swirled cake with a light glaze.", true, "Marble Bundt Cake", 42.00m },
                    { new Guid("00000000-0000-0000-0000-000000000041"), "Cake", "Coffee sponge with toasted walnuts and coffee buttercream.", true, "Coffee Walnut Cake Slice", 19.00m },
                    { new Guid("00000000-0000-0000-0000-000000000042"), "Cake", "Almond sponge layered with raspberry filling.", true, "Raspberry Almond Cake Slice", 19.50m },
                    { new Guid("00000000-0000-0000-0000-000000000043"), "Cake", "Rich, fudgy chocolate cake made without wheat flour.", true, "Flourless Chocolate Cake Slice", 21.00m },
                    { new Guid("00000000-0000-0000-0000-000000000044"), "Cake", "Layers of almond sponge, coffee cream, and dark chocolate ganache.", true, "Opera Cake Slice", 22.00m },
                    { new Guid("00000000-0000-0000-0000-000000000045"), "Cake", "Thin honey cake layers with a creamy, lightly tangy filling.", true, "Honey Layer Cake Slice", 20.00m },
                    { new Guid("00000000-0000-0000-0000-000000000046"), "Cookie", "Chewy cookie with dark chocolate chunks.", true, "Chocolate Chip Cookie", 6.00m },
                    { new Guid("00000000-0000-0000-0000-000000000047"), "Cookie", "Soft cocoa cookie packed with dark and milk chocolate.", true, "Double Chocolate Cookie", 6.50m },
                    { new Guid("00000000-0000-0000-0000-000000000048"), "Cookie", "Chewy oat cookie with raisins and a hint of cinnamon.", true, "Oatmeal Raisin Cookie", 5.50m },
                    { new Guid("00000000-0000-0000-0000-000000000049"), "Cookie", "Soft cinnamon-sugar cookie with a lightly crisp edge.", true, "Snickerdoodle Cookie", 5.50m },
                    { new Guid("00000000-0000-0000-0000-000000000050"), "Cookie", "Tender peanut butter cookie with a rich roasted flavor.", true, "Peanut Butter Cookie", 6.00m },
                    { new Guid("00000000-0000-0000-0000-000000000051"), "Cookie", "Crumbly shortbread made with butter and a touch of salt.", true, "Butter Shortbread", 5.00m },
                    { new Guid("00000000-0000-0000-0000-000000000052"), "Cookie", "Soft spiced cookie with ginger, cinnamon, and molasses.", true, "Ginger Molasses Cookie", 5.50m },
                    { new Guid("00000000-0000-0000-0000-000000000053"), "Cookie", "Chewy cookie with white chocolate and macadamia nuts.", true, "White Chocolate Macadamia Cookie", 7.00m },
                    { new Guid("00000000-0000-0000-0000-000000000054"), "Cookie", "Almond cookie sandwich with a raspberry jam center.", true, "Linzer Cookie", 7.00m },
                    { new Guid("00000000-0000-0000-0000-000000000055"), "Cookie", "Soft vanilla cookie with a delicate sugar finish.", true, "Vanilla Sugar Cookie", 5.00m },
                    { new Guid("00000000-0000-0000-0000-000000000056"), "Cookie", "Buttery cookie with pistachios and dried cranberries.", true, "Pistachio Cranberry Cookie", 7.00m },
                    { new Guid("00000000-0000-0000-0000-000000000057"), "Cookie", "Chewy coconut cookie with a golden toasted exterior.", true, "Coconut Macaroon", 6.00m },
                    { new Guid("00000000-0000-0000-0000-000000000058"), "Cookie", "Twice-baked almond biscuits made for dipping in coffee.", true, "Almond Biscotti", 6.50m },
                    { new Guid("00000000-0000-0000-0000-000000000059"), "Cookie", "Chewy cookie with nutty brown butter and dark chocolate.", true, "Brown Butter Cookie", 6.50m },
                    { new Guid("00000000-0000-0000-0000-000000000060"), "Cookie", "Soft lemon cookie dusted with powdered sugar.", true, "Lemon Crinkle Cookie", 5.50m },
                    { new Guid("00000000-0000-0000-0000-000000000061"), "Muffin", "Soft muffin filled with blueberries and topped with a crumb cap.", true, "Blueberry Muffin", 9.00m },
                    { new Guid("00000000-0000-0000-0000-000000000062"), "Muffin", "Moist cocoa muffin with chocolate chips.", true, "Chocolate Muffin", 9.50m },
                    { new Guid("00000000-0000-0000-0000-000000000063"), "Muffin", "Ripe banana muffin with toasted walnuts.", true, "Banana Walnut Muffin", 9.00m },
                    { new Guid("00000000-0000-0000-0000-000000000064"), "Muffin", "Rich chocolate muffin with dark chocolate chunks.", true, "Double Chocolate Muffin", 10.00m },
                    { new Guid("00000000-0000-0000-0000-000000000065"), "Muffin", "Bright lemon muffin with poppy seeds and citrus glaze.", true, "Lemon Poppy Seed Muffin", 9.00m },
                    { new Guid("00000000-0000-0000-0000-000000000066"), "Muffin", "Tender muffin with diced apples and cinnamon streusel.", true, "Apple Cinnamon Muffin", 9.50m },
                    { new Guid("00000000-0000-0000-0000-000000000067"), "Muffin", "Cranberry muffin with fresh orange zest.", true, "Cranberry Orange Muffin", 9.50m },
                    { new Guid("00000000-0000-0000-0000-000000000068"), "Muffin", "Muffin with carrot, apple, raisins, and seeds.", true, "Morning Glory Muffin", 10.00m },
                    { new Guid("00000000-0000-0000-0000-000000000069"), "Muffin", "Pumpkin muffin warmly spiced with cinnamon and nutmeg.", true, "Pumpkin Spice Muffin", 9.50m },
                    { new Guid("00000000-0000-0000-0000-000000000070"), "Muffin", "Raspberry muffin with white chocolate pieces.", true, "Raspberry White Chocolate Muffin", 10.00m },
                    { new Guid("00000000-0000-0000-0000-000000000071"), "Muffin", "Carrot muffin with walnuts and a gentle spice blend.", true, "Carrot Walnut Muffin", 9.50m },
                    { new Guid("00000000-0000-0000-0000-000000000072"), "Muffin", "Golden, lightly sweet cornbread muffin with a tender crumb.", true, "Cornbread Muffin", 8.00m },
                    { new Guid("00000000-0000-0000-0000-000000000073"), "Muffin", "Wholesome bran muffin with plump raisins.", true, "Bran Raisin Muffin", 8.50m },
                    { new Guid("00000000-0000-0000-0000-000000000074"), "Muffin", "Soft muffin with pistachios and a hint of cardamom.", true, "Pistachio Muffin", 10.00m },
                    { new Guid("00000000-0000-0000-0000-000000000075"), "Muffin", "Vanilla muffin generously filled with chocolate chips.", true, "Chocolate Chip Muffin", 9.00m },
                    { new Guid("00000000-0000-0000-0000-000000000076"), "Other", "Dense chocolate brownie with a shiny, crackled top.", true, "Classic Fudge Brownie", 9.00m },
                    { new Guid("00000000-0000-0000-0000-000000000077"), "Other", "Fudgy brownie swirled with caramel and flaky salt.", true, "Salted Caramel Brownie", 11.00m },
                    { new Guid("00000000-0000-0000-0000-000000000078"), "Other", "Buttery blondie with white chocolate chunks.", true, "White Chocolate Blondie", 9.00m },
                    { new Guid("00000000-0000-0000-0000-000000000079"), "Other", "Buttery tart shell filled with smooth, tangy lemon curd.", true, "Lemon Tart", 17.00m },
                    { new Guid("00000000-0000-0000-0000-000000000080"), "Other", "Shortbread bar topped with caramelized pecans.", true, "Pecan Pie Bar", 11.00m },
                    { new Guid("00000000-0000-0000-0000-000000000081"), "Other", "Oat crumble bar with a bright raspberry filling.", true, "Raspberry Crumble Bar", 9.50m },
                    { new Guid("00000000-0000-0000-0000-000000000082"), "Other", "Creamy baked cheesecake on a buttery biscuit base.", true, "Baked Cheesecake Slice", 19.00m },
                    { new Guid("00000000-0000-0000-0000-000000000083"), "Other", "Flaky savory pastry filled with spinach and feta.", true, "Spinach Feta Hand Pie", 13.00m },
                    { new Guid("00000000-0000-0000-0000-000000000084"), "Other", "Golden hand pie filled with mushrooms and thyme.", true, "Mushroom Thyme Hand Pie", 13.50m },
                    { new Guid("00000000-0000-0000-0000-000000000085"), "Other", "Puff pastry wrapped around seasoned sausage filling.", true, "Sausage Roll", 12.00m },
                    { new Guid("00000000-0000-0000-0000-000000000086"), "Other", "Crisp puff pastry twists baked with aged cheese.", true, "Cheese Straws", 8.00m },
                    { new Guid("00000000-0000-0000-0000-000000000087"), "Other", "Chewy baked pretzel finished with coarse salt.", true, "Soft Pretzel", 8.50m },
                    { new Guid("00000000-0000-0000-0000-000000000088"), "Other", "Braided sweet bread swirled with cinnamon and cocoa.", true, "Cinnamon Babka", 18.00m },
                    { new Guid("00000000-0000-0000-0000-000000000089"), "Other", "Tender, lightly sweet scone with a golden top.", true, "Vanilla Scone", 9.00m },
                    { new Guid("00000000-0000-0000-0000-000000000090"), "Other", "Buttery tart filled with apples and topped with oat crumble.", true, "Apple Crumble Tart", 16.00m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "Notes", "Status", "TotalPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 11, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000001"), "A selection of sourdough, pastry, and cake.", "Confirmed", 52.00m, null },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 11, 21, 10, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000001"), "Muffin and brownie.", "Confirmed", 18.50m, null },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 11, 22, 8, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000002"), null, "Confirmed", 9.00m, null }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "ProductNameAtOrder", "Quantity", "UnitPriceAtOrder" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), "Country Sourdough Loaf", 1, 19.50m },
                    { new Guid("30000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000018"), "Pistachio Croissant", 1, 14.50m },
                    { new Guid("30000000-0000-0000-0000-000000000003"), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000031"), "Red Velvet Cake Slice", 1, 18.00m },
                    { new Guid("30000000-0000-0000-0000-000000000004"), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000062"), "Chocolate Muffin", 1, 9.50m },
                    { new Guid("30000000-0000-0000-0000-000000000005"), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000076"), "Classic Fudge Brownie", 1, 9.00m },
                    { new Guid("30000000-0000-0000-0000-000000000006"), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000016"), "Butter Croissant", 1, 9.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));
        }
    }
}
