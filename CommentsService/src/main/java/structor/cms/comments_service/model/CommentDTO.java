package structor.cms.comments_service.model;

import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import java.time.OffsetDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class CommentDTO {

    private Integer id;

    @NotNull
    private UUID guid;

    @Size(max = 255)
    private String createdBy;

    private OffsetDateTime createdDate;

    @Size(max = 255)
    private String updatedBy;

    private OffsetDateTime updatedDate;

    @NotNull
    @Size(max = 255)
    private String text;

    private Integer rating;

    @Size(max = 255)
    private String author;

    @NotNull
    private Integer authorId;

    @NotNull
    private Integer postId;

    private Integer parentId;

    private List<CommentDTO> children = new ArrayList<>();

}
