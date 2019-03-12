using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PROG1442_Exercise4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG1442_Exercise4.Data
{
    public static class ArtSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new ArtContext(serviceProvider.GetRequiredService<DbContextOptions<ArtContext>>()))
            {
                if (!context.ArtTypes.Any())
                {
                    context.ArtTypes.AddRange(
                     new ArtType
                     {
                         Type = "Painting"
                     },
                     new ArtType
                     {
                         Type = "Drawing"
                     },
                     new ArtType
                     {
                         Type = "Sculpture"
                     },
                     new ArtType
                     {
                         Type = "Plastic Art"
                     },
                     new ArtType
                     {
                         Type = "Decorative Art"
                     },
                     new ArtType
                     {
                         Type = "Visual Art"
                     }
                   );
                    context.SaveChanges();
                }
                if (!context.Artworks.Any())
                {
                    context.Artworks.AddRange(
                     new Artwork
                     {
                         Name = "Red Dot",
                         Value = 12000m,
                         Finished = DateTime.Parse("2002-06-06"),
                         Description = "Painting of a large Red Dot on a white backgraound.",
                         ArtTypeID = (context.ArtTypes.Where(d => d.Type == "Painting").SingleOrDefault().ID)
                     },
                     new Artwork
                     {
                         Name = "Rossini Regal",
                         Value = 99000m,
                         Finished = DateTime.Parse("2009-12-06"),
                         Description = "Photograph of a regal horse.",
                         ArtTypeID = (context.ArtTypes.Where(d => d.Type == "Visual Art").SingleOrDefault().ID)
                     },
                     new Artwork
                     {
                         Name = "Love Sublime",
                         Value = 19.99m,
                         Finished = DateTime.Parse("2015-09-21"),
                         Description = "Soapstone Sculpture of woman's face gazing at an unknown figure.",
                         ArtTypeID = (context.ArtTypes.Where(d => d.Type == "Sculpture").SingleOrDefault().ID)
                     },
                     new Artwork
                     {
                         Name = "Igor Smashes",
                         Value = 750000.50m,
                         Finished = DateTime.Parse("1976-07-11"),
                         Description = "Abstract concept of smashed emotion done in crumpled paper.",
                         ArtTypeID = (context.ArtTypes.Where(d => d.Type == "Plastic Art").SingleOrDefault().ID)
                     });
                    context.SaveChanges();
                }
            }
        }
    }
}
