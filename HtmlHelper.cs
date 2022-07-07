// Decompiled with JetBrains decompiler
// Type: PagedList.Mvc.HtmlHelper
// Assembly: PagedList.Mvc, Version=4.5.0.0, Culture=neutral, PublicKeyToken=abbb863e9397c5e1
// MVID: 37097A3D-BDC5-44EC-ADA5-249267DA545B
// Assembly location: C:\Users\Kevin\source\repos\EcommerceWebApplication\packages\PagedList.Mvc.4.5.0.0\lib\net40\PagedList.Mvc.dll
// XML documentation location: C:\Users\Kevin\source\repos\EcommerceWebApplication\packages\PagedList.Mvc.4.5.0.0\lib\net40\PagedList.Mvc.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PagedList.Mvc
{
    /// <summary>
    /// 	Extension methods for generating paging controls that can operate on instances of IPagedList.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        private static TagBuilder WrapInListItem(string text)
        {
            TagBuilder tagBuilder = new TagBuilder("li");
            tagBuilder.SetInnerText(text);
            return tagBuilder;
        }

        private static TagBuilder WrapInListItem(
          TagBuilder inner,
          PagedListRenderOptions options,
          params string[] classes)
        {
            TagBuilder tagBuilder = new TagBuilder("li");
            foreach (string str in classes)
                tagBuilder.AddCssClass(str);
            if (options.FunctionToTransformEachPageLink != null)
                return options.FunctionToTransformEachPageLink(tagBuilder, inner);
            tagBuilder.InnerHtml = ((object)inner).ToString();
            return tagBuilder;
        }

        private static TagBuilder First(
          IPagedList list,
          Func<int, string> generatePageUrl,
          PagedListRenderOptions options)
        {
            TagBuilder inner = new TagBuilder(options.ATaglElementClasses)
            {
                InnerHtml = string.Format(options.LinkToFirstPageFormat, (object)1)
            };
            if (list.IsFirstPage)
                return HtmlHelperExtensions.WrapInListItem(inner, options, "PagedList-skipToFirst", "disabled");
            inner.Attributes["href"] = generatePageUrl(1);
            return HtmlHelperExtensions.WrapInListItem(inner, options, "PagedList-skipToFirst");
        }

        private static TagBuilder Previous(
          IPagedList list,
          Func<int, string> generatePageUrl,
          PagedListRenderOptions options)
        {
            int num = list.PageNumber - 1;
            TagBuilder inner = new TagBuilder(options.ATaglElementClasses)
            {
                InnerHtml = string.Format(options.LinkToPreviousPageFormat, (object)num)
            };
            inner.Attributes["rel"] = "prev";
            if (!list.HasPreviousPage)
                return HtmlHelperExtensions.WrapInListItem(inner, options, "PagedList-skipToPrevious", "disabled");
            inner.Attributes["href"] = generatePageUrl(num);
            return HtmlHelperExtensions.WrapInListItem(inner, options, "PagedList-skipToPrevious");
        }

        private static TagBuilder Page(
          int i,
          IPagedList list,
          Func<int, string> generatePageUrl,
          PagedListRenderOptions options)
        {
            Func<int, string> func = options.FunctionToDisplayEachPageNumber ?? (Func<int, string>)(pageNumber => string.Format(options.LinkToIndividualPageFormat, (object)pageNumber));
            int num = i;
            TagBuilder inner = new TagBuilder(options.ATaglElementClasses);
            inner.SetInnerText(func(num));
            if (i == list.PageNumber)
                return HtmlHelperExtensions.WrapInListItem(inner, options, "active");
            inner.Attributes["href"] = generatePageUrl(num);
            return HtmlHelperExtensions.WrapInListItem(inner, options);
        }

        private static TagBuilder Next(
          IPagedList list,
          Func<int, string> generatePageUrl,
          PagedListRenderOptions options)
        {
            int num = list.PageNumber + 1;
            TagBuilder inner = new TagBuilder(options.ATaglElementClasses)
            {
                InnerHtml = string.Format(options.LinkToNextPageFormat, (object)num)
            };
            inner.Attributes["rel"] = "next";
            if (!list.HasNextPage)
                return HtmlHelperExtensions.WrapInListItem(inner, options, "PagedList-skipToNext", "disabled");
            inner.Attributes["href"] = generatePageUrl(num);
            return HtmlHelperExtensions.WrapInListItem(inner, options, "PagedList-skipToNext");
        }

        private static TagBuilder Last(
          IPagedList list,
          Func<int, string> generatePageUrl,
          PagedListRenderOptions options)
        {
            int pageCount = list.PageCount;
            TagBuilder inner = new TagBuilder(options.ATaglElementClasses)
            {
                InnerHtml = string.Format(options.LinkToLastPageFormat, (object)pageCount)
            };
            if (list.IsLastPage)
                return HtmlHelperExtensions.WrapInListItem(inner, options, "PagedList-skipToLast", "disabled");
            inner.Attributes["href"] = generatePageUrl(pageCount);
            return HtmlHelperExtensions.WrapInListItem(inner, options, "PagedList-skipToLast");
        }

        private static TagBuilder PageCountAndLocationText(
          IPagedList list,
          PagedListRenderOptions options)
        {
            TagBuilder inner = new TagBuilder(options.ATaglElementClasses);
            inner.SetInnerText(string.Format(options.PageCountAndCurrentLocationFormat, (object)list.PageNumber, (object)list.PageCount));
            return HtmlHelperExtensions.WrapInListItem(inner, options, "PagedList-pageCountAndLocation", "disabled");
        }

        private static TagBuilder ItemSliceAndTotalText(
          IPagedList list,
          PagedListRenderOptions options)
        {
            TagBuilder inner = new TagBuilder(options.ATaglElementClasses);
            inner.SetInnerText(string.Format(options.ItemSliceAndTotalFormat, (object)list.FirstItemOnPage, (object)list.LastItemOnPage, (object)list.TotalItemCount));
            return HtmlHelperExtensions.WrapInListItem(inner, options, "PagedList-pageCountAndLocation", "disabled");
        }

        private static TagBuilder Ellipses(PagedListRenderOptions options) => HtmlHelperExtensions.WrapInListItem(new TagBuilder("a")
        {
            InnerHtml = options.EllipsesFormat
        }, options, "PagedList-ellipses", "disabled");

        /// <summary>
        /// 	Displays a configurable paging control for instances of PagedList.
        /// </summary>
        /// <param name="html">This method is meant to hook off HtmlHelper as an extension method.</param>
        /// <param name="list">The PagedList to use as the data source.</param>
        /// <param name="generatePageUrl">A function that takes the page number of the desired page and returns a URL-string that will load that page.</param>
        /// <returns>Outputs the paging control HTML.</returns>
        public static MvcHtmlString PagedListPager(
          this HtmlHelper html,
          IPagedList list,
          Func<int, string> generatePageUrl)
        {
            return html.PagedListPager(list, generatePageUrl, new PagedListRenderOptions());
        }

        /// <summary>
        /// 	Displays a configurable paging control for instances of PagedList.
        /// </summary>
        /// <param name="html">This method is meant to hook off HtmlHelper as an extension method.</param>
        /// <param name="list">The PagedList to use as the data source.</param>
        /// <param name="generatePageUrl">A function that takes the page number  of the desired page and returns a URL-string that will load that page.</param>
        /// <param name="options">Formatting options.</param>
        /// <returns>Outputs the paging control HTML.</returns>
        public static MvcHtmlString PagedListPager(
          this HtmlHelper html,
          IPagedList list,
          Func<int, string> generatePageUrl,
          PagedListRenderOptions options)
        {
            if (options.Display == PagedListDisplayMode.Never || options.Display == PagedListDisplayMode.IfNeeded && list.PageCount <= 1)
                return (MvcHtmlString)null;

            List<TagBuilder> source = new List<TagBuilder>();
            int start = 1;
            int num1 = list.PageCount;
            int count = num1;

            if (options.MaximumPageNumbersToDisplay.HasValue)
            {
                int pageCount = list.PageCount;
                int? numbersToDisplay = options.MaximumPageNumbersToDisplay;
                if ((pageCount <= numbersToDisplay.GetValueOrDefault() ? 0 : (numbersToDisplay.HasValue ? 1 : 0)) != 0)
                {
                    int num2 = options.MaximumPageNumbersToDisplay.Value;
                    start = list.PageNumber - num2 / 2;
                    if (start < 1)
                        start = 1;
                    count = num2;
                    num1 = start + count - 1;
                    if (num1 > list.PageCount)
                        start = list.PageCount - num2 + 1;
                }
            }

            if (options.DisplayLinkToFirstPage == PagedListDisplayMode.Always || options.DisplayLinkToFirstPage == PagedListDisplayMode.IfNeeded && start > 1)
                source.Add(HtmlHelperExtensions.First(list, generatePageUrl, options));

            if (options.DisplayLinkToPreviousPage == PagedListDisplayMode.Always || options.DisplayLinkToPreviousPage == PagedListDisplayMode.IfNeeded && !list.IsFirstPage)
                source.Add(HtmlHelperExtensions.Previous(list, generatePageUrl, options));

            if (options.DisplayPageCountAndCurrentLocation)
                source.Add(HtmlHelperExtensions.PageCountAndLocationText(list, options));

            if (options.DisplayItemSliceAndTotal)
                source.Add(HtmlHelperExtensions.ItemSliceAndTotalText(list, options));

            if (options.DisplayLinkToIndividualPages)
            {
                if (options.DisplayEllipsesWhenNotShowingAllPageNumbers && start > 1)
                    source.Add(HtmlHelperExtensions.Ellipses(options));
                foreach (int i in Enumerable.Range(start, count))
                {
                    if (i > start && !string.IsNullOrWhiteSpace(options.DelimiterBetweenPageNumbers))
                        source.Add(HtmlHelperExtensions.WrapInListItem(options.DelimiterBetweenPageNumbers));
                    source.Add(HtmlHelperExtensions.Page(i, list, generatePageUrl, options));
                }
                if (options.DisplayEllipsesWhenNotShowingAllPageNumbers && start + count - 1 < list.PageCount)
                    source.Add(HtmlHelperExtensions.Ellipses(options));
            }

            if (options.DisplayLinkToNextPage == PagedListDisplayMode.Always || options.DisplayLinkToNextPage == PagedListDisplayMode.IfNeeded && !list.IsLastPage)
                source.Add(HtmlHelperExtensions.Next(list, generatePageUrl, options));

            if (options.DisplayLinkToLastPage == PagedListDisplayMode.Always || options.DisplayLinkToLastPage == PagedListDisplayMode.IfNeeded && num1 < list.PageCount)
                source.Add(HtmlHelperExtensions.Last(list, generatePageUrl, options));


            if (((IEnumerable<TagBuilder>)source).Any<TagBuilder>())
            {
                if (!string.IsNullOrWhiteSpace(options.ClassToApplyToFirstListItemInPager))
                    ((IEnumerable<TagBuilder>)source).First<TagBuilder>().AddCssClass(options.ClassToApplyToFirstListItemInPager);
                if (!string.IsNullOrWhiteSpace(options.ClassToApplyToLastListItemInPager))
                    ((IEnumerable<TagBuilder>)source).Last<TagBuilder>().AddCssClass(options.ClassToApplyToLastListItemInPager);

                foreach (TagBuilder tagBuilder in source)
                {
                    foreach (string str in options.LiElementClasses ?? Enumerable.Empty<string>())
                        tagBuilder.AddCssClass(str);
                }


            }

            string str1 = ((IEnumerable<TagBuilder>)source).Aggregate<TagBuilder, StringBuilder, string>(new StringBuilder(), (Func<StringBuilder, TagBuilder, StringBuilder>)((sb, listItem) => sb.Append(((object)listItem).ToString())), (Func<StringBuilder, string>)(sb => sb.ToString()));
            TagBuilder tagBuilder1 = new TagBuilder("ul")
            {
                InnerHtml = str1
            };
            foreach (string str2 in options.UlElementClasses ?? Enumerable.Empty<string>())
                tagBuilder1.AddCssClass(str2);


            TagBuilder tagBuilder2 = new TagBuilder("div");
            foreach (string str3 in options.ContainerDivClasses ?? Enumerable.Empty<string>())
                tagBuilder2.AddCssClass(str3);


            tagBuilder2.InnerHtml = ((object)tagBuilder1).ToString();
            return new MvcHtmlString(((object)tagBuilder2).ToString());
        }

        /// <summary>
        ///  Displays a configurable "Go To Page:" form for instances of PagedList.
        /// </summary>
        /// <param name="html">This method is meant to hook off HtmlHelper as an extension method.</param>
        /// <param name="list">The PagedList to use as the data source.</param>
        /// <param name="formAction">The URL this form should submit the GET request to.</param>
        /// <returns>Outputs the "Go To Page:" form HTML.</returns>
        public static MvcHtmlString PagedListGoToPageForm(
          this HtmlHelper html,
          IPagedList list,
          string formAction)
        {
            return html.PagedListGoToPageForm(list, formAction, "page");
        }

        /// <summary>
        ///  Displays a configurable "Go To Page:" form for instances of PagedList.
        /// </summary>
        /// <param name="html">This method is meant to hook off HtmlHelper as an extension method.</param>
        /// <param name="list">The PagedList to use as the data source.</param>
        /// <param name="formAction">The URL this form should submit the GET request to.</param>
        /// <param name="inputFieldName">The querystring key this form should submit the new page number as.</param>
        /// <returns>Outputs the "Go To Page:" form HTML.</returns>
        public static MvcHtmlString PagedListGoToPageForm(
          this HtmlHelper html,
          IPagedList list,
          string formAction,
          string inputFieldName)
        {
            return html.PagedListGoToPageForm(list, formAction, new GoToFormRenderOptions(inputFieldName));
        }

        /// <summary>
        ///  Displays a configurable "Go To Page:" form for instances of PagedList.
        /// </summary>
        /// <param name="html">This method is meant to hook off HtmlHelper as an extension method.</param>
        /// <param name="list">The PagedList to use as the data source.</param>
        /// <param name="formAction">The URL this form should submit the GET request to.</param>
        /// <param name="options">Formatting options.</param>
        /// <returns>Outputs the "Go To Page:" form HTML.</returns>
        public static MvcHtmlString PagedListGoToPageForm(
          this HtmlHelper html,
          IPagedList list,
          string formAction,
          GoToFormRenderOptions options)
        {
            TagBuilder tagBuilder1 = new TagBuilder("form");
            tagBuilder1.AddCssClass("PagedList-goToPage");
            tagBuilder1.Attributes.Add("action", formAction);
            tagBuilder1.Attributes.Add("method", "get");
            TagBuilder tagBuilder2 = new TagBuilder("fieldset");
            TagBuilder tagBuilder3 = new TagBuilder("label");
            tagBuilder3.Attributes.Add("for", options.InputFieldName);
            tagBuilder3.SetInnerText(options.LabelFormat);
            TagBuilder tagBuilder4 = new TagBuilder("input");
            tagBuilder4.Attributes.Add("type", options.InputFieldType);
            tagBuilder4.Attributes.Add("name", options.InputFieldName);
            tagBuilder4.Attributes.Add("value", list.PageNumber.ToString());
            TagBuilder tagBuilder5 = new TagBuilder("input");
            tagBuilder5.Attributes.Add("type", "submit");
            tagBuilder5.Attributes.Add("value", options.SubmitButtonFormat);
            tagBuilder2.InnerHtml = ((object)tagBuilder3).ToString();
            tagBuilder2.InnerHtml += tagBuilder4.ToString((TagRenderMode)3);
            tagBuilder2.InnerHtml += tagBuilder5.ToString((TagRenderMode)3);
            tagBuilder1.InnerHtml = ((object)tagBuilder2).ToString();
            return new MvcHtmlString(((object)tagBuilder1).ToString());
        }
    }
}
