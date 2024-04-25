package structor.cms.comments_service.config;

import org.springframework.amqp.core.Binding;
import org.springframework.amqp.core.BindingBuilder;
import org.springframework.amqp.core.FanoutExchange;
import org.springframework.amqp.core.Queue;
import org.springframework.amqp.core.TopicExchange;
import org.springframework.amqp.rabbit.connection.ConnectionFactory;
import org.springframework.amqp.rabbit.listener.MessageListenerContainer;
import org.springframework.amqp.rabbit.listener.SimpleMessageListenerContainer;
import org.springframework.amqp.rabbit.listener.adapter.MessageListenerAdapter;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import structor.cms.comments_service.service.MessagingConsumer;

@Configuration

public class RabbitMQConfig {
    static final String ExchangeName = "Structor.CMS.Contracts:PostDeleted";
    static final String QueueName = "PostDeletedCommentsConsumer";

    @Bean
    Queue queue() {
        return new Queue(QueueName);
    }

    @Bean
    FanoutExchange exchange() {
        return new FanoutExchange(ExchangeName);
    }

    @Bean
    Binding binding(Queue queue, FanoutExchange exchange) {
        return BindingBuilder.bind(queue).to(exchange);
    }

    @Bean
    MessageListenerAdapter listenerAdapter(MessagingConsumer consumer) {
        return new MessageListenerAdapter(consumer, "postDeletedMessageRecieved");
    }

    @Bean
    MessageListenerContainer container(ConnectionFactory connectionFactory, MessageListenerAdapter listenerAdapter) {
        SimpleMessageListenerContainer container = new SimpleMessageListenerContainer();
        container.setConnectionFactory(connectionFactory);
        container.setQueueNames(QueueName);
        container.setMessageListener(listenerAdapter);

        return container;

    }
}
