package structor.cms.comments_service.service;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collector;
import java.util.stream.Collectors;

import org.hibernate.validator.constraints.UUID;
import org.springframework.data.domain.Example;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import structor.cms.comments_service.domain.Comment;
import structor.cms.comments_service.model.CommentDTO;
import structor.cms.comments_service.model.FilterDTO;
import structor.cms.comments_service.model.NewCommentDTO;
import structor.cms.comments_service.model.UpdatedCommentDTO;
import structor.cms.comments_service.repos.CommentRepository;
import structor.cms.comments_service.repos.CommentsSpecification;
import structor.cms.comments_service.util.Exceptions.NotFoundException;
import structor.cms.comments_service.util.Exceptions.ReferencedWarning;

@Service
public class CommentService {

    private final CommentRepository commentRepository;

    public CommentService(final CommentRepository commentRepository) {
        this.commentRepository = commentRepository;
    }

    public Page<CommentDTO> findAll(FilterDTO filterDTO, Pageable pageable) {
        var spec = CommentsSpecification.filterBy(filterDTO);

        var comments = commentRepository.findAll(spec, pageable);

        var res = comments.map(comment -> mapToDTO(comment, new CommentDTO()));

        return res;
    }

    public Page<CommentDTO> findAll(Pageable pageable) {
        final var comments = commentRepository.findAll(pageable);

        var res = comments.map(comment -> mapToDTO(comment, new CommentDTO()));

        return res;
    }

    public Page<CommentDTO> findAllByParentId(final Integer parentId, final Pageable pageable) {
        final var comments = commentRepository.findAllByParentId(parentId, pageable);

        var res = comments.map(comment -> mapToDTO(comment, new CommentDTO()));

        return res;
    }

    public Page<CommentDTO> findAllByPostId(final Integer postId, final Pageable pageable) {
        final var comments = commentRepository.findAllByPostId(postId, pageable);

        var res = comments.map(comment -> mapToDTO(comment, new CommentDTO()));

        return res;
    }

    public Page<CommentDTO> findAllByAuthorId(final Integer authorId, final Pageable pageable) {
        final var comments = commentRepository.findAllByAuthorId(authorId, pageable);

        var res = comments.map(comment -> mapToDTO(comment, new CommentDTO()));

        return res;
    }

    public Page<CommentDTO> findAllByAuthorIdAndPostId(final Integer authorId, final Integer postId,
            final Pageable pageable) {
        final var comments = commentRepository.findAllByAuthorIdAndPostId(authorId, postId, pageable);

        var res = comments.map(comment -> mapToDTO(comment, new CommentDTO()));

        return res;
    }

    public CommentDTO get(final Integer id) {
        return commentRepository.findById(id)
                .map(comment -> mapToDTO(comment, new CommentDTO()))
                .orElseThrow(NotFoundException::new);
    }

    public Integer create(final NewCommentDTO commentDTO) {
        final Comment comment = new Comment();
        mapToEntity(commentDTO, comment);
        return commentRepository.save(comment).getId();
    }

    public void update(final Integer id, final UpdatedCommentDTO commentDTO) {
        final Comment comment = commentRepository.findById(id)
                .orElseThrow(NotFoundException::new);
        mapToEntity(commentDTO, comment);
        commentRepository.save(comment);
    }

    @Transactional
    public void delete(final Integer id) {
        var comment = commentRepository.findById(id);

        if (comment.isPresent()) {

            var children = commentRepository.findAllByParentId(id, Pageable.unpaged());
            for (var child : children) {
                child.setParent(null);
            }
            commentRepository.deleteById(id);
        }

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
        commentDTO.setParentId(comment.getParent() == null ? null : comment.getParent().getId());

        var children = commentRepository.findAllByParentId(comment.getId(),
                Pageable.unpaged()).getContent();

        var dtoChildren = children.stream()
                .map(child -> mapToDTO(child, new CommentDTO()))
                .collect(Collectors.toList());
        commentDTO.setChildren(dtoChildren);

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
        comment.setParentId(commentDTO.getParentId());
        final Comment parent = commentDTO.getParentId() == null ? null
                : commentRepository.findById(commentDTO.getParentId())
                        .orElseThrow(() -> new NotFoundException("parent not found"));
        comment.setParent(parent);
        return comment;
    }

    private Comment mapToEntity(final NewCommentDTO commentDTO, final Comment comment) {
        comment.setCreatedBy(commentDTO.getAuthor());
        comment.setText(commentDTO.getText());
        comment.setRating(commentDTO.getRating());
        comment.setAuthor(commentDTO.getAuthor());
        comment.setAuthorId(commentDTO.getAuthorId());
        comment.setPostId(commentDTO.getPostId());
        comment.setParentId(commentDTO.getParent());

        final Comment parent = commentDTO.getParent() == null ? null
                : commentRepository.findById(commentDTO.getParent())
                        .orElseThrow(() -> new NotFoundException("parent not found"));
        comment.setParent(parent);
        return comment;
    }

    private Comment mapToEntity(final UpdatedCommentDTO commentDTO, final Comment comment) {
        comment.setText(commentDTO.getText());
        comment.setRating(commentDTO.getRating());
        comment.setUpdatedBy(comment.getAuthor());

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
