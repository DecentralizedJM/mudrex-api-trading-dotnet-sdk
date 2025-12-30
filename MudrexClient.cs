using System.Text.Json;
using Mudrex.TradingSDK.Exceptions;
using Mudrex.TradingSDK.Models;

namespace Mudrex.TradingSDK;

/// <summary>
/// Main Mudrex API client
/// </summary>
public class MudrexClient : IAsyncDisposable
{
    private readonly string _apiSecret;
    private readonly string _baseUrl;
    private readonly TimeSpan _timeout;
    private readonly HttpClient _httpClient;
    private readonly RateLimiter _rateLimiter;

    public WalletApi Wallet { get; }
    public AssetsApi Assets { get; }
    public LeverageApi Leverage { get; }
    public OrdersApi Orders { get; }
    public PositionsApi Positions { get; }
    public FeesApi Fees { get; }

    /// <summary>
    /// Creates a new Mudrex API client
    /// </summary>
    public MudrexClient(string apiSecret)
        : this(apiSecret, "https://trade.mudrex.com/fapi/v1", TimeSpan.FromSeconds(30))
    {
    }

    /// <summary>
    /// Creates a new Mudrex API client with custom configuration
    /// </summary>
    public MudrexClient(string apiSecret, string baseUrl, TimeSpan timeout)
    {
        _apiSecret = apiSecret;
        _baseUrl = baseUrl;
        _timeout = timeout;
        _httpClient = new HttpClient { Timeout = timeout };
        _rateLimiter = new RateLimiter(2.0); // 2 requests per second

        // Initialize API modules
        Wallet = new WalletApi(this);
        Assets = new AssetsApi(this);
        Leverage = new LeverageApi(this);
        Orders = new OrdersApi(this);
        Positions = new PositionsApi(this);
        Fees = new FeesApi(this);
    }

    /// <summary>
    /// Performs a GET request
    /// </summary>
    public async Task<byte[]> GetAsync(string path)
    {
        return await DoRequestAsync("GET", path, null);
    }

    /// <summary>
    /// Performs a POST request
    /// </summary>
    public async Task<byte[]> PostAsync(string path, string? body)
    {
        return await DoRequestAsync("POST", path, body);
    }

    /// <summary>
    /// Performs a PATCH request
    /// </summary>
    public async Task<byte[]> PatchAsync(string path, string? body)
    {
        return await DoRequestAsync("PATCH", path, body);
    }

    /// <summary>
    /// Performs a DELETE request
    /// </summary>
    public async Task<byte[]> DeleteAsync(string path, string? body)
    {
        return await DoRequestAsync("DELETE", path, body);
    }

    /// <summary>
    /// Internal method to perform HTTP requests
    /// </summary>
    private async Task<byte[]> DoRequestAsync(string method, string path, string? body)
    {
        // Apply rate limiting
        await _rateLimiter.WaitAsync();

        var url = $"{_baseUrl}{path}";
        
        var requestMessage = new HttpRequestMessage
        {
            Method = new HttpMethod(method),
            RequestUri = new Uri(url)
        };

        // Set headers
        requestMessage.Headers.Add("X-Authentication", _apiSecret);
        requestMessage.Content = new StringContent(body ?? "", System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsByteArrayAsync();

            // Check for errors
            await CheckForErrorsAsync(response.StatusCode, content);

            return content;
        }
        catch (HttpRequestException ex)
        {
            throw new MudrexException($"Request failed: {ex.Message}", -1, -1);
        }
    }

    /// <summary>
    /// Checks HTTP response for errors and throws appropriate exceptions
    /// </summary>
    private Task CheckForErrorsAsync(System.Net.HttpStatusCode statusCode, byte[] body)
    {
        if ((int)statusCode < 400)
            return Task.CompletedTask;

        var bodyStr = System.Text.Encoding.UTF8.GetString(body);
        
        try
        {
            var response = JsonSerializer.Deserialize<ApiResponse<object>>(bodyStr);
            var message = response?.Message ?? bodyStr;
            var code = response?.Error?.Code ?? -1;

            var exception = (int)statusCode switch
            {
                401 => new AuthenticationException($"Authentication failed: {message}", code),
                429 => new RateLimitException($"Rate limit exceeded: {message}", code),
                400 when code == 1002 || message.Contains("insufficient balance") =>
                    new InsufficientBalanceException($"Insufficient balance: {message}", code),
                400 => new ValidationException($"Validation error: {message}", code),
                404 => new NotFoundException($"Not found: {message}", code),
                409 => new ConflictException($"Conflict: {message}", code),
                >= 500 => new ServerException($"Server error: {message}", code, (int)statusCode),
                _ => new MudrexException($"API error: {message}", code, (int)statusCode)
            };

            throw exception;
        }
        catch (JsonException)
        {
            throw new MudrexException($"Failed to parse error response: {bodyStr}", -1, (int)statusCode);
        }
    }

    public async ValueTask DisposeAsync()
    {
        _httpClient?.Dispose();
        await Task.CompletedTask;
    }
}

/// <summary>
/// Simple rate limiter implementation
/// </summary>
internal class RateLimiter
{
    private readonly double _minInterval;
    private long _lastRequestTime;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    internal RateLimiter(double requestsPerSecond)
    {
        _minInterval = 1000.0 / requestsPerSecond; // Convert to milliseconds
        _lastRequestTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    internal async Task WaitAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var elapsed = now - _lastRequestTime;

            if (elapsed < _minInterval)
            {
                var sleepTime = (long)(_minInterval - elapsed);
                await Task.Delay((int)sleepTime);
            }

            _lastRequestTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
