const commentSchema = new Schema({
  id: {
    type: Number,
    required: true,
  },
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
});
commentSchema.index({ postId: 1, userId: 1 }, { unique: true });

const Comment = mongoose.model("Comments", commentSchema);

export default Comment;
