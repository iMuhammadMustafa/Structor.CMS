import amqplib from "amqplib";

const {
  RABBIT_USER,
  RABBIT_PASS,
  RABBIT_HOST,
  RABBIT_PORT,
  RABBIT_MESSAGE_DELETED_QueueName,
  RABBIT_MESSAGE_DELETED_ExchangeName
} = process.env;

let connection;
let channel;


async function rabbitConnect() {
  try {
    const amqpServer = `amqp://${RABBIT_USER}:${RABBIT_PASS}@${RABBIT_HOST}:${RABBIT_PORT}/`;
    connection = await amqplib.connect(amqpServer);

    console.log("RabbitMQ connected");

    channel = await connection.createChannel();

    console.log("Channel created");


    const queueName = RABBIT_MESSAGE_DELETED_QueueName;
    const exchangeName = RABBIT_MESSAGE_DELETED_ExchangeName;



    await channel.assertExchange(exchangeName, "fanout", { durable: true });

    await channel.assertQueue(queueName, { durable: true });
     await channel.bindQueue(queueName, exchangeName);


    await channel.consume(queueName, (msg) => {

      if (msg.content) {
        const message = msg.content.toString();

        const messageObj = JSON.parse(message);

        console.log(messageObj);
        
        channel.ack(msg);
      }
    });



    console.log("Queue created");
  } catch (error) {
    console.log(error);
  }
}

export { rabbitConnect };
