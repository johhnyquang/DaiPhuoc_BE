namespace DaiPhuocBE.DependencyInjection.Options
{
    public class DatabaseSettings
    {
        public string ConnectionStrings { get; set; } = string.Empty;
        public int CommandTimeout { get; set; } = 30;
        public bool EnableRetryOnFailure { get; set; } = true;
        public int MaxRetryTotal { get; set; } = 3;
        public int MaxDelayRetrySeconds { get; set; } = 10;// số giây đợi giữa các lần retry
    }
}
