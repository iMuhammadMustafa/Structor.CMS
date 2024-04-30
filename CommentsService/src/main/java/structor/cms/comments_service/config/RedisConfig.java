package structor.cms.comments_service.config;

import java.util.List;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.cache.annotation.EnableCaching;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.redis.connection.RedisStandaloneConfiguration;
import org.springframework.data.redis.connection.jedis.JedisConnectionFactory;
import org.springframework.data.redis.core.RedisTemplate;

import structor.cms.comments_service.domain.Comment;

@Configuration
@EnableCaching
public class RedisConfig {

    @Value("${spring.redis.host}")
    public static final String RedisHost = "localhost";
    @Value("${spring.redis.port}")
    public static final int RedisPort = 6379;

    @Bean
    JedisConnectionFactory jedisConnectionFactory() {
        RedisStandaloneConfiguration redisStandaloneConfiguration = new RedisStandaloneConfiguration(RedisHost,
                RedisPort);

        return new JedisConnectionFactory(redisStandaloneConfiguration);
    }

    @Bean
    public RedisTemplate<String, Object> redisTemplate() {
        RedisTemplate<String, Object> template = new RedisTemplate<>();
        template.setConnectionFactory(jedisConnectionFactory());
        return template;
    }

    @Bean
    public RedisTemplate<String, List<String>> redisStringListTemplate() {
        RedisTemplate<String, List<String>> template = new RedisTemplate<>();
        template.setConnectionFactory(jedisConnectionFactory());
        return template;
    }

    @Bean
    public RedisTemplate<String, List<Comment>> redisCommentTemplate() {
        RedisTemplate<String, List<Comment>> template = new RedisTemplate<>();
        template.setConnectionFactory(jedisConnectionFactory());
        return template;
    }

    // @Bean(destroyMethod = "shutdown")
    // RedissonClient redisson() {
    // Config config = new Config();
    // config.useSingleServer()
    // .setAddress("redis://" + RedisHost + ":" + RedisPort);
    // return Redisson.create(config);
    // }

    // @Bean
    // CacheManager cacheManager(RedissonClient redissonClient) {
    // Map<String, CacheConfig> config = new HashMap<>();

    // // create "testMap" spring cache with ttl = 24 minutes and maxIdleTime = 12
    // // minutes
    // config.put("testMap", new CacheConfig(24 * 60 * 1000, 12 * 60 * 1000));
    // return (CacheManager) new RedissonSpringCacheManager(redissonClient, config);
    // }

}
