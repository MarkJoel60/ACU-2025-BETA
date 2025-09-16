// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARWriteOffType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR;

public class ARWriteOffType : ARDocType
{
  public static string DrCr(string DocType)
  {
    switch (DocType)
    {
      case "SMB":
        return "C";
      case "SMC":
        return "D";
      default:
        return (string) null;
    }
  }

  public new class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "SMB", "SMC" }, new string[2]
      {
        "Balance WO",
        "Credit WO"
      })
    {
    }
  }

  public class DefaultDrCrAttribute : PXDefaultAttribute
  {
    protected readonly Type _DocType;

    public DefaultDrCrAttribute(Type DocType)
    {
      this._DocType = DocType;
      this.PersistingCheck = (PXPersistingCheck) 2;
    }

    public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
    {
      string DocType = (string) sender.GetValue(e.Row, this._DocType.Name);
      if (DocType == null)
        return;
      e.NewValue = (object) ARWriteOffType.DrCr(DocType);
    }
  }
}
