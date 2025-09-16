// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.BAccountLocation
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<PX.Objects.CR.Location, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>>>))]
public class BAccountLocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.CR.BAccount.bAccountID))]
  [PXUIField(DisplayName = "Customer")]
  [PXSelector(typeof (Search<PX.Objects.CR.BAccount.bAccountID, Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>>>>), new System.Type[] {typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName), typeof (PX.Objects.CR.BAccount.classID), typeof (PX.Objects.CR.BAccount.status)}, SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  public virtual int? CustomerID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.CR.Location.locationID))]
  [PXUIField]
  [PXSelector(typeof (Search2<PX.Objects.CR.Location.locationID, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Location.defAddressID, Equal<PX.Objects.CR.Address.addressID>>>, Where2<Where<PX.Objects.CR.Location.bAccountID, Equal<Current<BAccountLocation.customerID>>>, And<Where<PX.Objects.CR.Location.locType, Equal<LocTypeList.customerLoc>, Or<PX.Objects.CR.Location.locType, Equal<LocTypeList.combinedLoc>>>>>, OrderBy<Asc<PX.Objects.CR.Location.locationCD>>>), new System.Type[] {typeof (PX.Objects.CR.Location.locationCD), typeof (PX.Objects.CR.Location.descr), typeof (PX.Objects.CR.Address.addressLine1), typeof (PX.Objects.CR.Address.addressLine2), typeof (PX.Objects.CR.Address.postalCode), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.state)}, SubstituteKey = typeof (PX.Objects.CR.Location.locationCD), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public virtual int? LocationID { get; set; }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Location.descr))]
  [PXUIField]
  public virtual 
  #nullable disable
  string Descr { get; set; }

  [PXDBString(BqlField = typeof (PX.Objects.CR.BAccount.acctCD))]
  [PXUIField(DisplayName = "Customer ID")]
  public virtual string CustomerCD { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.CR.Location.isActive))]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  public class PK : 
    PrimaryKeyOf<BAccountLocation>.By<BAccountLocation.customerID, BAccountLocation.locationID>
  {
    public static BAccountLocation Find(
      PXGraph graph,
      int? customerID,
      int? locationID,
      PKFindOptions options = 0)
    {
      return (BAccountLocation) PrimaryKeyOf<BAccountLocation>.By<BAccountLocation.customerID, BAccountLocation.locationID>.FindBy(graph, (object) customerID, (object) locationID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<BAccountLocation>.By<BAccountLocation.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<BAccountLocation>.By<BAccountLocation.customerID, BAccountLocation.locationID>
    {
    }
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountLocation.customerID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountLocation.locationID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountLocation.descr>
  {
  }

  public abstract class customerCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountLocation.customerCD>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BAccountLocation.isActive>
  {
  }
}
