package structor.cms.comments_service.repos;

import java.util.Optional;

import org.springframework.data.jpa.domain.Specification;

import structor.cms.comments_service.domain.Comment;
import structor.cms.comments_service.model.FilterDTO;

public class CommentsSpecification {

    public static final String AuthorId = "authorId";
    public static final String PostId = "postId";
    public static final String ParentId = "parentId";

    public static Specification<Comment> filterBy(FilterDTO filterDTO) {
        return Specification
                .where(postIdIs(filterDTO.getPostId()))
                .and(authorIdIs(filterDTO.getAuthorId()))
                .and(parentIdIs(filterDTO.getParentId()));
    }

    private static Specification<Comment> authorIdIs(Optional<Integer> authorId) {
        return ((root, query, cb) -> authorId == null || authorId.isEmpty() ? cb.conjunction()
                : cb.equal(root.get(AuthorId), authorId.get()));
    }

    private static Specification<Comment> postIdIs(Optional<Integer> postId) {
        return ((root, query, cb) -> postId == null || postId.isEmpty() ? cb.conjunction()
                : cb.equal(root.get(PostId), postId.get()));
    }

    private static Specification<Comment> parentIdIs(Optional<Integer> parentId) {
        return ((root, query, cb) -> parentId == null || parentId.isEmpty() ? cb.conjunction()
                : cb.equal(root.get(ParentId), parentId.get()));
    }
}
