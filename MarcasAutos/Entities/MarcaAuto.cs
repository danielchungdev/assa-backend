namespace MarcasAutos.Entities
{
    public class MarcaAuto
    {
        //id para base de datos
        public int Id {get; set;}
        //Nombre de la marca es requerido
        public required string Nombre {get; set;}
        //Default como empty string. Creador es optional
        public string Creador {get; set;} = string.Empty;
    }
}