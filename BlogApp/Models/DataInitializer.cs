using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            // kategori
            var kategori = new List<Kategori>()
            {
                new Kategori(){KategoriAdi = "Kategori 1"}
            };

            foreach (var item in kategori)
            {
                context.Kategoriler.Add(item);
            }

            context.SaveChanges();

            // blog
            var blog = new List<Blog>()
            {
                new Blog()
                {
                    Baslik = "Başlık 1",
                    Aciklama = "Kategori 1 Hakkında",
                    EklenmeTarihi = DateTime.Now,
                    Goruntulenme=0,
                    Anasayfa = true,
                    Onay = true,
                    Icerik = "Kategori 1 Hakkında Yaptıklarım",
                    Resim = "user.png",
                    KategoriId = 1,
                    KullaniciAdi = "Mertcan"
                }
            };

            foreach (var item in blog)
            {
                context.Bloglar.Add(item);
            }

            context.SaveChanges();

            base.Seed(context); 
        }
    }
}