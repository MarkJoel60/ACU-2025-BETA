// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.ReportParameters.BlanketSODetailsReportParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.ReportParameters;

[PXVirtual]
[PXCacheName("Blanket SO Details Report Parameters")]
public class BlanketSODetailsReportParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDate]
  public virtual DateTime? DateFrom { get; set; }

  [PXDate]
  public virtual DateTime? DateTo { get; set; }

  [PXDate]
  public virtual DateTime? ExpiredByDate { get; set; }

  [CustomerActive]
  public virtual int? CustomerID { get; set; }

  [PXString(2, IsKey = true, IsFixed = true)]
  [PXSelector(typeof (Search<SOOrderType.orderType, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderType.active, Equal<True>>>>>.And<BqlOperand<SOOrderType.behavior, IBqlString>.IsEqual<SOBehavior.bL>>>>), Filterable = true)]
  public virtual 
  #nullable disable
  string BlanketOrderType { get; set; }

  [PXString(2, IsKey = true, IsFixed = true)]
  [PXSelector(typeof (Search<SOOrderType.orderType, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderType.active, Equal<True>>>>>.And<BqlOperand<SOOrderType.behavior, IBqlString>.IsNotEqual<SOBehavior.bL>>>>), Filterable = true)]
  public virtual string NotBlanketOrderType { get; set; }

  [PXString(15, IsKey = true, IsUnicode = true)]
  [PXSelector(typeof (Search2<PX.Objects.SO.SOOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AR.Customer, On<KeysRelation<Field<PX.Objects.SO.SOOrder.customerID>.IsRelatedTo<PX.Objects.AR.Customer.bAccountID>.AsSimpleKey.WithTablesOf<PX.Objects.AR.Customer, PX.Objects.SO.SOOrder>, PX.Objects.AR.Customer, PX.Objects.SO.SOOrder>.And<Where<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.behavior, Equal<SOBehavior.bL>>>>, And<BqlOperand<PX.Objects.SO.SOOrder.cancelled, IBqlBool>.IsEqual<False>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.orderType, Equal<BqlField<BlanketSODetailsReportParameters.blanketOrderType, IBqlString>.AsOptional>>>>>.Or<BqlOperand<Optional<BlanketSODetailsReportParameters.blanketOrderType>, IBqlString>.IsNull>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.customerID, Equal<BqlField<BlanketSODetailsReportParameters.customerID, IBqlInt>.AsOptional>>>>>.Or<BqlOperand<Optional<BlanketSODetailsReportParameters.customerID>, IBqlInt>.IsNull>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.orderDate, GreaterEqual<BqlField<BlanketSODetailsReportParameters.dateFrom, IBqlDateTime>.AsOptional>>>>>.Or<BqlOperand<Optional<BlanketSODetailsReportParameters.dateFrom>, IBqlDateTime>.IsNull>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.orderDate, LessEqual<BqlField<BlanketSODetailsReportParameters.dateTo, IBqlDateTime>.AsOptional>>>>>.Or<BqlOperand<Optional<BlanketSODetailsReportParameters.dateTo>, IBqlDateTime>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.expireDate, LessEqual<BqlField<BlanketSODetailsReportParameters.expiredByDate, IBqlDateTime>.AsOptional>>>>>.Or<BqlOperand<Optional<BlanketSODetailsReportParameters.expiredByDate>, IBqlDateTime>.IsNull>>>, OrderBy<Desc<PX.Objects.SO.SOOrder.orderNbr>>>), Filterable = true)]
  public virtual string OrderNbr { get; set; }

  public abstract class dateFrom : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSODetailsReportParameters.dateFrom>
  {
  }

  public abstract class dateTo : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSODetailsReportParameters.dateTo>
  {
  }

  public abstract class expiredByDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSODetailsReportParameters.expiredByDate>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BlanketSODetailsReportParameters.customerID>
  {
  }

  public abstract class blanketOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSODetailsReportParameters.blanketOrderType>
  {
  }

  public abstract class notBlanketOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSODetailsReportParameters.notBlanketOrderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSODetailsReportParameters.orderNbr>
  {
  }
}
