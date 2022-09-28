﻿using API.Controllers.Base;
using AutoMapper;
using Domain;
using Domain.Dto.Post;
using Domain.Models;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace API.Controllers
{
    [Route("api/posts")]
    public class PostsController : AppBaseController
    {
        private readonly IPostService _postsService;
        private readonly IMapper _mapper;

        public PostsController(IPostService postsService,IMapper mapper)
        {
            _postsService = postsService;
            _mapper = mapper;
        }

        [HttpGet("{postId:int}")]
        public async Task<PostModel> GetById(int postId, CancellationToken cancellationToken)
        {
            var post = await _postsService.GetById(postId,cancellationToken);

            return _mapper.Map<PostModel>(post);
        }

        [HttpPost]
        public async Task<PostModel> CreatePost(PostDto postContent, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<Post>(postContent);
            request.UserId = GetCurrentUserId();

            await _postsService.Add(request,cancellationToken);

            request.RegistrationDate = DateTime.UtcNow;
            return _mapper.Map<PostModel>(request);
        }

        [HttpPut("{postId:int}")]
        public async Task<PostModel> UpdatePostById(int postId,[FromBody] PostDto post, CancellationToken cancellationToken)
        {

            var request = _mapper.Map<Post>(post);
            request.Id = postId;
            request.UserId = GetCurrentUserId();

            await _postsService.Update(request,cancellationToken);

            var updatedPost = await _postsService.GetById(postId,cancellationToken);
            return _mapper.Map<PostModel>(updatedPost);
        }

        [HttpDelete("{postId:int}")]
        public async Task<IActionResult> DeletePostById(int postId, CancellationToken cancellationToken)
        {
            await _postsService.Remove(postId, issuerId: GetCurrentUserId(),cancellationToken);
            return Ok();
        }

        [HttpPost("paginated-search")]
        public async Task<PaginatedResult<PostModel>> GetPagedPosts(PagedRequest pagedRequest, CancellationToken cancellationToken)
        {
            var response = await _postsService.GetPage(pagedRequest,cancellationToken);

            return new PaginatedResult<PostModel>()
            {
                PageIndex = response.PageIndex,
                PageSize = response.PageSize,
                Total = response.Total,
                Items = response.Items.Select(e => _mapper.Map<PostModel>(e)).ToList()
            };
        }
    }
}