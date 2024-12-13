namespace Application.Data.Enums
{
    public class Controller2String
    {
        /// <summary>
        /// Deletes "Controller" from a Controller's name.
        /// <br />
        /// <b>Best use</b>: Use <c><typeparamref name="nameof"/>(<paramref name="NameOfController"/>)</c> for this function!
        /// </summary>
        /// <param name="NameOfController"></param>
        /// <returns>Controller's name but "Controller" is gone.</returns>
        public static string Eat(string NameOfController)
        {
            return NameOfController.EndsWith("Controller") ? NameOfController.Substring(0, NameOfController.Length - 10) : NameOfController;
        }
    }
}
