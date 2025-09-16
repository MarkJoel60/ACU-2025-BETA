// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOQuickProcessParametersReportsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;
using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable enable
namespace PX.Objects.SO;

[Serializable]
public sealed class SOQuickProcessParametersReportsExt : PXCacheExtension<
#nullable disable
SOQuickProcessParameters>
{
  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParametersReportsExt.printPickList.Step), false, DisplayName = "Print Pick List")]
  [PXQuickProcess.Step.RequiresSteps(typeof (SOQuickProcessParameters.createShipment), new Type[] {})]
  [PXQuickProcess.Step.IsInsertedJustAfter(typeof (SOQuickProcessParameters.createShipment))]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<BqlOperand<Current<SOOrderType.behavior>, IBqlString>.IsIn<SOBehavior.sO, SOBehavior.tR>>))]
  public bool? PrintPickList { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParametersReportsExt.printLabels.Step), false, DisplayName = "Print Labels")]
  [PXQuickProcess.Step.RequiresSteps(typeof (SOQuickProcessParameters.confirmShipment), new Type[] {})]
  [PXQuickProcess.Step.IsInsertedJustAfter(typeof (SOQuickProcessParameters.confirmShipment))]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<BqlOperand<Current<SOOrderType.behavior>, IBqlString>.IsIn<SOBehavior.sO, SOBehavior.tR>>))]
  public bool? PrintLabels { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParametersReportsExt.printConfirmation.Step), false, DisplayName = "Print Shipment Confirmation")]
  [PXQuickProcess.Step.RequiresSteps(typeof (SOQuickProcessParameters.confirmShipment), new Type[] {})]
  [PXQuickProcess.Step.IsInsertedJustAfter(typeof (SOQuickProcessParameters.confirmShipment))]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<BqlOperand<Current<SOOrderType.behavior>, IBqlString>.IsIn<SOBehavior.sO, SOBehavior.tR>>))]
  public bool? PrintConfirmation { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParametersReportsExt.printInvoice.Step), false, DisplayName = "Print Invoice")]
  [PXQuickProcess.Step.RequiresSteps(typeof (SOQuickProcessParameters.prepareInvoice), new Type[] {typeof (SOQuickProcessParameters.prepareInvoiceFromShipment)})]
  [PXQuickProcess.Step.IsInsertedJustAfter(typeof (SOQuickProcessParameters.prepareInvoice))]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderType.behavior>, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.cM, SOBehavior.mO>>>>>.And<BqlOperand<Current<SOOrderType.aRDocType>, IBqlString>.IsNotEqual<ARDocType.noUpdate>>>))]
  public bool? PrintInvoice { get; set; }

  public abstract class printPickList : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParametersReportsExt.printPickList>
  {
    [PXLocalizable]
    public class Step : PXQuickProcess.Step.Definition<SOShipmentEntry>
    {
      public const string OnPrintPickListSuccess = "<Pick List> is prepared";
      public const string OnPrintPickListFailure = "Preparing Pick List";

      public Step()
        : base((Expression<Func<SOShipmentEntry, PXAction>>) (g => g.printPickListAction))
      {
      }

      public virtual string OnSuccessMessage => "<Pick List> is prepared";

      public virtual string OnFailureMessage => "Preparing Pick List";
    }
  }

  public abstract class printLabels : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParametersReportsExt.printLabels>
  {
    [PXLocalizable]
    public class Step : PXQuickProcess.Step.Definition<SOShipmentEntry>
    {
      public const string OnPrintLabelsSuccess = "<Labels> are prepared";
      public const string OnPrintLabelsFailure = "Preparing Labels";

      public Step()
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: explicit constructor call
        base.\u002Ector(Expression.Lambda<Func<SOShipmentEntry, PXAction>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (LabelsPrinting.printLabels))), parameterExpression));
      }

      public virtual string OnSuccessMessage => "<Labels> are prepared";

      public virtual string OnFailureMessage => "Preparing Labels";
    }
  }

  public abstract class printConfirmation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParametersReportsExt.printConfirmation>
  {
    [PXLocalizable]
    public class Step : PXQuickProcess.Step.Definition<SOShipmentEntry>
    {
      public const string OnPrintShipmentSuccess = "<Shipment Confirmation> is prepared";
      public const string OnPrintShipmentFailure = "Preparing Shipment Confirmation";

      public Step()
        : base((Expression<Func<SOShipmentEntry, PXAction>>) (g => g.printShipmentConfirmation))
      {
      }

      public virtual string OnSuccessMessage => "<Shipment Confirmation> is prepared";

      public virtual string OnFailureMessage => "Preparing Shipment Confirmation";
    }
  }

  public abstract class printInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParametersReportsExt.printInvoice>
  {
    [PXLocalizable]
    public class Step : PXQuickProcess.Step.Definition<SOInvoiceEntry>
    {
      public const string OnPrintInvoiceSuccess = "<Invoice form> is prepared";
      public const string OnPrintInvoiceFailure = "Preparing Invoice form";

      public Step()
        : base((Expression<Func<SOInvoiceEntry, PXAction>>) (g => g.printInvoice))
      {
      }

      public virtual string OnSuccessMessage => "<Invoice form> is prepared";

      public virtual string OnFailureMessage => "Preparing Invoice form";
    }
  }
}
