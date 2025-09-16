// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.SalesTerritoryFieldAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// The attribute used in FK of <see cref="T:PX.Objects.CS.SalesTerritory.salesTerritoryID" /> fields.
/// </summary>
[PXDBString(15, IsUnicode = true)]
[PXUIField(DisplayName = "Sales Territory", FieldClass = "SalesTerritoryManagement")]
[PXSelector(typeof (Search2<PX.Objects.CS.SalesTerritory.salesTerritoryID, LeftJoin<PX.Objects.CS.Country, On<PX.Objects.CS.Country.countryID, Equal<PX.Objects.CS.SalesTerritory.countryID>>>>), new System.Type[] {typeof (PX.Objects.CS.SalesTerritory.salesTerritoryID), typeof (PX.Objects.CS.SalesTerritory.name), typeof (PX.Objects.CS.SalesTerritory.salesTerritoryType), typeof (PX.Objects.CS.Country.countryID), typeof (PX.Objects.CS.Country.description)}, DescriptionField = typeof (PX.Objects.CS.SalesTerritory.name))]
[PXRestrictor(typeof (Where<PX.Objects.CS.SalesTerritory.isActive, Equal<True>>), "The sales territory is inactive.", new System.Type[] {}, ShowWarning = true)]
public class SalesTerritoryFieldAttribute : AcctSubAttribute
{
}
