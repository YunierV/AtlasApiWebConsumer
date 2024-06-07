using AtlasApiWeb.Data;
using AtlasApiWeb.Models;
using AtlasApiWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AtlasApiWeb.Controllers
{
    //controladores: donde se realizan los metods HTTP y las solicitudes, donde tiene lugar la mayor parte de la logica de una "api"
    [Route("api/[controller]")] //especificaremos la ruta de nuestra api
    [ApiController] //aclararemos que esto sera una api controller
    public class AtlasPhotoController : Controller
    {
        //Metdos HTTP: get, post, put, delete (con esto haremos una AVM completa de AtlasPhoto y de la AtlasphotoController en general)

        //inyecion de dependencias
        //DbContext
        public readonly AppDbContext _context;

        //ResponseDTo para realizar-enviar- respuestas a el front
        public ResponseDto _response;

        //ahora directamente la inyecion, creamos contructor de la clase y pasamos la instancia de "AppDbContext" y creamos -nuevamente- el objeto new ResponseDto
        public AtlasPhotoController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDto();
        }

        //Metodos HTTP

        //traera todos lo objetos
        //creamos el metodo HTTP GET y le decimos su url
        [HttpGet("GetPhotos")]
        //va a ser de tipo ResponseDto por que es lo que vamos a devolver al front -como respuesta-
        public ResponseDto GetPhotos()
        {
            //vamos a hacer un try/catch para separar la logica que vamos a utilizar y para asegurarnos de que el Dto va a mandar una respuesta false si sale un error (catch)
            try
            {
                //IEnumerable: es mas versatil que una lista //_context: nos valdremos de context (es la base de datos) para traer la lista de fotos y cargarlas en IEnumerable
                //IEnumerable de AtlasPhoto que va a contener todos los registros en la base de datos
                IEnumerable<AtlasPhoto> photos = _context.Photos.ToList();
                //asignamos la response (respuesta)
                _response.Data = photos;
                //_response.IsSuccess = true; //ya esta asignado por defecto

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response; //la repuesta puede ser retornada en formato Json o XMl
        }

        //traera un objeto a traves de su Id
        //metodo HTTP GET y en su url solicitaremos el id del objeto (dato de la base de datos) que se solicita
        [HttpGet("GetPhotoById/{id}")]
        public ResponseDto GetPhotoId(int id)
        {
            //vamos a hacer un try/catch para separar la logica que vamos a utilizar y para asegurarnos de que el Dto va a mandar una respuesta false si sale un error (catch)
            try
            {
                //le asignamos a "photo" el valor retornado por "SingleOrDefault" que seria el dato unico coincidente -si hay mas de un dato coincidente retorna una excepcion(error), si no hay datos coincidentes retorna el por defecto-
                var photo = _context.Photos.SingleOrDefault(p => p.Id == id);
                //asignamos la response (respuesta)
                _response.Data = photo;
                //_response.IsSuccess = true; //ya esta asignado por defecto

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response; //la repuesta puede ser retornada en formato Json o XMl
        }

        //traera un objeto a traves de su Title
        //metodo HTTP GET y en su url solicitaremos el titulo del objeto (dato de la base de datos) que se solicita
        [HttpGet("GetPhotoByTitle/{title}")]
        public ResponseDto GetPhotoTitle(string title)
        {
            //vamos a hacer un try/catch para separar la logica que vamos a utilizar y para asegurarnos de que el Dto va a mandar una respuesta false si sale un error (catch)
            try
            {
                //le asignamos a "photo" el valor retornado por "FirstOrDefault" que seria el primer dato coincidente -si hay mas de un dato coincidente retorna solo el primero, si no hay datos coincidentes retorna el por defecto-
                var photo = _context.Photos.FirstOrDefault(p => p.Title == title);
                //asignamos la response (respuesta)
                _response.Data = photo;
                //_response.IsSuccess = true; //ya esta asignado por defecto

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response; //la repuesta puede ser retornada en formato Json o XMl
        }

        //crear dato en la base de datos
        //metodo HTTP POST para agregar un nuevo dato en la DB
        [HttpPost("PostPhoto")]
        //los parametros los reciviremos del cuerpo del mensaje
        public ResponseDto PostPhoto([FromBody] AtlasPhoto photo)
        {
            try
            {
                //añadir al DbContext (base de datos)
                _context.Photos.Add(photo);
                //lo guardamos
                _context.SaveChanges();

                //lo pasamos como resultado
                _response.Data = photo;
                
            }catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        //editar dato en la base de datos
        //metodo HTTP PUT para editar o modificar las propiedades de nuestros datos en la DB
        [HttpPut("PutPhoto")]
        public ResponseDto PutPhoto([FromBody] AtlasPhoto photo)
        {
            try
            {
                //actualizamos el dato en la DB (context)
                _context.Photos.Update(photo);
                _context.SaveChanges();

                //retornamos el objeto cambiado como resultado
                _response.Data = photo;

            }catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        //eliminar dato de la base de datos
        //metodo HTTP DELETE para eliminar un dato de la DB
        [HttpDelete("DeletePhoto/{id}")]
        public ResponseDto DeletePhoto(int id)
        {
            try
            {
                //asignamos a una variable el dato que vamos a eliminar (lo buscamos en la DB)
                var photo = _context.Photos.SingleOrDefault(p => p.Id == id);
                //eliminamos el dato
                _context.Photos.Remove(photo);
                //guardamos los cambios
                _context.SaveChanges();

                _response.Data = photo;
                _response.Message = "Eliminado!";

            }catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}
