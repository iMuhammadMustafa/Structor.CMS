package structor.cms.comments_service.domain;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.OneToMany;
import jakarta.persistence.Table;
import java.util.Set;
import lombok.Getter;
import lombok.Setter;

@Entity
@Table(name = "Comments")
@Getter
@Setter
public class Comment extends IEntity {

    @Column(nullable = false)
    private String text;

    @Column
    private Integer rating;

    @Column
    private String author;

    @Column(nullable = false)
    private Integer authorId;

    @Column(nullable = false)
    private Integer postId;

    @Column(name = "parent_id", insertable = false, updatable = false)
    private Integer parentId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "parent_id")
    private Comment parent;

    @OneToMany(mappedBy = "parent")
    private Set<Comment> children;

    // @NotNull
    // @AssertFalse(message = "A comment cannot reference itself as a parent.")
    // public boolean isParentSelfReferencing() {
    // return this.parentId != null && this.parentId.equals(this.getId());
    // }
}
