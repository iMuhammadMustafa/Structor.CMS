using AutoMapper;
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
    private readonly IMapper _mapper;

    public TagService(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TagDto>> GetAll(Pagination pagination)
    {
        var data = await _tagRepository.GetAll()
                                        .Paginate(pagination.Page, pagination.Size)
                                        .ToListAsync();

        return _mapper.Map<IEnumerable<TagDto>>(data);
    }
    public async Task<TagDto?> FindById(int id)
    {
        var data = await _tagRepository.FindById(id);

        return _mapper.Map<TagDto>(data);
    }
    public async Task<TagDto?> FindByGuid(Guid guid)
    {
        var data = await _tagRepository.FindByGuid(guid);

        return _mapper.Map<TagDto>(data);

    }
    public async Task<TagDto> Insert(TagFormDto entity)
    {
        var data = _mapper.Map<Tag>(entity);

        await _tagRepository.Add(data, true);

        return _mapper.Map<TagDto>(data);
    }
    public async Task<TagDto> Update(int id, TagFormDto entity)
    {
        var data = _mapper.Map<Tag>(entity);
        data.Id = id;

        await _tagRepository.Update(data, true);

        return _mapper.Map<TagDto>(data);
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
