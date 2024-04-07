using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class ItemImage
    {
        public ItemImage()
        {
        }

        public ItemImage(Guid iD, string img)
        {
            ID = iD;
            Img = img;
        }

        public Guid ID { get; set; }
        public string Img { get; set; }
    }
}
  