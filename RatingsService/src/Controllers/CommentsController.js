import express from "express";

import {
  deleteRatingById,
  deleteRatingsByCommentId,
  deleteRatingsByUserId,
  downvoteComment,
  getAllComments,
  getByCommentId,
  getByRatingId,
  getByUserId,
  removeVote,
  upvoteComment,
} from "../db/Repositories/CommentsRepository.js";

const router = express.Router();

router.get("/", async (req, res) => {
  try {
    const comments = await getAllComments();
    res.json(comments);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

router.get("/:commentId", async (req, res) => {
  try {
    const comment = await getByCommentId(req.params.commentId);
    if (!comment) {
      return res.status(404).json({ message: "Ratings not found" });
    }
    res.json(comment);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});
router.get("/rating/:ratingId", async (req, res) => {
  try {
    const comment = await getByRatingId(req.params.ratingId);
    if (!comment) {
      return res.status(404).json({ message: "Ratings not found" });
    }
    res.json(comment);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});
router.get("/user/:userId", async (req, res) => {
  try {
    const comment = await getByUserId(req.params.userId);
    if (!comment) {
      return res.status(404).json({ message: "Ratings not found" });
    }
    res.json(comment);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

router.post("/upvote", async (req, res) => {
  try {
    const comment = await upvoteComment(req.body.commentId, req.body.userId);
    res.status(201).json(comment);
  } catch (err) {
    res.status(400).json({ error: err.message });
  }
});
router.post("/downvote", async (req, res) => {
  try {
    const comment = await downvoteComment(req.body.commentId, req.body.userId);
    res.status(201).json(comment);
  } catch (err) {
    res.status(400).json({ error: err.message });
  }
});
router.post("/removeVote", async (req, res) => {
  try {
    const post = await removeVote(req.body.commentId, req.body.userId);
    res.status(201).json(post);
  } catch (err) {
    res.status(400).json({ error: err.message });
  }
});

router.delete("/:commentId", async (req, res) => {
  try {
    const deletedComment = await deleteRatingsByCommentId(req.params.commentId);
    res.status(203).json(deletedComment);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});
router.delete("/rating/:ratingId", async (req, res) => {
  try {
    const deletedComment = await deleteRatingById(req.params.ratingId);
    res.status(203).json(deletedComment);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});
router.delete("/user/:userId", async (req, res) => {
  try {
    const deletedComment = await deleteRatingsByUserId(req.params.userId);
    res.status(203).json(deletedComment);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

export default router;
