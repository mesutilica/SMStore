namespace SMStore.WebUIAPIUsing.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile, string filePath = "/wwwroot/Img/")
        {
            string fileName = ""; // yüklenecek dosya adı için değişken oluşturduk

            fileName = formFile.FileName; // oluşturduğumuz değişkene yüklenecek dosya adını aktardık

            string directory = Directory.GetCurrentDirectory() + filePath + fileName; // dosyanın yükleneceği dizini belirledik (GetCurrentDirectory metodu uygulamanın çalıştığı fiziksel yolu getirir)

            using var stream = new FileStream(directory, FileMode.Create); // dosya yükleme için gerekli bir dosya akış nesnesi oluşturup sınıfa yükleme yapacağımız dizini(directory) ve yükleme tipimizi(yeni dosya oluşturma) belirttik

            await formFile.CopyToAsync(stream); // yukardaki ayarlarla dosyamızı asenkron bir şekilde sunucuya yükledik

            return fileName; // bu metodun kullanılacağı yere yüklenen dosya adını geri gönderdik
        }
        public static bool FileRemover(string fileName, string filePath = "/wwwroot/Img/")
        {
            string directory = Directory.GetCurrentDirectory() + filePath + fileName;

            if (File.Exists(directory)) // File.Exists metodu c# ta var olan ve kendisine verilen adresteki dosya var mı yok mu kontrol eden metottur.
            {
                File.Delete(directory); // File.Delete metodu verilen adresteki dosyayı sunucudan siler
                return true;
            }

            return false;
        }
    }
}
