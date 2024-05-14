package structor.cms.comments_service.integrations.rabbitmq.messageContracts;

import java.util.UUID;

public record PostDeleted(Integer id, UUID guid) {
}