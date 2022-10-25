namespace SMStore.WebUIAPIUsing.Models
{
    public class AdminLoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
// Class larda data annotation yerine fluent validation ile veri doğrulaması yapabiliriz
// Bunun için nuget pm dan fluentvalidation asp.net core paketini projemize yüklemeliyiz
// 