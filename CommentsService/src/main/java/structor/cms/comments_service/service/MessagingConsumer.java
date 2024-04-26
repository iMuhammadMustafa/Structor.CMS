package structor.cms.comments_service.service;

import org.springframework.stereotype.Component;

import com.fasterxml.jackson.databind.ObjectMapper;

import structor.cms.comments_service.integrations.messagingContracts.PostDeletedMessage;

@Component
public class MessagingConsumer {

    private CommentService commentService;
    private ObjectMapper objectMapper;

    public MessagingConsumer(final CommentService commentService, ObjectMapper objectMapper) {
        this.commentService = commentService;
        this.objectMapper = objectMapper;

    }

    public void postDeletedMessageRecieved(Object message) {

        var postDeletedMessage = objectMapper.convertValue(message, PostDeletedMessage.class);
        System.out.println("----> Post deleted message recieved: " + postDeletedMessage);

        commentService.deleteByPostId(postDeletedMessage.id());
    }
}
