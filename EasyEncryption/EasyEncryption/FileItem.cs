using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEncryption
{
    class FileItem
    {
        public string EncKey { get; set; }
        public string IV { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public string Size { get; set; }
        public long longSize { get; set; }
        public string Owner { get; set; }
        public string Group { get; set; }
        public string path { get; set; }

        public bool Downloaded { get; set; }

        public FileItem()
        {
        }
    }
}
