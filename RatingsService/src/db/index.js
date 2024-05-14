import mongoose from "mongoose";

const dbConnection = async () => {
  try {
    mongoose.set("strictQuery", false);
    const connection = await mongoose.connect(`${process.env.MONGO_URI}`);
    console.info(`--> Connected to database ${process.env.MONGO_URI}`);
  } catch (e) {
    console.error(e);
    console.info('--> Retrying Database connection in 10 seconds...');
    setTimeout(async () => {
      await dbConnection();
    }, 10000);
  }
};

export default dbConnection;
