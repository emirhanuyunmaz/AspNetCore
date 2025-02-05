using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app){
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
            if(context != null){
                // Eğer uygulanmamış bir migrations varsa uygulaması için.
                if(context.Database.GetPendingMigrations().Any()){
                    context.Database.Migrate();
                }

                if(!context.Tags.Any()){
                    context.Tags.AddRange(
                        new Tag {Text="Web Programlama" ,Url="web-programlama",TagColor=TagColor.info},
                        new Tag {Text="Backend" ,Url="backend",TagColor=TagColor.warning},
                        new Tag {Text="Frontend" ,Url="frontend",TagColor=TagColor.success},
                        new Tag {Text="Fullstack" ,Url="fullstack",TagColor=TagColor.secondary,},
                        new Tag {Text="Php" ,Url = "php",TagColor=TagColor.primary}
                    );
                    context.SaveChanges();
                }

                if(!context.Users.Any()){
                    context.Users.AddRange(
                        new User{ Name = "emirhan",Email="emirhan@gmail.com",Password="123456789",Image="10.jpg"},
                        new User{ Name = "ahmet",Email="ahmet@gmail.com",Password="123456789",Image="11.jpg"},
                        new User{ Name = "ali",Email="ali@gmail.com",Password="123456789",Image="12.jpg"}
                    );
                    context.SaveChanges();
                }

                if(!context.Posts.Any()){
                    context.Posts.AddRange(
                        new Post{
                            Title="Asp.net core",
                            Url="asp-net-core",
                            Description ="", 
                            Content = "Asp.net core dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags=context.Tags.Take(3).ToList(),
                            Image="1.jpg",
                            UserId=1,
                            Comments = new List<Comment>{
                                new Comment { Text="Çok faydalandığım bir kurs", PublishedOn=DateTime.Now.AddDays(-10),UserId = 2},
                                new Comment { Text="İyi bir kurs", PublishedOn=DateTime.Now.AddDays(-20),UserId = 1},
                            }
                        },
                        new Post{
                            Title="Nodejs",
                            Url="nodejs",
                            Description ="",
                            Content = "Nodejs dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-20),
                            Tags=context.Tags.Take(2).ToList(),
                            Image="2.jpg",
                            UserId=2
                        },
                        new Post{
                            Title="Php",
                            Url="php",
                            Description ="",
                            Content = "Php dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-12),
                            Tags=context.Tags.Take(1).ToList(),
                            Image="3.jpg",
                            UserId=1
                        },
                        new Post{
                            Title="Django",
                            Url="django",
                            Description ="",                            
                            Content = "Django dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Tags=context.Tags.Take(1).ToList(),
                            Image="4.jpg",
                            UserId=2
                        }
                    );
                    context.SaveChanges();
                }

            }
        }
    }
}