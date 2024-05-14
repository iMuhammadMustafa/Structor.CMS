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


    channel = await connection.createChannel();

    const queueName = RABBIT_MESSAGE_DELETED_QueueName;
    const exchangeName = RABBIT_MESSAGE_DELETED_ExchangeName;



    await channel.assertExchange(exchangeName, "fanout", { durable: true });

    await channel.assertQueue(queueName, { durable: true });
     await channel.bindQueue(queueName, exchangeName);


    await channel.consume(queueName, (msg) => {

      if (msg.content) {
        const message = msg.content.toString();

        const messageObj = JSON.parse(message);
                
        channel.ack(msg);
      }
    });



    console.log(`--> RabbitMQ connected on ${RABBIT_HOST}:${RABBIT_PORT}`);
  } catch (error) {
    console.error(error);
    console.log("--> Failed to Connect to RabbitMQ.");
    console.log("--> Retrying connection in 10 seconds...");
    setTimeout(async () => {
      await rabbitConnect();
    }, 10000);
  }
}

export { rabbitConnect };
