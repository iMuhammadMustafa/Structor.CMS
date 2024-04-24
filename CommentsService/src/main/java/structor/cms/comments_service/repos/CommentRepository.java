package structor.cms.comments_service.repos;

import org.springframework.data.jpa.repository.JpaRepository;

import structor.cms.comments_service.domain.Comment;

public interface CommentRepository extends JpaRepository<Comment, Integer> {

    Comment findFirstByParentAndIdNot(Comment comment, final Integer id);

}
