using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEncryption
{
    class FileItem
    {
        public string hashedFilename { get; set; }
        public string EncKey { get; set; }
        public string IV { get; set; }
        public string Originalfilename { get; set; }
        public string OriginalfileExt { get; set; }
        public long Size { get; set; }
        public string owner { get; set; }
        public string shared { get; set; }
        public string path { get; set; }

        public FileItem()
        {
        }
    }
}
