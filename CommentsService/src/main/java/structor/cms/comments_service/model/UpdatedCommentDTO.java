package structor.cms.comments_service.model;

import java.time.OffsetDateTime;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class UpdatedCommentDTO {
    private Integer rating;
    private OffsetDateTime updatedDate = OffsetDateTime.now();
    private String text;

}
