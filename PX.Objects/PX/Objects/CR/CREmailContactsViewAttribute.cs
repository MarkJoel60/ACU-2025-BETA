// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CREmailContactsViewAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Reflection;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class CREmailContactsViewAttribute : System.Attribute
{
  private readonly BqlCommand _select;

  public CREmailContactsViewAttribute(System.Type select)
  {
    if (select == (System.Type) null)
      throw new ArgumentNullException(nameof (select));
    this._select = typeof (BqlCommand).IsAssignableFrom(select) ? BqlCommand.CreateInstance(new System.Type[1]
    {
      select
    }) : throw new ArgumentException($"type '{select.Name}' must inherit PX.Data.BqlCommand", nameof (select));
  }

  public BqlCommand Select => this._select;

  [Obsolete]
  public static PXView GetView(PXGraph graph, System.Type objType)
  {
    if (graph == null || objType == (System.Type) null)
      return (PXView) null;
    return System.Attribute.GetCustomAttribute((MemberInfo) objType, typeof (CREmailContactsViewAttribute), true) is CREmailContactsViewAttribute customAttribute && customAttribute.Select != null ? new PXView(graph, true, customAttribute.Select) : (PXView) null;
  }
}
