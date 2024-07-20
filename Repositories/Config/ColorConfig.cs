using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{
    public class ColorConfig : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(c => c.ColorId);
            builder.Property(c => c.ColorName).IsRequired();

            builder.HasData(
                new Color() { ColorId = 1, ColorName = "Red" },
                new Color() { ColorId = 2, ColorName = "Blue" },
                new Color() { ColorId = 3, ColorName = "Green" },
                new Color() { ColorId = 4, ColorName = "Yellow" },
                new Color() { ColorId = 5, ColorName = "Purple" },
                new Color() { ColorId = 6, ColorName = "Orange" },
                new Color() { ColorId = 7, ColorName = "Pink" },
                new Color() { ColorId = 8, ColorName = "Brown" },
                new Color() { ColorId = 9, ColorName = "Black" },
                new Color() { ColorId = 10, ColorName = "White" },
                new Color() { ColorId = 11, ColorName = "Gray" },
                new Color() { ColorId = 12, ColorName = "Cyan" },
                new Color() { ColorId = 13, ColorName = "Magenta" },
                new Color() { ColorId = 14, ColorName = "Lime" },
                new Color() { ColorId = 15, ColorName = "Indigo" },
                new Color() { ColorId = 16, ColorName = "Violet" },
                new Color() { ColorId = 17, ColorName = "Maroon" },
                new Color() { ColorId = 18, ColorName = "Olive" },
                new Color() { ColorId = 19, ColorName = "Navy" },
                new Color() { ColorId = 20, ColorName = "Teal" }
             );
        }
    }

}
