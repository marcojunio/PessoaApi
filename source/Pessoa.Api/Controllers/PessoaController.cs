using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pessoa.Domain.Commands;
using Pessoa.Domain.Handlers;
using Pessoa.Domain.Repositories;
using Pessoa.Shared.ContractsCommand;
using Pessoa.Shared.Core.FileManipulation.Contracts;
using Pessoa.Shared.Helpers;

namespace Pessoa.Api.Controllers{ 

    [ApiController]
    [Route("api/v1/pessoa")]
    [Authorize(Roles = "User")]
    public class PessoaController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Save(
        [FromBody] CommandCreatePessoa command,
        [FromServices] PessoaHandler handler)
        { 
            var result = (GenericCommandResult) await handler.Handle(command);
            
            if(result.Success){
                return Ok(result);
            }else{
                return BadRequest(result);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(
        [FromServices] IPessoaEntityRepository pessoaEntityRepository)
        { 
            var result = await pessoaEntityRepository.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAll(string id,
        [FromServices] IPessoaEntityRepository pessoaEntityRepository)
        { 
            var result = await pessoaEntityRepository.GetById(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
        [FromBody] CommandUpdatePessoa command,
        [FromServices] PessoaHandler handler)
        {
            var result = (GenericCommandResult)await handler.Handle(command);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Update(string id,[FromServices] IPessoaEntityRepository pessoaEntityRepository)
        {
            await pessoaEntityRepository.Delete(id);
            return Ok();
        }

        [HttpPost]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [Route("upload-file")]
        public async Task<IActionResult> UploadSingleFile([FromServices] IUploadFileService uploadService,[FromForm] IFormFile file)
        {
            var user = HttpContextHelper.GetClaims(User.Claims);
            var response = await uploadService.UploadFile(file,user?.CustumerId,user?.Name);

            return Ok(response);
        }


    }
}