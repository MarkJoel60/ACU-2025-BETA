// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerContactType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.AR;

public class CustomerContactType : NotificationContactType
{
  /// <summary>
  /// Defines a list of the possible ContactType for the AR Customer <br />
  /// Namely: Primary, Billing, Shipping, Employee <br />
  /// Mostly, this attribute serves as a container <br />
  /// </summary>
  public class ClassListAttribute : PXStringListAttribute
  {
    public ClassListAttribute()
      : base(new string[4]{ "P", "B", "S", "E" }, new string[4]
      {
        "Account Email",
        "Billing",
        "Account Location Email",
        "Employee"
      })
    {
    }
  }

  public new class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[5]{ "P", "B", "S", "E", "C" }, new string[5]
      {
        "Account Email",
        "Billing",
        "Account Location Email",
        "Employee",
        "Contact"
      })
    {
    }
  }
}
