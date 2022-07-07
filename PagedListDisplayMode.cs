// Decompiled with JetBrains decompiler
// Type: PagedList.Mvc.PagedListDisplayMode
// Assembly: PagedList.Mvc, Version=4.5.0.0, Culture=neutral, PublicKeyToken=abbb863e9397c5e1
// MVID: 37097A3D-BDC5-44EC-ADA5-249267DA545B
// Assembly location: C:\Users\Kevin\source\repos\EcommerceWebApplication\packages\PagedList.Mvc.4.5.0.0\lib\net40\PagedList.Mvc.dll
// XML documentation location: C:\Users\Kevin\source\repos\EcommerceWebApplication\packages\PagedList.Mvc.4.5.0.0\lib\net40\PagedList.Mvc.xml

namespace PagedList.Mvc
{
  /// <summary>
  /// A tri-state enum that controls the visibility of portions of the PagedList paging control.
  /// </summary>
  public enum PagedListDisplayMode
  {
    /// <summary>Always render.</summary>
    Always,
    /// <summary>Never render.</summary>
    Never,
    /// <summary>
    /// Only render when there is data that makes sense to show (context sensitive).
    /// </summary>
    IfNeeded,
  }
}
