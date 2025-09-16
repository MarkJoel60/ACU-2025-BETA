// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMContactType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CR;

public class CRMContactType : NotificationContactType
{
  public class ClassListAttribute : PXStringListAttribute
  {
    public ClassListAttribute()
      : base(new string[4]{ "P", "E", "C", "S" }, new string[4]
      {
        "Account Email",
        "Employee",
        "Contact",
        "Account Location Email"
      })
    {
    }
  }

  public new class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "P", "E", "C", "S" }, new string[4]
      {
        "Account Email",
        "Employee",
        "Contact",
        "Account Location Email"
      })
    {
    }
  }
}
