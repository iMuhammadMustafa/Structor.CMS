import express from "express";

import bodyParser from "body-parser";
import "dotenv/config";
import swaggerUi from "swagger-ui-express";

import specs from "./config/swagger.config.js";
import dbConnect from "./db/index.js";
import swaggerDocument from "./docs/swagger-output.json"  with { type: "json" };


import  postsController from "./Controllers/PostsController.js";
import  commentsController from "./Controllers/CommentsController.js";

dbConnect();

const app = express();

app.use(express.json());
app.use(express.urlencoded({ extended: false }));

app.use("/api/v1/posts", postsController);
app.use("/api/v1/comments", commentsController);

app.use("/swagger", swaggerUi.serve, swaggerUi.setup(swaggerDocument, specs, { explorer: true }));
app.all("*", (req, res) => res.sendStatus(404));

app.listen(process.env.PORT, () => {
  console.log(`Server is running on port ${process.env.PORT}`);
});
