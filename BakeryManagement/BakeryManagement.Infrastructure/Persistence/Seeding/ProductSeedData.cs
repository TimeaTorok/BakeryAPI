using BakeryManagement.Domain.Entities;
using BakeryManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagement.Infrastructure.Persistence.Seeding
{
    public static class ProductSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                // Bread
                CreateProduct(1, "Country Sourdough Loaf", "Naturally leavened loaf with a crisp crust and open crumb.", ProductCategory.Bread, 19.50m),
                CreateProduct(2, "Classic White Loaf", "Soft sandwich loaf baked with unbleached wheat flour.", ProductCategory.Bread, 12.00m),
                CreateProduct(3, "Whole Wheat Loaf", "Hearty loaf made with whole wheat flour and a tender crumb.", ProductCategory.Bread, 14.50m),
                CreateProduct(4, "Seeded Multigrain Loaf", "Multigrain bread topped with sunflower, pumpkin, and sesame seeds.", ProductCategory.Bread, 18.00m),
                CreateProduct(5, "Rustic Rye Loaf", "Dense, gently tangy rye bread with a chewy crust.", ProductCategory.Bread, 17.00m),
                CreateProduct(6, "French Baguette", "Long, crisp baguette with a light and airy interior.", ProductCategory.Bread, 9.00m),
                CreateProduct(7, "Olive Ciabatta", "Open-crumb ciabatta folded with Kalamata olives.", ProductCategory.Bread, 15.50m),
                CreateProduct(8, "Rosemary Focaccia", "Olive-oil focaccia finished with rosemary and flaky salt.", ProductCategory.Bread, 16.00m),
                CreateProduct(9, "Garlic Herb Focaccia", "Soft focaccia topped with roasted garlic and garden herbs.", ProductCategory.Bread, 17.00m),
                CreateProduct(10, "Butter Brioche Loaf", "Rich, golden brioche made with butter and eggs.", ProductCategory.Bread, 20.00m),
                CreateProduct(11, "Braided Challah", "Golden braided bread with a soft, lightly sweet crumb.", ProductCategory.Bread, 18.50m),
                CreateProduct(12, "Pumpernickel Loaf", "Dark rye loaf with a deep, malty flavor.", ProductCategory.Bread, 18.00m),
                CreateProduct(13, "Cinnamon Raisin Bread", "Soft breakfast bread filled with raisins and cinnamon.", ProductCategory.Bread, 16.50m),
                CreateProduct(14, "Spelt Country Loaf", "Rustic loaf baked with spelt flour and a crisp crust.", ProductCategory.Bread, 19.00m),
                CreateProduct(15, "Walnut Sourdough Loaf", "Naturally leavened sourdough with toasted walnut pieces.", ProductCategory.Bread, 23.00m),

                // Pastry
                CreateProduct(16, "Butter Croissant", "Laminated pastry with a crisp shell and buttery layers.", ProductCategory.Pastry, 9.00m),
                CreateProduct(17, "Chocolate Croissant", "Flaky butter croissant filled with dark chocolate.", ProductCategory.Pastry, 11.00m),
                CreateProduct(18, "Pistachio Croissant", "Croissant filled with pistachio cream and topped with chopped pistachios.", ProductCategory.Pastry, 14.50m),
                CreateProduct(19, "Almond Croissant", "Butter croissant filled with almond cream and sliced almonds.", ProductCategory.Pastry, 13.00m),
                CreateProduct(20, "Pain aux Raisins", "Laminated pastry spiraled with vanilla custard and raisins.", ProductCategory.Pastry, 12.00m),
                CreateProduct(21, "Apple Turnover", "Crisp pastry filled with cinnamon-spiced apples.", ProductCategory.Pastry, 11.50m),
                CreateProduct(22, "Cherry Danish", "Flaky Danish pastry with vanilla cream and cherry filling.", ProductCategory.Pastry, 12.50m),
                CreateProduct(23, "Cheese Danish", "Golden Danish pastry filled with lightly sweetened cream cheese.", ProductCategory.Pastry, 12.00m),
                CreateProduct(24, "Kouign-Amann", "Caramelized Breton pastry with crisp, buttery layers.", ProductCategory.Pastry, 14.00m),
                CreateProduct(25, "Raspberry Danish", "Laminated pastry with raspberry filling and a light glaze.", ProductCategory.Pastry, 12.50m),
                CreateProduct(26, "Lemon Cream Tartlet", "Individual buttery tart shell filled with lemon cream.", ProductCategory.Pastry, 15.00m),
                CreateProduct(27, "Palmiers", "Crisp, caramelized puff pastry cookies with a delicate crunch.", ProductCategory.Pastry, 8.50m),
                CreateProduct(28, "Vanilla Cream Horn", "Flaky pastry horn filled with vanilla pastry cream.", ProductCategory.Pastry, 11.00m),
                CreateProduct(29, "Apricot Danish", "Buttery Danish pastry topped with apricot and vanilla glaze.", ProductCategory.Pastry, 12.50m),
                CreateProduct(30, "Cinnamon Morning Bun", "Laminated bun rolled in cinnamon sugar.", ProductCategory.Pastry, 11.50m),

                // Cake
                CreateProduct(31, "Red Velvet Cake Slice", "Cocoa sponge with a soft crumb and cream cheese frosting.", ProductCategory.Cake, 18.00m),
                CreateProduct(32, "Chocolate Fudge Cake Slice", "Dark chocolate cake layered with smooth fudge frosting.", ProductCategory.Cake, 19.50m),
                CreateProduct(33, "Vanilla Bean Cake Slice", "Vanilla sponge layered with vanilla bean buttercream.", ProductCategory.Cake, 17.00m),
                CreateProduct(34, "Carrot Cake Slice", "Spiced carrot cake with walnuts and cream cheese frosting.", ProductCategory.Cake, 18.50m),
                CreateProduct(35, "Black Forest Cake Slice", "Chocolate sponge layered with cherries and whipped cream.", ProductCategory.Cake, 20.00m),
                CreateProduct(36, "Lemon Drizzle Cake Slice", "Moist lemon cake finished with a bright citrus drizzle.", ProductCategory.Cake, 16.50m),
                CreateProduct(37, "Strawberry Shortcake Slice", "Vanilla sponge with fresh strawberries and whipped cream.", ProductCategory.Cake, 19.00m),
                CreateProduct(38, "Coconut Layer Cake Slice", "Tender coconut cake with coconut cream frosting.", ProductCategory.Cake, 18.50m),
                CreateProduct(39, "German Chocolate Cake Slice", "Chocolate cake with toasted coconut and pecan filling.", ProductCategory.Cake, 20.00m),
                CreateProduct(40, "Marble Bundt Cake", "Vanilla and chocolate swirled cake with a light glaze.", ProductCategory.Cake, 42.00m),
                CreateProduct(41, "Coffee Walnut Cake Slice", "Coffee sponge with toasted walnuts and coffee buttercream.", ProductCategory.Cake, 19.00m),
                CreateProduct(42, "Raspberry Almond Cake Slice", "Almond sponge layered with raspberry filling.", ProductCategory.Cake, 19.50m),
                CreateProduct(43, "Flourless Chocolate Cake Slice", "Rich, fudgy chocolate cake made without wheat flour.", ProductCategory.Cake, 21.00m),
                CreateProduct(44, "Opera Cake Slice", "Layers of almond sponge, coffee cream, and dark chocolate ganache.", ProductCategory.Cake, 22.00m),
                CreateProduct(45, "Honey Layer Cake Slice", "Thin honey cake layers with a creamy, lightly tangy filling.", ProductCategory.Cake, 20.00m),

                // Cookie
                CreateProduct(46, "Chocolate Chip Cookie", "Chewy cookie with dark chocolate chunks.", ProductCategory.Cookie, 6.00m),
                CreateProduct(47, "Double Chocolate Cookie", "Soft cocoa cookie packed with dark and milk chocolate.", ProductCategory.Cookie, 6.50m),
                CreateProduct(48, "Oatmeal Raisin Cookie", "Chewy oat cookie with raisins and a hint of cinnamon.", ProductCategory.Cookie, 5.50m),
                CreateProduct(49, "Snickerdoodle Cookie", "Soft cinnamon-sugar cookie with a lightly crisp edge.", ProductCategory.Cookie, 5.50m),
                CreateProduct(50, "Peanut Butter Cookie", "Tender peanut butter cookie with a rich roasted flavor.", ProductCategory.Cookie, 6.00m),
                CreateProduct(51, "Butter Shortbread", "Crumbly shortbread made with butter and a touch of salt.", ProductCategory.Cookie, 5.00m),
                CreateProduct(52, "Ginger Molasses Cookie", "Soft spiced cookie with ginger, cinnamon, and molasses.", ProductCategory.Cookie, 5.50m),
                CreateProduct(53, "White Chocolate Macadamia Cookie", "Chewy cookie with white chocolate and macadamia nuts.", ProductCategory.Cookie, 7.00m),
                CreateProduct(54, "Linzer Cookie", "Almond cookie sandwich with a raspberry jam center.", ProductCategory.Cookie, 7.00m),
                CreateProduct(55, "Vanilla Sugar Cookie", "Soft vanilla cookie with a delicate sugar finish.", ProductCategory.Cookie, 5.00m),
                CreateProduct(56, "Pistachio Cranberry Cookie", "Buttery cookie with pistachios and dried cranberries.", ProductCategory.Cookie, 7.00m),
                CreateProduct(57, "Coconut Macaroon", "Chewy coconut cookie with a golden toasted exterior.", ProductCategory.Cookie, 6.00m),
                CreateProduct(58, "Almond Biscotti", "Twice-baked almond biscuits made for dipping in coffee.", ProductCategory.Cookie, 6.50m),
                CreateProduct(59, "Brown Butter Cookie", "Chewy cookie with nutty brown butter and dark chocolate.", ProductCategory.Cookie, 6.50m),
                CreateProduct(60, "Lemon Crinkle Cookie", "Soft lemon cookie dusted with powdered sugar.", ProductCategory.Cookie, 5.50m),

                // Muffin
                CreateProduct(61, "Blueberry Muffin", "Soft muffin filled with blueberries and topped with a crumb cap.", ProductCategory.Muffin, 9.00m),
                CreateProduct(62, "Chocolate Muffin", "Moist cocoa muffin with chocolate chips.", ProductCategory.Muffin, 9.50m),
                CreateProduct(63, "Banana Walnut Muffin", "Ripe banana muffin with toasted walnuts.", ProductCategory.Muffin, 9.00m),
                CreateProduct(64, "Double Chocolate Muffin", "Rich chocolate muffin with dark chocolate chunks.", ProductCategory.Muffin, 10.00m),
                CreateProduct(65, "Lemon Poppy Seed Muffin", "Bright lemon muffin with poppy seeds and citrus glaze.", ProductCategory.Muffin, 9.00m),
                CreateProduct(66, "Apple Cinnamon Muffin", "Tender muffin with diced apples and cinnamon streusel.", ProductCategory.Muffin, 9.50m),
                CreateProduct(67, "Cranberry Orange Muffin", "Cranberry muffin with fresh orange zest.", ProductCategory.Muffin, 9.50m),
                CreateProduct(68, "Morning Glory Muffin", "Muffin with carrot, apple, raisins, and seeds.", ProductCategory.Muffin, 10.00m),
                CreateProduct(69, "Pumpkin Spice Muffin", "Pumpkin muffin warmly spiced with cinnamon and nutmeg.", ProductCategory.Muffin, 9.50m),
                CreateProduct(70, "Raspberry White Chocolate Muffin", "Raspberry muffin with white chocolate pieces.", ProductCategory.Muffin, 10.00m),
                CreateProduct(71, "Carrot Walnut Muffin", "Carrot muffin with walnuts and a gentle spice blend.", ProductCategory.Muffin, 9.50m),
                CreateProduct(72, "Cornbread Muffin", "Golden, lightly sweet cornbread muffin with a tender crumb.", ProductCategory.Muffin, 8.00m),
                CreateProduct(73, "Bran Raisin Muffin", "Wholesome bran muffin with plump raisins.", ProductCategory.Muffin, 8.50m),
                CreateProduct(74, "Pistachio Muffin", "Soft muffin with pistachios and a hint of cardamom.", ProductCategory.Muffin, 10.00m),
                CreateProduct(75, "Chocolate Chip Muffin", "Vanilla muffin generously filled with chocolate chips.", ProductCategory.Muffin, 9.00m),

                // Other
                CreateProduct(76, "Classic Fudge Brownie", "Dense chocolate brownie with a shiny, crackled top.", ProductCategory.Other, 9.00m),
                CreateProduct(77, "Salted Caramel Brownie", "Fudgy brownie swirled with caramel and flaky salt.", ProductCategory.Other, 11.00m),
                CreateProduct(78, "White Chocolate Blondie", "Buttery blondie with white chocolate chunks.", ProductCategory.Other, 9.00m),
                CreateProduct(79, "Lemon Tart", "Buttery tart shell filled with smooth, tangy lemon curd.", ProductCategory.Other, 17.00m),
                CreateProduct(80, "Pecan Pie Bar", "Shortbread bar topped with caramelized pecans.", ProductCategory.Other, 11.00m),
                CreateProduct(81, "Raspberry Crumble Bar", "Oat crumble bar with a bright raspberry filling.", ProductCategory.Other, 9.50m),
                CreateProduct(82, "Baked Cheesecake Slice", "Creamy baked cheesecake on a buttery biscuit base.", ProductCategory.Other, 19.00m),
                CreateProduct(83, "Spinach Feta Hand Pie", "Flaky savory pastry filled with spinach and feta.", ProductCategory.Other, 13.00m),
                CreateProduct(84, "Mushroom Thyme Hand Pie", "Golden hand pie filled with mushrooms and thyme.", ProductCategory.Other, 13.50m),
                CreateProduct(85, "Sausage Roll", "Puff pastry wrapped around seasoned sausage filling.", ProductCategory.Other, 12.00m),
                CreateProduct(86, "Cheese Straws", "Crisp puff pastry twists baked with aged cheese.", ProductCategory.Other, 8.00m),
                CreateProduct(87, "Soft Pretzel", "Chewy baked pretzel finished with coarse salt.", ProductCategory.Other, 8.50m),
                CreateProduct(88, "Cinnamon Babka", "Braided sweet bread swirled with cinnamon and cocoa.", ProductCategory.Other, 18.00m),
                CreateProduct(89, "Vanilla Scone", "Tender, lightly sweet scone with a golden top.", ProductCategory.Other, 9.00m),
                CreateProduct(90, "Apple Crumble Tart", "Buttery tart filled with apples and topped with oat crumble.", ProductCategory.Other, 16.00m)
            );
        }

        private static Product CreateProduct(
            int seedNumber,
            string name,
            string description,
            ProductCategory category,
            decimal price)
        {
            return new Product
            {
                Id = Guid.Parse($"00000000-0000-0000-0000-{seedNumber:D12}"),
                Name = name,
                Description = description,
                Category = category,
                Price = price,
                IsAvailable = true
            };
        }
    }
}
