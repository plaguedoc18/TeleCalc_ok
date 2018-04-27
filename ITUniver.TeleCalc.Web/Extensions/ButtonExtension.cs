using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Extensions
{
    public static class ButtonExtension
    {
        /// <summary>
        /// Сгенерировать кнопку для отправки формы с заданным именем и onclick
        /// </summary>
        /// <param name="html"></param>
        /// <param name="name"></param>
        /// <returns>html-разметка</returns>
        public static MvcHtmlString Submit(this HtmlHelper html, 
            string name,
            string onclick)
        {
            var btn = $"<input type = \"submit\" value = \"{name}\" class=\"btn btn-default\" onclick=\"{onclick}\"/>";
            return new MvcHtmlString(btn);
        }
        /// <summary>
        /// Сгенерировать кнопку для отправки формы с заданным именем
        /// </summary>
        /// <param name="html"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MvcHtmlString Submit(this HtmlHelper html,
            string name)
        {
            var btn = $"<input type = \"submit\" value = \"{name}\" class=\"btn btn-default\"/>";
            return new MvcHtmlString(btn);
        }
    }
}