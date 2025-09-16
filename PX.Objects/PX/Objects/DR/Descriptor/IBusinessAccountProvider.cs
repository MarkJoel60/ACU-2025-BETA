// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.Descriptor.IBusinessAccountProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.EP;

#nullable disable
namespace PX.Objects.DR.Descriptor;

public interface IBusinessAccountProvider
{
  EPEmployee GetEmployee(int? employeeID);

  PX.Objects.AR.SalesPerson GetSalesPerson(int? salesPersonID);

  /// <summary>
  /// Retrieve the location matching the given business account ID
  /// and business account location ID.
  /// </summary>
  PX.Objects.CR.Location GetLocation(int? businessAccountID, int? businessAccountLocationId);
}
