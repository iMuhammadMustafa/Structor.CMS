import express from "express";

import bodyParser from "body-parser";
import "dotenv/config";
import swaggerUi from "swagger-ui-express";

import specs from "./config/swagger.config.js";
import routes from "./controllers/index.js";
import dbConnection from "./db/index.js";
import swaggerDocument from "./docs/swagger-output.json"  with { type: "json" };

dbConnection();

const app = express();

app.use(express.json());
app.use(express.urlencoded({ extended: false }));

app.use(routes);

app.use("/swagger", swaggerUi.serve, swaggerUi.setup(swaggerDocument, specs, { explorer: true }));
app.all("*", (req, res) => res.sendStatus(404));

app.listen(process.env.PORT, () => {
  console.log(`Server is running on port ${process.env.PORT}`);
});
