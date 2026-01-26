namespace DaiPhuocBE.DependencyInjection.Options
{
    public class RedisSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string InstanceName { get; set; } = string.Empty;
        public int DefaultExpirationMinutes { get; set; } = 60; // thời gian data tồn tại trong cache (tính bằng phút) hết thời gian này thì redis sẽ tự động xóa key
        public bool Enabled { get; set; } = true; 
        public bool EnviromentEnabled { get; set; } = false; // false là dev, true là prod
    }
}
