package structor.cms.comments_service.integrations.rabbitmq.consumers;

import org.springframework.stereotype.Component;

import com.fasterxml.jackson.databind.ObjectMapper;

import structor.cms.comments_service.integrations.rabbitmq.messageContracts.PostCached;
import structor.cms.comments_service.integrations.rabbitmq.messageContracts.PostDeleted;
import structor.cms.comments_service.service.CommentService;

@Component
public class PostDeletedConsumer {

    private CommentService commentService;
    private ObjectMapper objectMapper;

    public PostDeletedConsumer(final CommentService commentService, ObjectMapper objectMapper) {
        this.commentService = commentService;
        this.objectMapper = objectMapper;

    }

    public void handleMessage(Object message) {

        var postDeletedMessage = objectMapper.convertValue(message, PostDeleted.class);
        System.out.println("----> Post deleted message recieved: " + postDeletedMessage);

        commentService.deleteByPostId(postDeletedMessage.id());
    }
}
