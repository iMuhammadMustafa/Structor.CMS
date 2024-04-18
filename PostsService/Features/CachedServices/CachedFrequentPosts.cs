using Microsoft.Extensions.Caching.Distributed;
using PostsService.Features.Dtos;
using System.Text.Json;

namespace PostsService.Features.CachedServices;

public class CachedFrequentPosts : ICachedFrequentPosts
{
    private readonly IDistributedCache _cache;
    private readonly IConfiguration _configuration;

    private const string KEY_NAME = "Redis:Keys:Posts:Frequent";
    private readonly string _frequentPostsKey;
    public CachedFrequentPosts(IDistributedCache cache,
                               IConfiguration configuration)
    {
        _cache = cache;
        _configuration = configuration;

        _frequentPostsKey = _configuration[KEY_NAME] ?? throw new Exception("Cache Key Name Not found in Configuration");
    }

    public async Task<IEnumerable<PostDto>?> GetCachedPosts()
    {
        var posts = await _cache.GetStringAsync(_frequentPostsKey);

        if (posts is null) return null;

        var data = JsonSerializer.Deserialize<IEnumerable<PostDto>>(posts);

        return data;
    }


    public async Task<bool> SetCachedPosts(IEnumerable<PostDto> posts)
    {
        var data = JsonSerializer.Serialize(posts);

        if (string.IsNullOrWhiteSpace(data)) return false;

        await _cache.SetStringAsync(_frequentPostsKey, data);

        return true;
    }
}
