// Decompiled with JetBrains decompiler
// Type: PagedList.Mvc.GoToFormRenderOptions
// Assembly: PagedList.Mvc, Version=4.5.0.0, Culture=neutral, PublicKeyToken=abbb863e9397c5e1
// MVID: 37097A3D-BDC5-44EC-ADA5-249267DA545B
// Assembly location: C:\Users\Kevin\source\repos\EcommerceWebApplication\packages\PagedList.Mvc.4.5.0.0\lib\net40\PagedList.Mvc.dll
// XML documentation location: C:\Users\Kevin\source\repos\EcommerceWebApplication\packages\PagedList.Mvc.4.5.0.0\lib\net40\PagedList.Mvc.xml

namespace PagedList.Mvc
{
    /// <summary>
    ///  Options for configuring the output of <see cref="T:PagedList.Mvc.HrExtensions" />.
    /// </summary>
    public class GoToFormRenderOptions
    {
        /// <summary>
        ///  The default settings, with configurable querystring key (input field name).
        /// </summary>
        public GoToFormRenderOptions(string inputFieldName)
        {
            this.LabelFormat = "Go to page:";
            this.SubmitButtonFormat = "Go";
            this.InputFieldName = inputFieldName;
            this.InputFieldType = "number";
        }

        /// <summary>The default settings.</summary>
        public GoToFormRenderOptions()
          : this("page")
        {
        }

        /// <summary>The text to show in the form's input label.</summary>
        /// <example>"Go to page:"</example>
        public string LabelFormat { get; set; }

        /// <summary>The text to show in the form's submit button.</summary>
        /// <example>"Go"</example>
        public string SubmitButtonFormat { get; set; }

        /// <summary>
        ///  The querystring key this form should submit the new page number as.
        /// </summary>
        /// <example>"page"</example>
        public string InputFieldName { get; set; }

        /// <summary>
        ///  The HTML input type for this field. Defaults to the HTML5 "number" type, but can be changed to "text" if targetting previous versions of HTML.
        /// </summary>
        /// <example>"number"</example>
        public string InputFieldType { get; set; }
    }
}
