import Comment from "../Models/Comment.js";

async function getAllComments() {
  try {
    const comments = await Comment.find({ isDeleted: false });
    return comments;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function getByRatingId(_id) {
  try {
    const comments = await Comment.find({ _id });
    if (!comments) {
      throw new Error("Comment not found");
    }
    return comments;
  } catch (err) {
    throw new Error(err.message);
  }
}
async function getByCommentId(commentId) {
  try {
    const comments = await Comment.find({ commentId });
    if (!comments) {
      throw new Error("Comment not found");
    }
    return comments;
  } catch (err) {
    throw new Error(err.message);
  }
}
async function getByUserId(userId) {
  try {
    const comment = await Comment.find({ userId });
    if (!comment) {
      throw new Error("Comment not found");
    }
    return comment;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function upvoteComment(commentId, userId) {
  try {
    let comment = await Comment.findOne({ commentId, userId });

    if (!comment) {
      comment = await Comment.create({
        commentId,
        userId,
        rating: 1,
        createdBy: userId,
      });
    } else {
      comment.rating = 1;
      comment.updatedBy = userId;
      await comment.save();
    }

    return comment;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function downvoteComment(commentId, userId) {
  try {
    let comment = await Comment.findOne({ commentId, userId });

    if (!comment) {
      comment = await Comment.create({
        commentId,
        userId,
        rating: -1,
        createdBy: userId,
      });
    } else {
      comment.rating = -1;
      comment.updatedBy = userId;
      await comment.save();
    }

    return comment;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function removeVote(commentId, userId) {
  try {
    let comment = await Comment.findOne({
      commentId,
      userId,
    });

    if (!comment) {
      throw new Error("Comment not found");
    }

    comment.rating = 0;
    comment.updatedBy = userId;
    await comment.save();

    return comment;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function deleteRatingById(_id) {
  try {
    const deletedComment = await Comment.findOneAndDelete({ _id });
    if (!deletedComment) {
      throw new Error("Comment not found");
    }
    return deletedComment;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function deleteRatingsByCommentId(commentId) {
  try {
    const deletedComments = await Comment.deleteMany({ commentId });
    if (!deletedComments) {
      throw new Error("Comments not found");
    }
    return deletedComments;
  } catch (err) {
    throw new Error(err.message);
  }
}

async function deleteRatingsByUserId(userId) {
  try {
    const deletedComments = await Comment.deleteMany({ userId });
    if (!deletedComments) {
      throw new Error("Comments not found");
    }
    return deletedComments;
  } catch (err) {
    throw new Error(err.message);
  }
}

export {
  getAllComments,
  getByRatingId,
  getByUserId,
  getByCommentId,
  upvoteComment,
  downvoteComment,
  removeVote,
  deleteRatingById,
  deleteRatingsByCommentId,
  deleteRatingsByUserId,
};
