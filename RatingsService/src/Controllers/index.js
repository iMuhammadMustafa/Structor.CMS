// import { Router } from "express";
import express from "express";

const router = express.Router();

// const router = Router();

router.get("/", (req, res) => {
  res.send("Hello, world!");
});

router.all("*", (req, res) => res.sendStatus(404));
export default router;
