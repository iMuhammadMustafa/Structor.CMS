import express from "express";

import {
  deleteRatingById,
  deleteRatingsByPostId,
  deleteRatingsByUserId,
  downvotePost,
  getAllPosts,
  getByPostId,
  getByRatingId,
  getByUserId,
  removeVote,
  upvotePost,
} from "../db/Repositories/PostsRepository.js";

const router = express.Router();

router.get("/", async (req, res) => {
  try {
    const posts = await getAllPosts();
    res.json(posts);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

router.get("/:postId", async (req, res) => {
  try {
    const post = await getByPostId(req.params.postId);
    if (!post) {
      return res.status(404).json({ message: "Ratings not found" });
    }
    res.json(post);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});
router.get("/rating/:ratingId", async (req, res) => {
  try {
    const post = await getByRatingId(req.params.ratingId);
    if (!post) {
      return res.status(404).json({ message: "Ratings not found" });
    }
    res.json(post);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});
router.get("/user/:userId", async (req, res) => {
  try {
    const post = await getByUserId(req.params.userId);
    if (!post) {
      return res.status(404).json({ message: "Ratings not found" });
    }
    res.json(post);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

router.post("/upvote", async (req, res) => {
  try {
    const post = await upvotePost(req.body.postId, req.body.userId);
    res.status(201).json(post);
  } catch (err) {
    res.status(400).json({ error: err.message });
  }
});
router.post("/downvote", async (req, res) => {
  try {
    const post = await downvotePost(req.body.postId, req.body.userId);
    res.status(201).json(post);
  } catch (err) {
    res.status(400).json({ error: err.message });
  }
});
router.post("/removeVote", async (req, res) => {
  try {
    const post = await removeVote(req.body.postId, req.body.userId);
    res.status(201).json(post);
  } catch (err) {
    res.status(400).json({ error: err.message });
  }
});

router.delete("/:postId", async (req, res) => {
  try {
    const deletedPost = await deleteRatingsByPostId(req.params.postId);
    res.status(203).json(deletedPost);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});
router.delete("/rating/:ratingId", async (req, res) => {
  try {
    const deletedPost = await deleteRatingById(req.params.ratingId);
    res.status(203).json(deletedPost);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});
router.delete("/user/:userId", async (req, res) => {
  try {
    const deletedPost = await deleteRatingsByUserId(req.params.userId);
    res.status(203).json(deletedPost);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

export default router;
