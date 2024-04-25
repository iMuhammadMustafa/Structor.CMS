package structor.cms.comments_service.model;

import java.util.Optional;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class FilterDTO {
    private Optional<Integer> postId;
    private Optional<Integer> authorId;
    private Optional<Integer> parentId;
}
