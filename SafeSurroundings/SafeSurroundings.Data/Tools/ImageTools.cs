using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSurroundings.Data.Tools
{
    public static class ImageTools
    {
        public static string GetImageScrFromBytes(Byte[] ProfileImageBytes)
        {
            string imageBase64 = Convert.ToBase64String(ProfileImageBytes);
            return ProfileImageBytes != null && ProfileImageBytes.Length > 0 ? $"data:image/png;base64,{imageBase64}" : string.Empty;
        }

        public static Byte[] GetImageBytes()
        {
            try
            {
                Image profiletest = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Images//cat.png");
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    profiletest.Save(memoryStream, profiletest.RawFormat);
                    return memoryStream.ToArray();
                }
            }
            catch
            {
                return new byte[] { };
            }
        }


    }
}
