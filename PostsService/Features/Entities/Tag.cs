﻿using PostsService.Infrastructure.Entities;

namespace PostsService.Features.Entities;

public class Tag : IEntity
{
    public required string Name { get; set; }
    public IEnumerable<Post> Posts { get; set; } = new List<Post>();
}
