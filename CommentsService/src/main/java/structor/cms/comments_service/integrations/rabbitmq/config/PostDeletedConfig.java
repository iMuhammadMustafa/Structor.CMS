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

import structor.cms.comments_service.integrations.rabbitmq.consumers.PostDeletedConsumer;

@Configuration
public class PostDeletedConfig {
    public static final String ExchangeName = "Structor.CMS.Integrations.MessagingContracts:PostDeleted";
    public static final String QueueName = "PostDeletedCommentsConsumer";

    @Bean
    Queue postDeletedQueue() {
        return new Queue(QueueName);
    }

    @Bean
    FanoutExchange postDeletedExchange() {
        return new FanoutExchange(ExchangeName);
    }

    @Bean
    Binding postDeletedBinding(Queue postDeletedQueue, FanoutExchange postDeletedExchange) {
        return BindingBuilder.bind(postDeletedQueue).to(postDeletedExchange);
    }

    @Bean
    MessageListenerAdapter postDeletedListenerAdapter(PostDeletedConsumer consumer,
            Jackson2JsonMessageConverter messageConverter) {

        MessageListenerAdapter listenerAdapter = new MessageListenerAdapter(consumer, messageConverter);
        return listenerAdapter;
    }

    @Bean
    MessageListenerContainer postDeletedContainer(ConnectionFactory connectionFactory,
            MessageListenerAdapter postDeletedListenerAdapter) {
        SimpleMessageListenerContainer container = new SimpleMessageListenerContainer();
        container.setConnectionFactory(connectionFactory);
        container.setQueueNames(QueueName);
        container.setMessageListener(postDeletedListenerAdapter);
        return container;
    }
}
