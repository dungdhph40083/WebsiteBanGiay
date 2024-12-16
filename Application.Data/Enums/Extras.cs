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
            return NameOfController.EndsWith("Controller") ? NameOfController.Substring(0, NameOfController.Length - 10) : NameOfController;
        }
    }
}
