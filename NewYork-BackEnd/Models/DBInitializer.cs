using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NewYork_BackEnd.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NewYork_BackEnd.Models
{
    public class DBInitializer
    {
        public static void Initialize(FoosballContext context)
        {
            context.Database.EnsureCreated();

            // Look for any user.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            string password = "admin";

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            context.Users.AddRange(
                new User
                {
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@admin.be",
                    Password = hashed,
                    HashSalt = salt,
                    DateOfBirth = DateTime.Now,
                    Role = "admin",
                    TeamID = null
                });

            /*context.Tags.AddRange(
                new Tag { Name = "Sport" },
                new Tag { Name = "Film" },
                new Tag { Name = "Reizen" },
                new Tag { Name = "Games" }
                );

            context.ArticleStatuses.AddRange(
                new ArticleStatus { Name = "Draft" },
                new ArticleStatus { Name = "To review" },
                new ArticleStatus { Name = "Published" }
                );

            context.Articles.AddRange(
                new Article { UserID = 1, Title = "Messi verlaat FC Barçelona", SubTitle = "Messi stuurde een fax met de boodschap dat hij wilt vertrekken.", ArticleStatusID = 1, TagID = 1, Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus consequat non justo dignissim varius. Morbi finibus magna non neque bibendum efficitur. Aliquam eu auctor sem, ut mollis erat. Donec ornare dolor ex, tincidunt blandit purus sodales id. Phasellus a hendrerit libero. Nunc eu ultrices libero. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Integer consequat egestas dui sit amet dignissim. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. In sit amet cursus elit, eu dignissim elit. Ut aliquam cursus urna ultricies rhoncus. Proin vitae neque erat. Sed mollis consectetur diam eget vestibulum." }
                );*/

            context.SaveChanges();
        }
    }
}
