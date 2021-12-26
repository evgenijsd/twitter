using InterTwitter.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InterTwitter.Helpers
{
    public static class VcfFileHelper
    {
        private const string NewLine = "\r\n";
        private const string Separator = ";";
        private const string Header = "BEGIN:VCARD\r\nVERSION:2.1";
        private const string Name = "N:";
        private const string FormattedName = "FN:";
        private const string OrganizationName = "ORG:";
        private const string TitlePrefix = "TITLE:";
        private const string PhotoPrefix = "PHOTO;TYPE=JPEG;ENCODING=BASE64:";
        private const string PhonePrefix = "TEL;TYPE=";
        private const string PhoneSubPrefix = ",VOICE:";
        private const string AddressPrefix = "ADR;TYPE=";
        private const string AddressSubPrefix = ":;;";
        private const string EmailPrefix = "EMAIL:";
        private const string Footer = "END:VCARD";

        public static async Task<string> CreateFileAsync(VcfUser user)
        {
            StringBuilder fw = new StringBuilder();
            fw.Append(Header);
            fw.Append(NewLine);

            //Full Name
            if (!string.IsNullOrEmpty(user.Name) || !string.IsNullOrEmpty(user.Email))
            {
                fw.Append(Name);
                fw.Append(user.Name);
                fw.Append(Separator);
                fw.Append(NewLine);
            }

            //Photo
            if (!string.IsNullOrEmpty(user.Avatar))
            {
                fw.Append(PhotoPrefix);

                byte[] imageArray = File.ReadAllBytes(user.Avatar);
                string base64Representation = Convert.ToBase64String(imageArray);

                fw.Append(base64Representation);
                fw.Append(NewLine);
            }

            //Email
            if (!string.IsNullOrEmpty(user.Email))
            {
                fw.Append(EmailPrefix);
                fw.Append(user.Email);
                fw.Append(NewLine);
            }

            fw.Append(Footer);

            var file = Path.Combine(FileSystem.CacheDirectory, $"{user.Name}.vcf");
            File.WriteAllText(file, fw.ToString());
            return file;
        }
    }
}