import express from "express";

import bodyParser from "body-parser";
import "dotenv/config";

import routes from "./controllers/index.js";
import dbConnection from "./db/index.js";

const app = express();

app.use(express.json());
app.use(express.urlencoded({ extended: false }));

app.use(routes);

dbConnection();

app.listen(process.env.PORT, () => {
  console.log(`Server is running on port ${process.env.PORT}`);
});
