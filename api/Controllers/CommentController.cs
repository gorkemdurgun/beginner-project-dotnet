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
        private readonly IStockRepository _stockRepository;

        public CommentController(ApplicationDBContext context, ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            _commentRepository = commentRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            if (ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepository.GetAllAsync();
            var commentDto = comments.Select(c => c.MapToCommentDto());
            return Ok(comments);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {

            if (ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound(new { message = "Comment not found" });
            }
            return Ok(comment.MapToCommentDto());
        }



        [HttpPost]
        [Route("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _stockRepository.StockExists(stockId))
            {
                return BadRequest(new { message = "Stock does not exist" });
            }

            var commentModel = createCommentDto.MapToCommentFromCreateDto(stockId);
            await _commentRepository.CreateAsync(commentModel);

            var createdComment = commentModel.MapToCommentDto();
            return CreatedAtAction(nameof(GetCommentById), new { id = createdComment.Id }, createdComment);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
        {

            if (ModelState.IsValid)
                return BadRequest(ModelState);

            var commentModel = await _commentRepository.UpdateAsync(id, updateCommentDto);

            if (commentModel == null)
            {
                return NotFound(new { message = "Comment not found" });
            }

            return Ok(commentModel.MapToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            if (ModelState.IsValid)
                return BadRequest(ModelState);

            var commentModel = await _commentRepository.DeleteAsync(id);

            if (commentModel == null)
            {
                return NotFound(new { message = "Comment not found" });
            }

            return Ok(commentModel.MapToCommentDto());
        }
    }
}