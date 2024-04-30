package structor.cms.comments_service.integrations.rabbitmq.consumers;

import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

import org.redisson.RedissonClient;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.stereotype.Component;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;

import structor.cms.comments_service.domain.Comment;
import structor.cms.comments_service.integrations.rabbitmq.messageContracts.PostCached;
import structor.cms.comments_service.service.CommentService;

@Component
public class PostCachedConsumer {

    private CommentService commentService;
    private ObjectMapper objectMapper;
    private RedisTemplate<String, List<String>> redisStringListTemplate;

    public PostCachedConsumer(final CommentService commentService, ObjectMapper objectMapper,
            RedisTemplate<String, List<String>> redisStringListTemplate) {
        this.commentService = commentService;
        this.objectMapper = objectMapper;
        this.redisStringListTemplate = redisStringListTemplate;

    }

    public void handleMessage(List<Object> message) {

        var postsCached = objectMapper.convertValue(message, new TypeReference<List<PostCached>>() {
        });

        // List<PostCached> postsCached = message.stream()
        // .map(o -> objectMapper.convertValue(o, PostCached.class))
        // .collect(Collectors.toList());

        System.out.println("----> Post Cached message recieved: " + postsCached);

        List<Integer> postsIds = postsCached.stream().map(post -> post.id()).collect(Collectors.toList());
        var comments = commentService.findAllByPostsId(postsIds);

        var serializedComments = comments.stream().map(comment -> {
            try {
                return objectMapper.writeValueAsString(comment);
            } catch (Exception e) {
                e.printStackTrace();
                return "";
            }
        }).collect(Collectors.toList());

        redisStringListTemplate.opsForValue().set("cachedComments", serializedComments);

        var cachedComments = redisStringListTemplate.opsForValue().get("cachedComments");

        var deSerializedComments = cachedComments.stream().map(comment -> {
            try {
                return objectMapper.readValue(comment, Comment.class);
            } catch (Exception e) {
                e.printStackTrace();
                return null;
            }
        }).collect(Collectors.toList());

        System.out.println("----> Cached comments: " + deSerializedComments);
    }
}
