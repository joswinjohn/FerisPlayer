
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerisPlayer
{
    public class Functions
    {
        public static void PopulateListBox(ListBox lsb, string folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(folder);
            FileInfo[] Files = dinfo.GetFiles(FileType);
            foreach (FileInfo file in Files)
            {
                lsb.Items.Add(file.Name);
            }
        }

    }
}