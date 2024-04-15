using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostsService.Features.Dtos;
using PostsService.Features.Entities;
using PostsService.Features.Repositories;
using PostsService.Infrastructure.DTOs.REST;
using PostsService.Infrastructure.Extentions;

namespace PostsService.Features.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, ITagRepository tagRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostDto>> GetAll(Pagination pagination)
        {
            var data = await _postRepository.GetAllPaginated(pagination);
            return _mapper.Map<IEnumerable<PostDto>>(data);
        }
        public async Task<IEnumerable<PostDto>> GetAllByCategoryId(int categoryId, Pagination pagination)
        {
            var data = await _postRepository.GetAllByCategoryId(categoryId, pagination);

            return _mapper.Map<IEnumerable<PostDto>>(data);
        }

        public async Task<IEnumerable<PostDto>> GetAllByTagId(int tagId, Pagination pagination)
        {
            var data = await _postRepository.GetAllByTagId(tagId, pagination);

            return _mapper.Map<IEnumerable<PostDto>>(data);
        }
        public async Task<PostDto?> FindById(int id)
        {
            var data = await _postRepository.FindById(id);

            if (data == null) throw new Exception("There is no Post by that ID");

            return _mapper.Map<PostDto>(data);
        }
        public async Task<PostDto?> FindByGuid(Guid guid)
        {
            var data = await _postRepository.FindByGuid(guid);

            if (data == null) throw new Exception("There is no Post by that Guid");

            return _mapper.Map<PostDto>(data);

        }
        public async Task<PostDto> Insert(PostFormDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            var tags = new List<Tag>();

            if (postDto.Tags is not null && postDto.Tags.Count > 0)
            {
                foreach (var tagDto in postDto.Tags.Where(tag => tag is not null))
                {
                    var existingTag = tagDto.Id <= 0
                                                ? await _tagRepository.FindByName(tagDto.Name)
                                                : await _tagRepository.FindById(tagDto.Id);

                    if (existingTag is null)
                    {
                        existingTag = new Tag { Name = tagDto.Name.ToTitleCase() };
                        await _tagRepository.Add(existingTag);
                    }

                    post.Tags.Add(existingTag);
                }
            }


            await _postRepository.Add(post);
            await _postRepository.SaveChanges();

            return _mapper.Map<PostDto>(post);
        }
        public async Task<PostDto> Update(int id, PostFormUpdateDto entity)
        {
            var data = _mapper.Map<Post>(entity);

            await _postRepository.Update(data, true);

            return _mapper.Map<PostDto>(data);
        }

        public async Task<PostDto> AddTags(int id, IEnumerable<TagFormDto> tags)
        {
            var post = await _postRepository.FindById(id);

            if (post is null) throw new Exception("No Post with that Id");

            foreach (var tagDto in tags)
            {
                var existingTag = tagDto.Id <= 0
                            ? await _tagRepository.FindByName(tagDto.Name)
                            : await _tagRepository.FindById(tagDto.Id);

                if (existingTag is null)
                {
                    existingTag = new Tag { Name = tagDto.Name.ToTitleCase() };
                    await _tagRepository.Add(existingTag);
                }

                post.Tags.Add(existingTag);

            }

            await _postRepository.Update(post);
            await _postRepository.SaveChanges();

            return _mapper.Map<PostDto>(post);
        }
        public async Task<PostDto> RemoveTags(int id, IEnumerable<int> tagIds)
        {
            var post = await _postRepository.GetAllIncluding(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);

            if (post is null) throw new Exception("No Post with that Id");

            post.Tags.RemoveAll(tag => tagIds.Contains(tag.Id));

            await _postRepository.Update(post);
            await _postRepository.SaveChanges();

            return _mapper.Map<PostDto>(post);
        }

        public async Task<bool> Delete(int id)
        {
            var data = await _postRepository.FindById(id);

            if (data == null) return false;

            await _postRepository.Delete(data, true);

            return true;
        }

        public async Task<int> GetCount()
        {
            return await _postRepository.Count();
        }


    }
}
