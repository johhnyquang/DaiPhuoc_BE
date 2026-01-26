
using DaiPhuocBE.DependencyInjection.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;

namespace DaiPhuocBE.DependencyInjection.Installer.SystemInstaller
{
    public class JwtInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configurations)
        {
            services.Configure<JwtSettings>(configurations.GetSection(nameof(JwtSettings)));
            var jwtConfig = configurations.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

            if (jwtConfig == null)
            {
                throw new InvalidOperationException($"Configuration section {nameof(JwtSettings)} is missing");
            }

            if (string.IsNullOrWhiteSpace(jwtConfig.SerectKey))
            {
                throw new InvalidOperationException($"JWT Secret cannot be empty");
            }

            if (jwtConfig.SerectKey.Length < 32)
            {
                throw new InvalidOperationException("JWT SecretKey must be at least 32 characters long for HS256");
            }

            if (string.IsNullOrWhiteSpace(jwtConfig.Issuer))
            {
                throw new InvalidOperationException("JWT Issuer cannot be empty");
            }

            var key = Encoding.UTF8.GetBytes(jwtConfig.SerectKey);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
            {
                // Lưu token sau khi validate thành công
                options.SaveToken = true;
                // Https metadata endpoint => Production
                options.RequireHttpsMetadata = true;
                // Token validation parameter
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    // Validate người phát hành token
                    ValidateIssuer = jwtConfig.ValidateIssuer,
                    ValidIssuer = jwtConfig.Issuer,

                    ValidateAudience = false,

                    // Validate token còn hạn không
                    ValidateLifetime = jwtConfig.ValidateLifetime,

                    // Validate chữ ký
                    ValidateIssuerSigningKey = jwtConfig.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    // Loại bỏ clock skew (mặc định 5 phút)
                    // Token hết hạn CHÍNH XÁC vào thời điểm chỉ định
                    ClockSkew = TimeSpan.Zero,
                };

                options.Events = new JwtBearerEvents
                {
                    // Xử lý khi authentication failed
                    OnAuthenticationFailed = context =>
                    {
                        // Nếu token hết hạn, thêm custom header
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Append("Token-Expired", "true");
                            // Client nhận được response:
                            // Status: 401
                            // Headers: Token-Expired: true
                            // → Client tự động gọi /api/auth/refresh-token
                        }
                        else if (context.Exception.GetType() == typeof(SecurityTokenSignatureKeyNotFoundException))
                        {
                            // Chữ ký sai -> token giả mạo
                            context.Response.Headers.Append("Token-Invalid", "signature-mismatch");

                            var logErrorSignature = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtInstaller>>();
                            logErrorSignature.LogError(context.Exception, "SECURITY: Invalid token signature from IP: {IP} and PORT: {port}", context.HttpContext.Connection.RemoteIpAddress, context.HttpContext.Connection.RemotePort);
                        }
                        else if (context.Exception.GetType() == typeof(SecurityTokenInvalidIssuerException))
                        {
                            // Issuer không đúng -> token từ hệ thống khác
                            context.Response.Headers.Append("Issuer-Token-Invalid", "wrong-issuer");
                        }

                        // Log error
                        var logError = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtInstaller>>();
                        logError.LogWarning(context.Exception, "Authentication failed for {Path} from {IP}. Reason: {Reason}", context.Request.Path, context.HttpContext.Connection.RemoteIpAddress, context.Exception.GetType().Name);

                        return Task.CompletedTask;
                    },

                    // Token đã validate thành công
                    OnTokenValidated = context =>
                    {
                        // Log thành công
                        var userId = context.Principal?.FindFirst("sub")?.Value;
                        var logSuccess = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtInstaller>>();
                        logSuccess.LogInformation("User {UserId} authenticated successfully", userId);

                        return Task.CompletedTask;
                    },

                    // Khi gửi challenge response (401)
                    OnChallenge = context =>
                    {
                        // Custom 401 response
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        string errorCode = string.Empty, errorMessage = string.Empty;

                        if (context.Response.Headers.ContainsKey("Token-Expired"))
                        {
                            errorCode = "TOKEN-EXPIRED";
                            errorMessage = "Your session has expired. Please login again.";
                        }
                        else if (context.Response.Headers.ContainsKey("Token-Invalid"))
                        {
                            errorCode = "TOKEN-INVALID";
                            errorMessage = "Invalid authentication token."; // lỗi này thường sẽ là lỗi ký sai signature
                        }
                        else if (context.Response.Headers.ContainsKey("Issuer-Token-Invalid"))
                        {
                            errorCode = "ISSUER-TOKEN-INVALID";
                            errorMessage = "Issuer Invalid authentication token."; // lỗi này là do token từ 1 nguồn khách
                        }
                        else
                        {
                            errorCode = "AUTHENTICATION_REQUIRED";
                            errorMessage = $"Authentication is required to access this resource. {context.Error}";
                        }

                        var detailError = new
                        {
                            success = false,
                            errorCode = errorCode,
                            errorMessage = errorMessage,
                            timestamp = DateTime.UtcNow,
                            path = context.Request.Path.Value
                        };

                        var result = System.Text.Json.JsonSerializer.Serialize(detailError);

                        return context.Response.WriteAsync(result);

                        // Client nhận được:
                        // {
                        //   "success": false    
                        //   "errorCode": "TOKEN-EXPIRED",
                        //   "errorMessage": "Your session has expired. Please login again.",
                        //   "timestamp": "2025-01-21T10:30:00Z",
                        //   "path": "/api/users"
                        // }
                    }
                };
            });

            //services.AddAuthorization(options =>
            //{
            //    options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //    options.AddPolicy("AdminPolicy", policy =>
            //           policy.RequireRole("Admin"));
            //});

            services.AddAuthorization();
            services.AddAuthorization();
        }
    }
}
