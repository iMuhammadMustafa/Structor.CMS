package structor.cms.comments_service.domain;

import jakarta.persistence.Column;
import jakarta.persistence.EntityListeners;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.MappedSuperclass;
import java.time.OffsetDateTime;
import java.util.UUID;
import lombok.Getter;
import lombok.Setter;
import org.springframework.data.annotation.CreatedDate;
import org.springframework.data.annotation.LastModifiedDate;
import org.springframework.data.jpa.domain.support.AuditingEntityListener;

@MappedSuperclass
// ASK: Why is this V ?
@EntityListeners(AuditingEntityListener.class)
@Getter
@Setter
public abstract class IEntity {

    @Id
    @Column(nullable = false, updatable = false)
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    @Column(nullable = false, columnDefinition = "char(36)")
    private UUID guid = UUID.randomUUID();

    @Column
    private String createdBy = "SYSTEM";

    @CreatedDate
    @Column(nullable = false, updatable = false)
    private OffsetDateTime createdDate = OffsetDateTime.now();

    private String updatedBy;

    @LastModifiedDate
    @Column(nullable = false)
    private OffsetDateTime updatedDate;
}
