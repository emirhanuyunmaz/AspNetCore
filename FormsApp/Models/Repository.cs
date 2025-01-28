namespace FormsApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();

        static Repository(){
            _categories.Add(new Category(){CategoryId = 1, Name = "Telefon"});
            _categories.Add(new Category(){CategoryId = 2, Name = "Bilgisayar"});
        
            _products.Add(new Product(){ProductId =1,CategoryId = 1 , Fiyat = 40000 , Name = "Iphone 14",IsActive = true,Image ="1.jpg" });
            _products.Add(new Product(){ProductId =3,CategoryId = 1 , Fiyat = 45000 , Name = "Iphone 15",IsActive = true,Image ="2.jpg" });
            _products.Add(new Product(){ProductId =4,CategoryId = 1 , Fiyat = 50000 , Name = "Iphone 16",IsActive = true,Image ="3.jpg" });
            _products.Add(new Product(){ProductId =2,CategoryId = 1 , Fiyat = 55000 , Name = "Iphone 16 +",IsActive = true,Image ="4.jpg" });

            _products.Add(new Product(){ProductId =5,CategoryId = 2 , Fiyat = 55000 , Name = "Macbook Air ",IsActive = true,Image ="5.jpg" });
            _products.Add(new Product(){ProductId =6,CategoryId = 2 , Fiyat = 60000 , Name = "Macbook Pro",IsActive = true,Image ="6.jpg" });
        }

        public static List<Product> Products{
            get{
                return _products;
            }
        }

        public static void EditProduct(Product updateProduct){
            var entity = _products.FirstOrDefault(p => p.ProductId == updateProduct.ProductId);
            if(entity != null ){
                entity.Name = updateProduct.Name;
                entity.Fiyat = updateProduct.Fiyat;
                entity.Image = updateProduct.Image;
                entity.CategoryId = updateProduct.CategoryId;
            }
        }

        public static void DeleteProduct(Product deleteProduct){
            var entity = _products.FirstOrDefault(p => p.ProductId == deleteProduct.ProductId);
            if(entity != null){
                _products.Remove(entity);
            }
        }

        public static void CreateProduct(Product entity){
            _products.Add(entity);
        }
        
        public static List<Category> Categories{
            get{
                return _categories;
            }
        }
    }
}