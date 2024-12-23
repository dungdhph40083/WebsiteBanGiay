namespace Application.Data.Enums
{
    public class Controller2String
    {
        /// <summary>
        /// Deletes "Controller" from a Controller's name.
        /// </summary>
        /// <param name="NameOfController"></param>
        /// <returns>Controller's name but "Controller" is gone.</returns>
        public static string Eat(string NameOfController)
        {
            return NameOfController.EndsWith("Controller") ? NameOfController[..^10] : NameOfController;
        }
    }
}
