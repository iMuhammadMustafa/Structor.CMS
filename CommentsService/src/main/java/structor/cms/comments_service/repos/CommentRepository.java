package structor.cms.comments_service.repos;

import java.util.List;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import io.micrometer.common.lang.Nullable;
import structor.cms.comments_service.domain.Comment;

@Repository
public interface CommentRepository extends JpaRepository<Comment, Integer> {

    Page<Comment> findAll(@Nullable Specification<Comment> spec, Pageable pageable);

    // Page<Comment>
    // findAllByPostIfNotNullIdAndAuthorIdIfNotNullAndParentIdIfNotNull(final
    // Integer postId,
    // final Integer authorId, final Integer parentId, final Pageable pageable);

    Page<Comment> findAllByParentId(final Integer parentId, final Pageable pageable);

    Page<Comment> findAllByPostId(final Integer postId, final Pageable pageable);

    List<Comment> findAllByPostIdIn(final List<Integer> postsIds);

    Page<Comment> findAllByAuthorId(final Integer authorId, final Pageable pageable);

    Page<Comment> findAllByAuthorIdAndPostId(final Integer authorId, final Integer postId, final Pageable pageable);

    Comment findFirstByParentAndIdNot(Comment comment, final Integer id);

    void deleteAllByPostId(Integer postId);
}
