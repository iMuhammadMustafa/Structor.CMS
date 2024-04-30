import { Schema, model } from "mongoose";

const postSchema = new Schema({
  postId: {
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
postSchema.index({ postId: 1, userId: 1 }, { unique: true });

const Post = model("Post", postSchema);

export default Post;
