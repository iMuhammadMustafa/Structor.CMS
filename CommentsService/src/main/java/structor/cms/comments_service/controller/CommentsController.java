package structor.cms.comments_service.controller;

import io.swagger.v3.oas.annotations.responses.ApiResponse;
import jakarta.validation.Valid;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.web.PageableDefault;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import structor.cms.comments_service.model.CommentDTO;
import structor.cms.comments_service.model.FilterDTO;
import structor.cms.comments_service.model.NewCommentDTO;
import structor.cms.comments_service.model.UpdatedCommentDTO;
import structor.cms.comments_service.service.CommentService;

@RestController
@RequestMapping(value = "/api/v1/comments", produces = MediaType.APPLICATION_JSON_VALUE)
public class CommentsController {

    private final CommentService commentService;

    public CommentsController(final CommentService commentService) {
        this.commentService = commentService;
    }

    @GetMapping
    public ResponseEntity<Page<CommentDTO>> getAllCommentsFiltered(final FilterDTO filterDTO,
            @PageableDefault(size = 10) Pageable pageable) {
        return ResponseEntity.ok(commentService.findAll(filterDTO, pageable));
    }

    // @GetMapping
    // public ResponseEntity<Page<CommentDTO>> getAllComments(@PageableDefault(size
    // = 10) final Pageable pageable) {
    // return ResponseEntity.ok(commentService.findAll(pageable));
    // }

    // @GetMapping
    // public ResponseEntity<Page<CommentDTO>> findAllByParentId(final Integer
    // parentId, final Pageable pageable) {
    // return ResponseEntity.ok(commentService.findAllByParentId(parentId,
    // pageable));
    // }

    // @GetMapping
    // public ResponseEntity<Page<CommentDTO>> findAllByPostId(final Integer postId,
    // final Pageable pageable) {
    // return ResponseEntity.ok(commentService.findAllByPostId(postId, pageable));
    // }

    // @GetMapping
    // public ResponseEntity<Page<CommentDTO>> findAllByAuthorId(final Integer
    // authorId, final Pageable pageable) {
    // return ResponseEntity.ok(commentService.findAllByAuthorId(authorId,
    // pageable));
    // }

    // @GetMapping
    // public ResponseEntity<Page<CommentDTO>> findAllByAuthorIdAndPostId(final
    // Integer authorId, final Integer postId,
    // final Pageable pageable) {
    // return ResponseEntity.ok(commentService.findAllByAuthorIdAndPostId(authorId,
    // postId, pageable));
    // }

    @GetMapping("/{id}")
    public ResponseEntity<CommentDTO> getComment(@PathVariable(name = "id") final Integer id) {
        return ResponseEntity.ok(commentService.get(id));
    }

    @PostMapping
    @ApiResponse(responseCode = "201")
    public ResponseEntity<Integer> createComment(@RequestBody @Valid final NewCommentDTO commentDTO) {
        final Integer createdId = commentService.create(commentDTO);
        return new ResponseEntity<>(createdId, HttpStatus.CREATED);
    }

    @PutMapping("/{id}")
    public ResponseEntity<Integer> updateComment(@PathVariable(name = "id") final Integer id,
            @RequestBody @Valid final UpdatedCommentDTO commentDTO) {
        commentService.update(id, commentDTO);
        return ResponseEntity.ok(id);
    }

    @DeleteMapping("/{id}")
    @ApiResponse(responseCode = "204")
    public ResponseEntity<Void> deleteComment(@PathVariable(name = "id") final Integer id) {
        // final ReferencedWarning referencedWarning =
        // commentService.getReferencedWarning(id);
        // if (referencedWarning != null) {
        // throw new ReferencedException(referencedWarning);
        // }
        commentService.delete(id);
        return ResponseEntity.noContent().build();
    }

}
