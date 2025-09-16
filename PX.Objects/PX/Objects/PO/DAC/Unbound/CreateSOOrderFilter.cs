// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.DAC.Unbound.CreateSOOrderFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.SO;

#nullable enable
namespace PX.Objects.PO.DAC.Unbound;

[PXHidden]
public class CreateSOOrderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [PXDBString(2, IsFixed = true)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType, Where<PX.Objects.SO.SOOrderType.active, Equal<True>, And<PX.Objects.SO.SOOrderType.behavior, In3<SOBehavior.sO, SOBehavior.tR>, And<PX.Objects.SO.SOOrderType.aRDocType, Equal<ARDocType.invoice>>>>>))]
  [PXUIField(DisplayName = "Sales Order Type", Required = true)]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDefault]
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  public virtual string OrderNbr { get; set; }

  [PXBool]
  public virtual bool? FixedCustomer { get; set; }

  [PXDefault]
  [CustomerActive(typeof (Search<BAccountR.bAccountID, Where<BAccountR.type, Equal<BAccountType.customerType>>>))]
  public virtual int? CustomerID { get; set; }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<CreateSOOrderFilter.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXDefault(null, typeof (Search<BAccount2.defLocationID, Where<BAccount2.bAccountID, Equal<Optional<CreateSOOrderFilter.customerID>>>>))]
  [PXUIField(DisplayName = "Location", Required = true)]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField]
  public virtual string CustomerOrderNbr { get; set; }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CreateSOOrderFilter.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CreateSOOrderFilter.orderNbr>
  {
  }

  public abstract class fixedCustomer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateSOOrderFilter.fixedCustomer>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CreateSOOrderFilter.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CreateSOOrderFilter.customerLocationID>
  {
  }

  public abstract class customerOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CreateSOOrderFilter.customerOrderNbr>
  {
  }
}
