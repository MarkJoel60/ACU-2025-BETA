// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateReturnOrder.CreateReturnOrderFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCreateReturnOrder;

[PXHidden]
[Serializable]
public class CreateReturnOrderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault(typeof (Search<SOSetup.defaultReturnOrderType>))]
  [PXSelector(typeof (Search<SOOrderType.orderType>), DescriptionField = typeof (SOOrderType.descr))]
  [PXRestrictor(typeof (Where<SOOrderType.active, Equal<boolTrue>>), "Order Type '{0}' is not active.", new System.Type[] {typeof (SOOrderType.descr)})]
  [PXRestrictor(typeof (Where<BqlOperand<SOOrderType.behavior, IBqlString>.IsEqual<SOBehavior.rM>>), "The order type cannot be used.", new System.Type[] {})]
  [PXUIField(DisplayName = "Return Order Type")]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXUnboundDefault]
  [PXUIField(DisplayName = "Order Nbr.", TabOrder = 1)]
  public virtual string OrderNbr { get; set; }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CreateReturnOrderFilter.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CreateReturnOrderFilter.orderNbr>
  {
  }
}
