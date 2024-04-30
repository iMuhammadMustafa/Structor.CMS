import Post from "../Models/Post.js";

async function getAllPosts() {
  try {
    const posts = await Post.find();
    return posts;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function getByRatingId(_id) {
  try {
    const posts = await Post.find({ _id });
    if (!posts) {
      throw new Error("Post not found");
    }
    return posts;
  } catch (err) {
    throw new Error(err.message);
  }
}
async function getByPostId(postId) {
  try {
    const posts = await Post.find({ postId });
    if (!posts) {
      throw new Error("Post not found");
    }
    return posts;
  } catch (err) {
    throw new Error(err.message);
  }
}
async function getByUserId(userId) {
  try {
    const post = await Post.find({ userId });
    if (!post) {
      throw new Error("Post not found");
    }
    return post;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function upvotePost(postId, userId) {
  try {
    let post = await Post.findOne({ postId, userId });

    if (!post) {
      post = await Post.create({
        postId,
        userId,
        rating: 1,
        createdBy: userId,
      });
    } else {
      post.rating = 1;
      post.updatedBy = userId;
      await post.save();
    }

    return post;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function downvotePost(postId, userId) {
  try {
    let post = await Post.findOne({ postId, userId });

    if (!post) {
      post = await Post.create({
        postId,
        userId,
        rating: -1,
        createdBy: userId,
      });
    } else {
      post.rating = -1;
      post.updatedBy = userId;
      await post.save();
    }

    return post;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function removeVote(postId, userId) {
  try {
    let post = await Post.findOne({
      postId,
      userId,
    });

    if (!post) {
      throw new Error("Post not found");
    }

    post.rating = 0;
    post.updatedBy = userId;
    await post.save();

    return post;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function deleteRatingById(_id) {
  try {
    const deletedPost = await Post.findOneAndDelete({ _id });
    if (!deletedPost) {
      throw new Error("Post not found");
    }
    return deletedPost;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function deleteRatingsByPostId(postId) {
  try {
    const deletedPosts = await Post.deleteMany({ postId });
    if (!deletedPosts) {
      throw new Error("Posts not found");
    }
    return deletedPosts;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function deleteRatingsByUserId(userId) {
  try {
    const deletedPosts = await Post.deleteMany({ userId });
    if (!deletedPosts) {
      throw new Error("Posts not found");
    }
    return deletedPosts;
  } catch (err) {
    throw new Error(err.message);
  }
}

export {
  getAllPosts,
  getByRatingId,
  getByUserId,
  getByPostId,
  upvotePost,
  downvotePost,
  removeVote,
  deleteRatingById,
  deleteRatingsByPostId,
  deleteRatingsByUserId,
};
