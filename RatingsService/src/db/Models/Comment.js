import { Schema, model } from "mongoose";

const commentSchema = new Schema({
  commentId: {
    type: Number,
    required: true,
  },
  userId: {
    type: Number,
    required: true,
  },
  rating: {
    type: Number,
    required: true,
    default: 0,
  },
  createdBy: {
    type: String,
    required: true,
    default: "System",
  },
  updatedBy: {
    type: String,
  },
  isDeleted: {
    type: Boolean,
    default: false,
  },
});
commentSchema.index({ commentId: 1, userId: 1 }, { unique: true });

const Comment = model("Comments", commentSchema);

export default Comment;
