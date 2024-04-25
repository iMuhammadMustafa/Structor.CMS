package structor.cms.comments_service.model;

import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import java.time.OffsetDateTime;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class NewCommentDTO {
    private OffsetDateTime createdDate = OffsetDateTime.now();
    private OffsetDateTime updatedDate = OffsetDateTime.now();

    @NotNull
    @Size(max = 255)
    private String text;

    private Integer rating;

    @NotNull
    private Integer authorId;
    @NotNull
    private String author;
    private String createdBy = this.getAuthor();

    @NotNull
    private Integer postId;

    private Integer parent;

}
