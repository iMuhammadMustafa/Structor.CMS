using Mapster;
using PostsService.Features.Dtos;
using PostsService.Features.Entities;
using PostsService.Features.Repositories;
using PostsService.Infrastructure.DTOs.REST;

namespace PostsService.Features.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;

        public PostService(IPostRepository postRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }
        public async Task<IEnumerable<PostDto>> GetAll(Pagination pagination)
        {
            var data = await _postRepository.GetAllPaginated(pagination);
            return data.Adapt<IEnumerable<PostDto>>();
        }
        public async Task<IEnumerable<PostDto>> GetAllByCategoryId(int categoryId, Pagination pagination)
        {
            var data = await _postRepository.GetAllByCategoryId(categoryId, pagination);

            return data.Adapt<IEnumerable<PostDto>>();
        }

        public async Task<IEnumerable<PostDto>> GetAllByTagId(int tagId, Pagination pagination)
        {
            var data = await _postRepository.GetAllByTagId(tagId, pagination);

            return data.Adapt<IEnumerable<PostDto>>();
        }
        public async Task<PostDto?> FindById(int id)
        {
            var data = await _postRepository.FindById(id);

            if (data == null) throw new Exception("There is no Post by that ID");

            return data.Adapt<PostDto>();
        }
        public async Task<PostDto?> FindByGuid(Guid guid)
        {
            var data = await _postRepository.FindByGuid(guid);

            if (data == null) throw new Exception("There is no Post by that Guid");

            return data.Adapt<PostDto>();

        }
        public async Task<PostDto> Insert(PostFormDto entity)
        {
            var data = entity.Adapt<Post>();

            await _postRepository.Add(data, true);

            return data.Adapt<PostDto>();
        }
        public async Task<PostDto> Update(PostFormDto entity)
        {
            var data = entity.Adapt<Post>();

            await _postRepository.Update(data, true);

            return data.Adapt<PostDto>();
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
