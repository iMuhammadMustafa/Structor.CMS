using Mapster;
using Microsoft.EntityFrameworkCore;
using PostsService.Features.Dtos;
using PostsService.Features.Entities;
using PostsService.Features.Repositories;
using PostsService.Infrastructure.DTOs.REST;
using PostsService.Infrastructure.Extentions;

namespace PostsService.Features.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<IEnumerable<TagDto>> GetAll(Pagination pagination)
    {
        var data = await _tagRepository.GetAll()
                                        .Paginate(pagination.Page, pagination.Size)
                                        .ToListAsync();

        return data.Adapt<IEnumerable<TagDto>>();
    }
    public async Task<TagDto?> FindById(int id)
    {
        var data = await _tagRepository.FindById(id);

        return data?.Adapt<TagDto>();
    }
    public async Task<TagDto?> FindByGuid(Guid guid)
    {
        var data = await _tagRepository.FindByGuid(guid);

        return data?.Adapt<TagDto>();

    }
    public async Task<TagDto> Insert(TagFormDto entity)
    {
        var data = entity.Adapt<Tag>();

        await _tagRepository.Add(data, true);

        return data.Adapt<TagDto>();
    }
    public async Task<TagDto> Update(TagFormDto entity)
    {
        var data = entity.Adapt<Tag>();

        await _tagRepository.Update(data, true);

        return data.Adapt<TagDto>();
    }
    public async Task<bool> Delete(int id)
    {
        var data = await _tagRepository.FindById(id);

        if (data == null) return false;

        await _tagRepository.Delete(data, true);

        return true;
    }

    public async Task<int> GetCount()
    {
        return await _tagRepository.Count();
    }
}
