using Microsoft.AspNetCore.Mvc;
using SeventyModel;
using SeventyBusiness;
using Swashbuckle.AspNetCore.Annotations;

namespace SeventyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly AlbumService albumService;

        public AlbumsController(AlbumService albumService)
        {
            this.albumService = albumService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todos os álbuns",
            Description = "Retorna uma lista de todos os álbuns cadastrados. Caso não haja, retorna 204 No Content.",
            OperationId = "GetAllAlbums",
            Tags = new[] { "Álbum" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get()
        {
            var albuns = albumService.ListarTodos();
            return albuns.Count == 0 ? NoContent() : Ok(albuns);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtém um álbum por ID",
            Description = "Retorna um álbum com base no ID fornecido. Se não encontrar, retorna 404 Not Found.",
            OperationId = "GetAlbumById",
            Tags = new[] { "Álbum" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var album = albumService.ObterPorId(id);
            return album == null ? NotFound() : Ok(album);
        }

        [HttpGet("ano")]
        [SwaggerOperation(
            Summary = "Obtém álbuns por ano de lançamento",
            Description = "Retorna uma lista de álbuns filtrada por ano de lançamento. Se não houver, retorna 404 Not Found.",
            OperationId = "GetAlbumsByYear",
            Tags = new[] { "Álbum" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPorAno([FromQuery] int ano)
        {
            var albuns = albumService.ObterPorAno(ano);
            return albuns.Count == 0 ? NotFound() : Ok(albuns);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastra um novo álbum",
            Description = "Recebe um objeto de álbum e cadastra. Retorna o objeto criado com status 201 Created.",
            OperationId = "CreateAlbum",
            Tags = new[] { "Álbum" }
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AlbumModel album)
        {
            if (string.IsNullOrWhiteSpace(album.Name))
                return BadRequest("Nome do álbum é obrigatório.");

            var criado = albumService.Cadastrar(album);
            return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza um álbum existente",
            Description = "Atualiza os dados de um álbum. Retorna 204 No Content se bem-sucedido, ou 404 Not Found se não existir.",
            OperationId = "UpdateAlbum",
            Tags = new[] { "Álbum" }
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] AlbumModel album)
        {
            if (album == null || album.Id != id)
                return BadRequest("Dados inconsistentes.");

            return albumService.Atualizar(album) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove um álbum",
            Description = "Remove um álbum com base no ID. Retorna 204 No Content se bem-sucedido, ou 404 Not Found se não existir.",
            OperationId = "DeleteAlbum",
            Tags = new[] { "Álbum" }
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            return albumService.Remover(id) ? NoContent() : NotFound();
        }
    }
}
