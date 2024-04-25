package structor.cms.comments_service.service;

import org.springframework.stereotype.Component;

@Component
public class MessagingConsumer {

    public Integer postDeletedMessageRecieved(Integer postId) {
        System.out.println("Post deleted message recieved: " + postId);
        return postId;
    }
}
