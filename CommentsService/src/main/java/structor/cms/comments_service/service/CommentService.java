package structor.cms.comments_service.service;

import java.util.List;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;
import structor.cms.comments_service.domain.Comment;
import structor.cms.comments_service.model.CommentDTO;
import structor.cms.comments_service.repos.CommentRepository;
import structor.cms.comments_service.util.NotFoundException;
import structor.cms.comments_service.util.ReferencedWarning;

@Service
public class CommentService {

    private final CommentRepository commentRepository;

    public CommentService(final CommentRepository commentRepository) {
        this.commentRepository = commentRepository;
    }

    public List<CommentDTO> findAll(Pageable pageable) {
        final var comments = commentRepository.findAll(pageable);
        return comments.stream()
                .map(comment -> mapToDTO(comment, new CommentDTO()))
                .toList();
    }

    public CommentDTO get(final Integer id) {
        return commentRepository.findById(id)
                .map(comment -> mapToDTO(comment, new CommentDTO()))
                .orElseThrow(NotFoundException::new);
    }

    public Integer create(final CommentDTO commentDTO) {
        final Comment comment = new Comment();
        mapToEntity(commentDTO, comment);
        return commentRepository.save(comment).getId();
    }

    public void update(final Integer id, final CommentDTO commentDTO) {
        final Comment comment = commentRepository.findById(id)
                .orElseThrow(NotFoundException::new);
        mapToEntity(commentDTO, comment);
        commentRepository.save(comment);
    }

    public void delete(final Integer id) {
        commentRepository.deleteById(id);
    }

    private CommentDTO mapToDTO(final Comment comment, final CommentDTO commentDTO) {
        commentDTO.setId(comment.getId());
        commentDTO.setGuid(comment.getGuid());
        commentDTO.setCreatedBy(comment.getCreatedBy());
        commentDTO.setCreatedDate(comment.getCreatedDate());
        commentDTO.setUpdatedBy(comment.getUpdatedBy());
        commentDTO.setUpdatedDate(comment.getUpdatedDate());
        commentDTO.setText(comment.getText());
        commentDTO.setRating(comment.getRating());
        commentDTO.setAuthor(comment.getAuthor());
        commentDTO.setAuthorId(comment.getAuthorId());
        commentDTO.setPostId(comment.getPostId());
        commentDTO.setParent(comment.getParent() == null ? null : comment.getParent().getId());
        return commentDTO;
    }

    private Comment mapToEntity(final CommentDTO commentDTO, final Comment comment) {
        comment.setGuid(commentDTO.getGuid());
        comment.setCreatedBy(commentDTO.getCreatedBy());
        comment.setCreatedDate(commentDTO.getCreatedDate());
        comment.setUpdatedBy(commentDTO.getUpdatedBy());
        comment.setUpdatedDate(commentDTO.getUpdatedDate());
        comment.setText(commentDTO.getText());
        comment.setRating(commentDTO.getRating());
        comment.setAuthor(commentDTO.getAuthor());
        comment.setAuthorId(commentDTO.getAuthorId());
        comment.setPostId(commentDTO.getPostId());
        final Comment parent = commentDTO.getParent() == null ? null
                : commentRepository.findById(commentDTO.getParent())
                        .orElseThrow(() -> new NotFoundException("parent not found"));
        comment.setParent(parent);
        return comment;
    }

    public ReferencedWarning getReferencedWarning(final Integer id) {
        final ReferencedWarning referencedWarning = new ReferencedWarning();
        final Comment comment = commentRepository.findById(id)
                .orElseThrow(NotFoundException::new);
        final Comment parentComment = commentRepository.findFirstByParentAndIdNot(comment, comment.getId());
        if (parentComment != null) {
            referencedWarning.setKey("comment.comment.parent.referenced");
            referencedWarning.addParam(parentComment.getId());
            return referencedWarning;
        }
        return null;
    }

}
