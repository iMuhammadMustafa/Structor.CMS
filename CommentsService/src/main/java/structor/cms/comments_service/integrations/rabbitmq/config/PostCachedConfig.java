package structor.cms.comments_service.integrations.rabbitmq.config;

import org.springframework.amqp.core.Binding;
import org.springframework.amqp.core.BindingBuilder;
import org.springframework.amqp.core.FanoutExchange;
import org.springframework.amqp.core.Queue;
import org.springframework.amqp.rabbit.connection.ConnectionFactory;
import org.springframework.amqp.rabbit.listener.MessageListenerContainer;
import org.springframework.amqp.rabbit.listener.SimpleMessageListenerContainer;
import org.springframework.amqp.rabbit.listener.adapter.MessageListenerAdapter;
import org.springframework.amqp.support.converter.Jackson2JsonMessageConverter;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import structor.cms.comments_service.integrations.rabbitmq.consumers.PostCachedConsumer;

@Configuration
public class PostCachedConfig {
    public static final String ExchangeName = "Structor.CMS.Integrations.MessagingContracts:PostCached[]";
    public static final String QueueName = "PostCachedCommentsConsumer";

    @Bean
    Queue postCachedQueue() {
        return new Queue(QueueName);
    }

    @Bean
    FanoutExchange postCachedExchange() {
        return new FanoutExchange(ExchangeName);
    }

    @Bean
    Binding postCachedBinding(Queue postCachedQueue, FanoutExchange postCachedExchange) {
        return BindingBuilder.bind(postCachedQueue).to(postCachedExchange);
    }

    @Bean
    MessageListenerAdapter postCachedListenerAdapter(PostCachedConsumer consumer,
            Jackson2JsonMessageConverter messageConverter) {

        MessageListenerAdapter listenerAdapter = new MessageListenerAdapter(consumer, messageConverter);
        return listenerAdapter;
    }

    @Bean
    MessageListenerContainer postCachedContainer(ConnectionFactory connectionFactory,
            MessageListenerAdapter postCachedListenerAdapter) {
        var container = new SimpleMessageListenerContainer();
        container.setConnectionFactory(connectionFactory);
        container.setQueueNames(QueueName);
        container.setMessageListener(postCachedListenerAdapter);
        return container;
    }
}
