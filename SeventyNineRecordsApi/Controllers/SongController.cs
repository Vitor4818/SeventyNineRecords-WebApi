using Microsoft.AspNetCore.Mvc;
using SeventyModel;
using SeventyBusiness;
using Swashbuckle.AspNetCore.Annotations;

namespace SeventyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly SongService songService;

        public SongsController(SongService songService)
        {
            this.songService = songService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todas as músicas",
            Description = "Retorna uma lista de todas as músicas cadastradas. Se não houver músicas, retorna 204 No Content.",
            OperationId = "GetAllSongs",
            Tags = new[] { "Song" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get()
        {
            var songs = songService.ListarTodas();
            return songs.Count == 0 ? NoContent() : Ok(songs);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtém uma música por ID",
            Description = "Retorna uma música com base no ID fornecido. Se não for encontrada, retorna 404 Not Found.",
            OperationId = "GetSongById",
            Tags = new[] { "Song" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var song = songService.ObterPorId(id);
            return song == null ? NotFound() : Ok(song);
        }

        [HttpGet("por-album")]
        [SwaggerOperation(
            Summary = "Obtém músicas por ID do álbum",
            Description = "Retorna uma lista de músicas que pertencem ao álbum especificado. Se nenhuma música for encontrada, retorna 404 Not Found.",
            OperationId = "GetSongsByAlbumId",
            Tags = new[] { "Song" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPorAlbum([FromQuery] int albumId)
        {
            var songs = songService.ObterPorAlbumId(albumId);
            return songs == null || songs.Count == 0 ? NotFound() : Ok(songs);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastra uma nova música",
            Description = "Recebe um objeto de música e cadastra no sistema. Retorna a música criada com status 201 Created.",
            OperationId = "CreateSong",
            Tags = new[] { "Song" }
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] SongModel song)
        {
            if (string.IsNullOrWhiteSpace(song.Name) || string.IsNullOrWhiteSpace(song.Duration))
                return BadRequest("Nome e duração da música são obrigatórios.");

            var criada = songService.Cadastrar(song);
            return CreatedAtAction(nameof(Get), new { id = criada.Id }, criada);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza uma música existente",
            Description = "Atualiza os dados de uma música existente. Retorna 204 No Content se a atualização for bem-sucedida, ou 404 Not Found se não existir.",
            OperationId = "UpdateSong",
            Tags = new[] { "Song" }
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] SongModel song)
        {
            if (song == null || song.Id != id)
                return BadRequest("Dados inconsistentes.");

            return songService.Atualizar(song) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove uma música",
            Description = "Remove uma música com base no ID fornecido. Retorna 204 No Content se a remoção for bem-sucedida ou 404 Not Found se não existir.",
            OperationId = "DeleteSong",
            Tags = new[] { "Song" }
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            return songService.Remover(id) ? NoContent() : NotFound();
        }
    }
}
