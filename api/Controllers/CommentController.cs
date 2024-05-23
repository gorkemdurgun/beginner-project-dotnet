using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Mappers;
using api.Dtos.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommentRepository _commentRepository;

        public CommentController(ApplicationDBContext context, ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentDto = comments.Select(c => c.MapToCommentDto());
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound(new { message = "Comment not found" });
            }
            return Ok(comment.MapToCommentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentReqDto)
        {
            var commentModel = commentReqDto.MapToCommentFromCreateDto();
            await _commentRepository.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.Id }, commentModel.MapToCommentDto());
        }


    }
}