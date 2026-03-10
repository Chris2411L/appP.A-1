using System;
using System.Collections.Generic;

namespace appP.A.Models
{
    public static class AppData
    {
        public static List<Categoria> Categorias { get; set; } = new();
        public static Carrito CarritoActual { get; set; } = new Carrito();

        public static bool IsAdmin { get; set; } = false;

        private static int _nextProductId = 1;
        public static int GetNextProductId() => _nextProductId++;

        static AppData()
        {
            static string Slug(string s) => (s ?? "").ToLower().Replace(" ", "-")
                .Replace("á", "a").Replace("é", "e").Replace("í", "i")
                .Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n");

            static void Llenar(Categoria cat, string prefijo, string[] variantes, int total, double basePrecio, double paso = 1.0)
            {
                var rnd = new Random();
                for (int i = 0; i < total; i++)
                {
                    var v = variantes[i % variantes.Length];
                    var nombre = $"{prefijo} {v}";
                    var desc = $"Delicioso {prefijo.ToLower()} sabor {v}. Calidad premium para ti.";
                    var precio = Math.Round(basePrecio + (i % 10) * paso, 2);
                    var precioAnterior = Math.Round(precio + 5.00, 2);

                    // ==========================================
                    // MAPEO DE IMÁGENES PARA TODOS LOS PRODUCTOS
                    // ==========================================
                    string img = v switch
                    {
                        // --- BEBIDAS ---
                        "Cola" => "https://us.coca-cola.com/content/dam/nagbrands/us/coke/en/value-collection/coca-cola-1.25-liter-new.png",
                        "Naranja" => "https://tse2.mm.bing.net/th/id/OIP.if2XEAYsnMMh-LxF625BiAHaHa",
                        "Limón" => "https://www.coca-cola.com/content/dam/onexp/bo/es/brands/sprite/new/sprite-bo.jpg",
                        "Manzana" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750102201554L.jpg",
                        "Uva" => "https://tse2.mm.bing.net/th/id/OIP.fzbeYC9ASYhg_-nC8bIeiAHaHa",
                        "Tamarindo" => "https://tse2.mm.bing.net/th/id/OIP.6bsH4OX8YSfp1s-EB31qGQHaHa",
                        "Mango" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00000007500175L.jpg",
                        "Durazno" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00001983620021L.jpg",
                        "Mineral" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00002113601054L.jpg",
                        "Energética" => "https://tse3.mm.bing.net/th/id/OIP.FemY_hu9GpxrqWb9NZw_UQHaHa",
                        "Té Verde" => "https://i5.walmartimages.com/gr/images/product-images/img_large/00750105535889L.jpg",
                        "Té Negro" => "https://www.soriana.com/on/demandware.static/-/Sites-soriana-grocery-master-catalog/default/dw0784733d/images/product/7501055358861_A.jpg",
                        "Café Frío" => "https://images-v2.rappi.co.cr/products/1f5b63e7-0021-45e1-b578-9cbb4ea1c4aa.jpg",
                        "Horchata" => "https://tse3.mm.bing.net/th/id/OIP.jbtNjbKVM5EIRy0eaF9OewHaHa",
                        "Jamaica" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00075810400631L.jpg",

                        // --- SNACKS ---
                        "Sabritas" => "https://th.bing.com/th/id/R.6623bc46db7eef2390e1162b6ae5fe21?rik=Eb%2fPWVdeTG6NQg&pid=ImgRaw&r=0",
                        "Doritos" => "https://tfchgi.vteximg.com.br/arquivos/ids/172756-1000-1000/doritos.jpg",
                        "Ruffles" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750047804422L.jpg",
                        "Cheetos" => "https://tse2.mm.bing.net/th/id/OIP.TXD5MWtSbM6xz8bVsxtEAAHaHa",
                        "Takis" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00075752800868L.jpg",
                        "Rancheritos" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750047804460L.jpg",
                        "Crujitos" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750101111559L.jpg",
                        "Totis" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750225110188L.jpg",
                        "Fritos" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750101113106L.jpg",
                        "Churritos" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750101113334L.jpg",
                        "Papas Adobadas" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750101116757L.jpg",
                        "Jalapeño" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750047801089L.jpg",
                        "Queso" => $"https://placehold.co/600x600/FFB300/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Limoncito" => $"https://placehold.co/600x600/7CB342/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Mix Botanero" => $"https://placehold.co/600x600/D84315/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",

                        // --- DULCES ---
                        "Paleta" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750101111624L.jpg",
                        "Pulparindo" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00000007246520L.jpg",
                        "Panditas" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750102540450L.jpg",
                        "Mazapán" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750102060047L.jpg",
                        "Skittles" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00004000000160L.jpg",
                        "Jolly" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00002070900085L.jpg",
                        "Halls" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00762230070989L.jpg",
                        "Rockaleta" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750101113061L.jpg",
                        "Pelón" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00004111600551L.jpg",
                        "Lucas" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750101111267L.jpg",
                        "Tamborcito" => $"https://placehold.co/600x600/E91E63/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Cachetada" => $"https://placehold.co/600x600/9C27B0/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Caramelo" => $"https://placehold.co/600x600/F44336/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Malvavisco" => $"https://placehold.co/600x600/F8BBD0/E91E63?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Dulce de Leche" => $"https://placehold.co/600x600/795548/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",

                        // --- CHOCOLATES ---
                        "Snickers" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00004000026240L.jpg",
                        "KitKat" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750105861734L.jpg",
                        "Crunch" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750105861778L.jpg",
                        "Kisses" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00003400024005L.jpg",
                        "Carlos V" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750105861775L.jpg",
                        "Abuelita" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750105861754L.jpg",
                        "Milky Way" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00004000026241L.jpg",
                        "Ferrero" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00800050000378L.jpg",
                        "Bubu Lubu" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750102540451L.jpg",
                        "Gansito" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750103046316L.jpg",
                        "Kinder" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00800050014603L.jpg",
                        "Amargo 70%" => $"https://placehold.co/600x600/3E2723/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Crema Cacahuate" => $"https://placehold.co/600x600/FFB300/3E2723?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Avellana" => $"https://placehold.co/600x600/5D4037/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Menta" => $"https://placehold.co/600x600/004D40/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",

                        // --- ENCHILADOS ---
                        "Sandía" => $"https://placehold.co/600x600/D32F2F/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Chamoy" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00004111600551L.jpg",
                        "Miguelito" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750101111267L.jpg",
                        "Piña" => $"https://placehold.co/600x600/FBC02D/D32F2F?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Pepino" => $"https://placehold.co/600x600/388E3C/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Guayaba" => $"https://placehold.co/600x600/F57C00/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Ciruela" => $"https://placehold.co/600x600/880E4F/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Manzana Verde" => $"https://placehold.co/600x600/689F38/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Pepino-Chile" => $"https://placehold.co/600x600/1B5E20/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Piña-Chile" => $"https://placehold.co/600x600/F57F17/D32F2F?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Chile-Limón" => $"https://placehold.co/600x600/C62828/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",

                        // --- GOMITAS ---
                        "Ositos" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750102540450L.jpg",
                        "Aros" => $"https://placehold.co/600x600/FF9800/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Gusanitos" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750102540455L.jpg",
                        "Frutas" => $"https://placehold.co/600x600/9C27B0/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Ácidas" => $"https://placehold.co/600x600/CDDC39/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Corazones" => $"https://placehold.co/600x600/E91E63/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Colas" => $"https://placehold.co/600x600/5D4037/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Surtidas" => $"https://placehold.co/600x600/00BCD4/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Arándano" => $"https://placehold.co/600x600/3F51B5/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Cereza" => $"https://placehold.co/600x600/D32F2F/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",

                        // --- GALLETAS ---
                        "Emperador Limón" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750100067320L.jpg",
                        "Chokis" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750100063162L.jpg",
                        "Marías" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750100060364L.jpg",
                        "Oreo" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00762230073248L.jpg",
                        "Príncipe" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00762230076274L.jpg",
                        "Triki Trakes" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750100060370L.jpg",
                        "Canelitas" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750100060368L.jpg",
                        "Habaneras" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750100060372L.jpg",
                        "Animalitos" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750100060375L.jpg",
                        "Surtido Rico" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750100060378L.jpg",
                        "Mantequilla" => $"https://placehold.co/600x600/FFC107/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Avena" => $"https://placehold.co/600x600/BCAAA4/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Chocolate" => $"https://placehold.co/600x600/4E342E/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Rellenas" => $"https://placehold.co/600x600/D7CCC8/4E342E?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Vainilla" => $"https://placehold.co/600x600/FFF9C4/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",

                        // --- CHICLES ---
                        "Trident" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00762230085002L.jpg",
                        "Clorets" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00762230071191L.jpg",
                        "Bubbaloo" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00762230082729L.jpg",
                        "Motita" => $"https://placehold.co/600x600/E91E63/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Orbit" => $"https://placehold.co/600x600/2196F3/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Doublemint" => $"https://placehold.co/600x600/4CAF50/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "BigTime" => $"https://placehold.co/600x600/E040FB/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Drops" => $"https://placehold.co/600x600/00BCD4/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Hierbabuena" => $"https://placehold.co/600x600/8BC34A/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Fresa" => $"https://placehold.co/600x600/F44336/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Mora Azul" => $"https://placehold.co/600x600/3F51B5/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Canela" => $"https://placehold.co/600x600/BF360C/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",

                        // --- IMPORTADOS ---
                        "Pocky" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00007314115201L.jpg",
                        "Hi-Chew" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00007314115202L.jpg",
                        "KitKat Japón" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00007314115203L.jpg",
                        "Twix" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00004000026245L.jpg",
                        "Butterfinger" => $"https://placehold.co/600x600/FFC107/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Reese’s" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00003400000016L.jpg",
                        "Mentos" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00007314115204L.jpg",
                        "Haribo" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00007314115205L.jpg",
                        "Toffee" => $"https://placehold.co/600x600/A1887F/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Turco Lokum" => $"https://placehold.co/600x600/D81B60/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Choco Pie" => $"https://placehold.co/600x600/C2185B/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Pepero" => $"https://placehold.co/600x600/388E3C/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Milkita" => $"https://placehold.co/600x600/1E88E5/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Laffy Taffy" => $"https://placehold.co/600x600/FFEB3B/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",

                        // --- PAN Y PASTELITOS ---
                        "Concha" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00000000003080L.jpg",
                        "Mantecadas" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750103046187L.jpg",
                        "Gansito Pan" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750103046316L.jpg",
                        "Roles Canela" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750103043236L.jpg",
                        "Napo" => $"https://placehold.co/600x600/F57C00/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Magdalena" => $"https://placehold.co/600x600/FFB300/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Donita" => "https://i5.walmartimages.com.mx/gr/images/product-images/img_large/00750103046318L.jpg",
                        "Panquesito Marmoleado" => $"https://placehold.co/600x600/5D4037/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Cuernito Dulce" => $"https://placehold.co/600x600/FFA000/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Oreja" => $"https://placehold.co/600x600/FFCA28/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Pan de Elote" => $"https://placehold.co/600x600/FFF59D/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Berlín" => $"https://placehold.co/600x600/D32F2F/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Panqué Vainilla" => $"https://placehold.co/600x600/FFF176/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Panqué Choco" => $"https://placehold.co/600x600/4E342E/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Panqué Nuez" => $"https://placehold.co/600x600/8D6E63/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",

                        // --- REPOSTERÍA ---
                        "Brownie" => $"https://placehold.co/600x600/3E2723/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Pay de Queso" => $"https://placehold.co/600x600/FFF59D/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Pay de Limón" => $"https://placehold.co/600x600/AED581/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Cheesecake Fresa" => $"https://placehold.co/600x600/F06292/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Flan" => $"https://placehold.co/600x600/FFD54F/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Gelatina Mosaico" => $"https://placehold.co/600x600/BA68C8/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Cupcake Vainilla" => $"https://placehold.co/600x600/FFF9C4/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Cupcake Choco" => $"https://placehold.co/600x600/5D4037/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Cupcake RedVelvet" => $"https://placehold.co/600x600/C62828/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Alfajor" => $"https://placehold.co/600x600/D7CCC8/4E342E?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Macaron" => $"https://placehold.co/600x600/F8BBD0/C2185B?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Rol de Guayaba" => $"https://placehold.co/600x600/FFCC80/E65100?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Rol de Zarzamora" => $"https://placehold.co/600x600/9575CD/311B92?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Tres Leches" => $"https://placehold.co/600x600/ECEFF1/37474F?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Tiramisú" => $"https://placehold.co/600x600/795548/EFEBE9?text={Uri.EscapeDataString(nombre)}&font=Montserrat",

                        // --- COMBOS Y OFERTAS ---
                        "Combo Escolar" => $"https://placehold.co/600x600/2196F3/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Fiesta" => $"https://placehold.co/600x600/E91E63/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Gamer" => $"https://placehold.co/600x600/00E676/333333?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Cine" => $"https://placehold.co/600x600/B71C1C/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Oficina" => $"https://placehold.co/600x600/607D8B/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Viaje" => $"https://placehold.co/600x600/00ACC1/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Dulcero" => $"https://placehold.co/600x600/FF4081/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Picante" => $"https://placehold.co/600x600/DD2C00/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Kids" => $"https://placehold.co/600x600/FFEB3B/E65100?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Premium" => $"https://placehold.co/600x600/263238/FFD700?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Mix" => $"https://placehold.co/600x600/673AB7/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Para 2" => $"https://placehold.co/600x600/F06292/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Familiar" => $"https://placehold.co/600x600/4CAF50/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Ahorro" => $"https://placehold.co/600x600/FF9800/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",
                        "Combo Sorpresa" => $"https://placehold.co/600x600/9C27B0/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat",

                        // Por defecto en caso de un error
                        _ => $"https://placehold.co/600x600/4A148C/FFFFFF?text={Uri.EscapeDataString(nombre)}&font=Montserrat"
                    };

                    var rating = Math.Round(rnd.NextDouble() * 5, 1);

                    var prod = new Producto(nombre, desc, precio, precioAnterior, rating, img, cat.Nombre)
                    {
                        Id = _nextProductId++
                    };

                    cat.Productos.Add(prod);
                }
            }

            // ====== Categorías y variantes ======
            var bebidas = new Categoria("Bebidas", "https://th.bing.com/th/id/OIP.i-pXxfR-oXDEusBRc-O6TgHaFu?o=7");
            var snacks = new Categoria("Snacks", "https://image.freepik.com/vector-gratis/coleccion-colorida-snacks-diseno-plano_23-2147915369.jpg");
            var dulces = new Categoria("Dulces", "https://th.bing.com/th/id/OIP.YZp6ukUjmitj4wQosxFjjQHaHa?o=7");
            var chocolates = new Categoria("Chocolates", "https://png.pngtree.com/png-clipart/20230913/original/pngtree-chocolate-clipart-cute-set-of-chocolate-with-leaves-and-nuts-cartoon-png-image_11078025.png");
            var enchilados = new Categoria("Enchilados", "https://thumbs.dreamstime.com/b/chili-peppers-99018780.jpg");
            var gomitas = new Categoria("Gomitas", "https://th.bing.com/th/id/OIP.H9vqd2TDSyj_Xc6a7cfSsgHaHa?w=192&h=192&c=7&r=0&o=7&cb=12&pid=1.7&rm=3");
            var galletas = new Categoria("Galletas", "https://tse2.mm.bing.net/th/id/OIP.IGxt6eTx__HBNJ8QjwSkNAHaHL?cb=12&rs=1&pid=ImgDetMain&o=7&rm=3");
            var chicles = new Categoria("Chicles", "https://img.freepik.com/vector-premium/maquina-chicles-pasada-moda-divertida-linda-vector-icono-ilustracion-dibujos-animados-dibujados-mano-aislado-sobre-fondo-blanco-caramelo-concepto-logotipo-maquina-dispensadora-chicle_92289-2988.jpg?w=1480");
            var importados = new Categoria("Importados", "https://img.freepik.com/vector-premium/camion-reparto-aislado-cajas-carga-paquetes-dibujos-animados-plana_101884-722.jpg?w=2000");
            var panPastelitos = new Categoria("Pan y Pastelitos", "https://static.vecteezy.com/system/resources/previews/024/818/161/non_2x/cartoon-cake-illustration-cute-design-ai-generative-png.png");
            var reposteria = new Categoria("Repostería", "https://img.freepik.com/vector-premium/lindo-chef-pasteleria-terminando-postre-vector-dibujos-animados_1022901-102101.jpg");
            var combosOfertas = new Categoria("Combos y Ofertas", "https://img.freepik.com/psd-premium/cartel-super-ofertas-fondo-amarillo_658787-1014.jpg?w=2000");

            var vBebidas = new[] { "Cola", "Naranja", "Limón", "Manzana", "Uva", "Tamarindo", "Mango", "Durazno", "Mineral", "Energética", "Té Verde", "Té Negro", "Café Frío", "Horchata", "Jamaica" };
            var vSnacks = new[] { "Sabritas", "Doritos", "Ruffles", "Cheetos", "Takis", "Rancheritos", "Crujitos", "Totis", "Fritos", "Churritos", "Papas Adobadas", "Jalapeño", "Queso", "Limoncito", "Mix Botanero" };
            var vDulces = new[] { "Paleta", "Pulparindo", "Panditas", "Mazapán", "Skittles", "Jolly", "Halls", "Rockaleta", "Pelón", "Lucas", "Tamborcito", "Cachetada", "Caramelo", "Malvavisco", "Dulce de Leche" };
            var vChoco = new[] { "Snickers", "KitKat", "Crunch", "Kisses", "Carlos V", "Abuelita", "Milky Way", "Ferrero", "Bubu Lubu", "Gansito", "Kinder", "Amargo 70%", "Crema Cacahuate", "Avellana", "Menta" };
            var vEnchi = new[] { "Tamarindo", "Mango", "Sandía", "Chamoy", "Miguelito", "Limón", "Piña", "Pepino", "Guayaba", "Ciruela", "Manzana Verde", "Durazno", "Pepino-Chile", "Piña-Chile", "Chile-Limón" };
            var vGomitas = new[] { "Ositos", "Aros", "Gusanitos", "Frutas", "Ácidas", "Corazones", "Colas", "Surtidas", "Sandía", "Mango", "Arándano", "Cereza", "Durazno", "Piña", "Uva" };
            var vGalletas = new[] { "Emperador Limón", "Chokis", "Marías", "Oreo", "Príncipe", "Triki Trakes", "Canelitas", "Habaneras", "Animalitos", "Surtido Rico", "Mantequilla", "Avena", "Chocolate", "Rellenas", "Vainilla" };
            var vChicles = new[] { "Trident", "Clorets", "Bubbaloo", "Motita", "Orbit", "Doublemint", "BigTime", "Drops", "Menta", "Hierbabuena", "Fresa", "Uva", "Sandía", "Mora Azul", "Canela" };
            var vImport = new[] { "Pocky", "Hi-Chew", "KitKat Japón", "Twix", "Butterfinger", "Reese’s", "Mentos", "Haribo", "Toffee", "Turco Lokum", "Choco Pie", "Pepero", "Milkita", "Laffy Taffy" };
            var vPan = new[] { "Concha", "Mantecadas", "Gansito Pan", "Roles Canela", "Napo", "Magdalena", "Donita", "Panquesito Marmoleado", "Cuernito Dulce", "Oreja", "Pan de Elote", "Berlín", "Panqué Vainilla", "Panqué Choco", "Panqué Nuez" };
            var vRepost = new[] { "Brownie", "Pay de Queso", "Pay de Limón", "Cheesecake Fresa", "Flan", "Gelatina Mosaico", "Cupcake Vainilla", "Cupcake Choco", "Cupcake RedVelvet", "Alfajor", "Macaron", "Rol de Guayaba", "Rol de Zarzamora", "Tres Leches", "Tiramisú" };
            var vCombos = new[] { "Combo Escolar", "Combo Fiesta", "Combo Gamer", "Combo Cine", "Combo Oficina", "Combo Viaje", "Combo Dulcero", "Combo Picante", "Combo Kids", "Combo Premium", "Combo Mix", "Combo Para 2", "Combo Familiar", "Combo Ahorro", "Combo Sorpresa" };

            Llenar(bebidas, "Bebida", vBebidas, total: 18, basePrecio: 14.0, paso: 1.0);
            Llenar(snacks, "Snack", vSnacks, total: 18, basePrecio: 16.0, paso: 1.0);
            Llenar(dulces, "Dulce", vDulces, total: 24, basePrecio: 6.0, paso: 0.8);
            Llenar(chocolates, "Chocolate", vChoco, total: 18, basePrecio: 15.0, paso: 1.2);
            Llenar(enchilados, "Enchilado", vEnchi, total: 15, basePrecio: 10.0, paso: 1.0);
            Llenar(gomitas, "Gomitas", vGomitas, total: 20, basePrecio: 12.0, paso: 0.9);
            Llenar(galletas, "Galletas", vGalletas, total: 16, basePrecio: 12.0, paso: 0.8);
            Llenar(chicles, "Chicle", vChicles, total: 12, basePrecio: 2.5, paso: 0.5);
            Llenar(importados, "Dulce Importado", vImport, total: 20, basePrecio: 22.0, paso: 1.5);
            Llenar(panPastelitos, "Pan", vPan, total: 12, basePrecio: 14.0, paso: 1.0);
            Llenar(reposteria, "Postre", vRepost, total: 12, basePrecio: 18.0, paso: 1.3);
            Llenar(combosOfertas, "Pack", vCombos, total: 15, basePrecio: 49.0, paso: 3.0);

            Categorias.AddRange(new[]
            {
                bebidas, snacks, dulces, chocolates, enchilados, gomitas,
                galletas, chicles, importados, panPastelitos, reposteria, combosOfertas
            });
        }
    }
}