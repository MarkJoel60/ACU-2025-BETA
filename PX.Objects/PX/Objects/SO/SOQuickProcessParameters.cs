// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOQuickProcessParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;
using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable enable
namespace PX.Objects.SO;

[Serializable]
public class SOQuickProcessParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (SOOrderType.orderType))]
  [PXParent(typeof (SOQuickProcessParameters.FK.OrderType))]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.createShipment.Step), false, DisplayName = "Create Shipment")]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<BqlOperand<Current<SOOrderType.behavior>, IBqlString>.IsIn<SOBehavior.sO, SOBehavior.tR>>>))]
  [PXQuickProcess.Step.IsStartPoint(typeof (Where<Where<BqlOperand<Current<SOOrderType.behavior>, IBqlString>.IsIn<SOBehavior.sO, SOBehavior.tR>>>))]
  public virtual bool? CreateShipment { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Warehouse ID", FieldClass = "INSITE")]
  [PXQuickProcess.Step.RelatedParameter(typeof (SOQuickProcessParameters.createShipment), "siteID")]
  [OrderSiteSelector]
  public virtual int? SiteID { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.confirmShipment.Step), false, DisplayName = "Confirm Shipment")]
  [PXQuickProcess.Step.RequiresSteps(typeof (SOQuickProcessParameters.createShipment), new Type[] {})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<BqlOperand<Current<SOOrderType.behavior>, IBqlString>.IsIn<SOBehavior.sO, SOBehavior.tR>>>))]
  public virtual bool? ConfirmShipment { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.updateIN.Step), false, DisplayName = "Update IN")]
  [PXQuickProcess.Step.RequiresSteps(typeof (SOQuickProcessParameters.confirmShipment), new Type[] {})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<BqlOperand<Current<SOOrderType.behavior>, IBqlString>.IsIn<SOBehavior.sO, SOBehavior.tR>>>))]
  public virtual bool? UpdateIN { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.prepareInvoiceFromShipment.Step), false, DisplayName = "Prepare Invoice")]
  [PXQuickProcess.Step.RequiresSteps(typeof (SOQuickProcessParameters.confirmShipment), new Type[] {})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where2<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderType.behavior>, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.cM, SOBehavior.mO>>>>>.And<BqlOperand<Current<SOOrderType.aRDocType>, IBqlString>.IsNotEqual<ARDocType.noUpdate>>>, And<Where<BqlOperand<Current<SOOrderType.behavior>, IBqlString>.IsIn<SOBehavior.sO, SOBehavior.tR>>>>))]
  public virtual bool? PrepareInvoiceFromShipment { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.prepareInvoice.Step), false, DisplayName = "Prepare Invoice")]
  [PXQuickProcess.Step.IsApplicable(typeof (Where2<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderType.behavior>, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.cM, SOBehavior.mO>>>>>.And<BqlOperand<Current<SOOrderType.aRDocType>, IBqlString>.IsNotEqual<ARDocType.noUpdate>>>, And<Not<Where<BqlOperand<Current<SOOrderType.behavior>, IBqlString>.IsIn<SOBehavior.sO, SOBehavior.tR>>>>>))]
  [PXQuickProcess.Step.IsStartPoint(typeof (Where<Current<SOOrderType.behavior>, In3<SOBehavior.iN, SOBehavior.cM, SOBehavior.mO>>))]
  public virtual bool? PrepareInvoice { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.emailInvoice.Step), false, DisplayName = "Email Invoice")]
  [PXQuickProcess.Step.RequiresSteps(typeof (SOQuickProcessParameters.prepareInvoice), new Type[] {typeof (SOQuickProcessParameters.prepareInvoiceFromShipment)})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderType.behavior>, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.cM, SOBehavior.mO>>>>>.And<BqlOperand<Current<SOOrderType.aRDocType>, IBqlString>.IsNotEqual<ARDocType.noUpdate>>>>))]
  public virtual bool? EmailInvoice { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.releaseInvoice.Step), false, DisplayName = "Release Invoice")]
  [PXQuickProcess.Step.RequiresSteps(typeof (SOQuickProcessParameters.prepareInvoice), new Type[] {typeof (SOQuickProcessParameters.prepareInvoiceFromShipment)})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderType.behavior>, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.cM, SOBehavior.mO>>>>>.And<BqlOperand<Current<SOOrderType.aRDocType>, IBqlString>.IsNotEqual<ARDocType.noUpdate>>>>))]
  public virtual bool? ReleaseInvoice { get; set; }

  [PXUIField(DisplayName = "Open All Created Documents in New Tabs")]
  [PXUIEnabled(typeof (Where<SOQuickProcessParameters.autoDownloadReports, Equal<False>>))]
  [PXQuickProcess.AutoRedirectOption]
  public virtual bool? AutoRedirect { get; set; }

  [PXUIField(DisplayName = "Download Created Printable Documents")]
  [PXUIEnabled(typeof (Where<SOQuickProcessParameters.autoRedirect, Equal<True>>))]
  [PXQuickProcess.AutoDownloadReportsOption]
  public virtual bool? AutoDownloadReports { get; set; }

  public class PK : PrimaryKeyOf<SOQuickProcessParameters>.By<SOQuickProcessParameters.orderType>
  {
    public static SOQuickProcessParameters Find(
      PXGraph graph,
      string orderType,
      PKFindOptions options = 0)
    {
      return (SOQuickProcessParameters) PrimaryKeyOf<SOQuickProcessParameters>.By<SOQuickProcessParameters.orderType>.FindBy(graph, (object) orderType, options);
    }
  }

  public static class FK
  {
    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOQuickProcessParameters>.By<SOQuickProcessParameters.orderType>
    {
    }
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOQuickProcessParameters.orderType>
  {
  }

  public abstract class createShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParameters.createShipment>
  {
    [PXLocalizable]
    public class Step : PXQuickProcess.Step.Definition<SOOrderEntry>
    {
      public const string OnCreateShipmentSuccess = "Shipment <*> is created";
      public const string OnCreateShipmentFailure = "Creating shipment";

      public Step()
        : base((Expression<Func<SOOrderEntry, PXAction>>) (g => g.createShipmentIssue))
      {
      }

      public virtual string OnSuccessMessage => "Shipment <*> is created";

      public virtual string OnFailureMessage => "Creating shipment";
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOQuickProcessParameters.siteID>
  {
  }

  public abstract class confirmShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParameters.confirmShipment>
  {
    [PXLocalizable]
    public class Step : PXQuickProcess.Step.Definition<SOShipmentEntry>
    {
      public const string OnConfirmShipmentSuccess = "Shipment is confirmed";
      public const string OnConfirmShipmentFailure = "Confirming shipment";

      public Step()
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: explicit constructor call
        base.\u002Ector(Expression.Lambda<Func<SOShipmentEntry, PXAction>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (ConfirmShipmentExtension.confirmShipmentAction))), parameterExpression));
      }

      public virtual string OnSuccessMessage => "Shipment is confirmed";

      public virtual string OnFailureMessage => "Confirming shipment";
    }
  }

  public abstract class updateIN : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOQuickProcessParameters.updateIN>
  {
    [PXLocalizable]
    public class Step : PXQuickProcess.Step.Definition<SOShipmentEntry>
    {
      public const string OnUpdateINSuccess = "Inventory Document <*> is created";
      public const string OnUpdateINFailure = "Creating Inventory Document";

      public Step()
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: explicit constructor call
        base.\u002Ector(Expression.Lambda<Func<SOShipmentEntry, PXAction>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (UpdateInventoryExtension.updateIN))), parameterExpression));
      }

      public virtual string OnSuccessMessage => "Inventory Document <*> is created";

      public virtual string OnFailureMessage => "Creating Inventory Document";
    }
  }

  public abstract class prepareInvoiceFromShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParameters.prepareInvoiceFromShipment>
  {
    [PXLocalizable]
    public class Step : PXQuickProcess.Step.Definition<SOShipmentEntry>
    {
      public const string OnPrepareInvoiceSuccess = "Invoice <*> is created";
      public const string OnPrepareInvoiceFailure = "Creating Invoice";

      public Step()
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: explicit constructor call
        base.\u002Ector(Expression.Lambda<Func<SOShipmentEntry, PXAction>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (InvoiceExtension.createInvoice))), parameterExpression));
      }

      public virtual string OnSuccessMessage => "Invoice <*> is created";

      public virtual string OnFailureMessage => "Creating Invoice";
    }
  }

  public abstract class prepareInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParameters.prepareInvoice>
  {
    [PXLocalizable]
    public class Step : PXQuickProcess.Step.Definition<SOOrderEntry>
    {
      public const string OnPrepareInvoiceSuccess = "Invoice <*> is created";
      public const string OnPrepareInvoiceFailure = "Creating Invoice";

      public Step()
        : base((Expression<Func<SOOrderEntry, PXAction>>) (g => g.prepareInvoice))
      {
      }

      public virtual string OnSuccessMessage => "Invoice <*> is created";

      public virtual string OnFailureMessage => "Creating Invoice";
    }
  }

  public abstract class emailInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParameters.emailInvoice>
  {
    [PXLocalizable]
    public class Step : PXQuickProcess.Step.Definition<SOInvoiceEntry>
    {
      public const string OnEmailInvoiceSuccess = "Invoice is emailed";
      public const string OnEmailInvoiceFailure = "Emailing Invoice";

      public Step()
        : base((Expression<Func<SOInvoiceEntry, PXAction>>) (g => g.emailInvoice))
      {
      }

      public virtual string OnSuccessMessage => "Invoice is emailed";

      public virtual string OnFailureMessage => "Emailing Invoice";
    }
  }

  public abstract class releaseInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParameters.releaseInvoice>
  {
    [PXLocalizable]
    public class Step : PXQuickProcess.Step.Definition<SOInvoiceEntry>
    {
      public const string OnReleaseInvoiceSuccess = "Invoice is released";
      public const string OnReleaseInvoiceFailure = "Releasing Invoice";

      public Step()
        : base((Expression<Func<SOInvoiceEntry, PXAction>>) (g => g.release))
      {
      }

      public virtual string OnSuccessMessage => "Invoice is released";

      public virtual string OnFailureMessage => "Releasing Invoice";
    }
  }

  public abstract class autoRedirect : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParameters.autoRedirect>
  {
  }

  public abstract class autoDownloadReports : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParameters.autoDownloadReports>
  {
  }
}
