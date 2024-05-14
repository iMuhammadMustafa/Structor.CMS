package structor.cms.comments_service.integrations.rabbitmq.messageContracts;

import java.util.UUID;

public record PostCached(Integer id, UUID guid) {
}