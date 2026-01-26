namespace DaiPhuocBE.Contants.Cache
{
    public static class CacheExpirations
    {
        // Danh mục ít thay đổi nên là thời gian hết hạn sẽ dài hơn mấy cái khác
        public static readonly TimeSpan DanhMucExpiried = TimeSpan.FromDays(365);

        // Thông tin user đăng nhập
        public static readonly TimeSpan UserExpiried = TimeSpan.FromDays(365);
    }
}
