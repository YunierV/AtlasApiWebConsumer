namespace AtlasApiWeb.Models.Dto
{
    //Dto: no tiene logica, solo contiene los datos que va a comunicar la api a travez del formato Json
    /*
     * se podria crear un AtlasPhotoDto y mapearla a travez del automaper 
     * (por cuestiones de simplicidad no se hara) solo se creara una clase que se llamara ResponseDto 
     * la cual nos servira como respuesta para todas las solicitudes api que enviemos.
     */
    public class ResponseDto
    {
        public object? Data { get; set; } //puede ser nulo, no transportar nada
        public bool IsSuccess { get; set; } = true; //por defecto sera "true" pero cada que halla una cathc dentro de la solicitud o metodo HTTP se va a pasar a "false"
        public string Message { get; set; } = ""; //por defecto estara vacio

        //despues de esto pasaremos a generar el string de conexion (ConnectionStrings)
    }
}
