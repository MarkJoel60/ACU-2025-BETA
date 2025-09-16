// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSQuickProcessParameters
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSQuickProcessParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (FSSrvOrdType.srvOrdType))]
  [PXParent(typeof (Select<FSSrvOrdType, Where<FSSrvOrdType.srvOrdType, Equal<Current<FSQuickProcessParameters.srvOrdType>>>>))]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.allowInvoiceServiceOrder.Step), false)]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Current<FSServiceOrder.allowInvoice>, Equal<False>>))]
  [PXQuickProcess.Step.IsStartPoint(typeof (Where<Current<FSServiceOrder.allowInvoice>, Equal<False>>))]
  public virtual bool? AllowInvoiceServiceOrder { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.completeServiceOrder.Step), false)]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Current<FSServiceOrder.completed>, Equal<False>, And<Current<FSServiceOrder.openDoc>, Equal<True>>>))]
  [PXQuickProcess.Step.IsStartPoint(typeof (Where<Current<FSServiceOrder.completed>, Equal<False>, And<Current<FSServiceOrder.openDoc>, Equal<True>>>))]
  public virtual bool? CompleteServiceOrder { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.closeAppointment.Step), false)]
  [PXQuickProcess.Step.IsStartPoint(typeof (Where<True, Equal<True>>))]
  public virtual bool? CloseAppointment { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.closeServiceOrder.Step), false)]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSQuickProcessParameters.completeServiceOrder), new Type[] {})]
  public virtual bool? CloseServiceOrder { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.emailInvoice.Step), false)]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<True, Equal<True>>>))]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSQuickProcessParameters.prepareInvoice), new Type[] {})]
  public virtual bool? EmailInvoice { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.emailSalesOrder.Step), false)]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSQuickProcessParameters.generateInvoice), new Type[] {})]
  public virtual bool? EmailSalesOrder { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.emailSignedAppointment.Step), false)]
  public virtual bool? EmailSignedAppointment { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.generateInvoiceFromAppointment.Step), false, DisplayName = "Run Billing")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSQuickProcessParameters.closeAppointment), new Type[] {})]
  public virtual bool? GenerateInvoiceFromAppointment { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.generateInvoiceFromServiceOrder.Step), false, DisplayName = "Run Billing")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSQuickProcessParameters.allowInvoiceServiceOrder), new Type[] {})]
  public virtual bool? GenerateInvoiceFromServiceOrder { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.payBill.Step), false)]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSQuickProcessParameters.releaseBill), new Type[] {})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<True, Equal<True>>>))]
  public virtual bool? PayBill { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.prepareInvoice.Step), false)]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSQuickProcessParameters.generateInvoice), new Type[] {})]
  public virtual bool? PrepareInvoice { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.releaseBill.Step), false)]
  public virtual bool? ReleaseBill { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.releaseInvoice.Step), false)]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<True, Equal<True>>>))]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSQuickProcessParameters.prepareInvoice), new Type[] {})]
  public virtual bool? ReleaseInvoice { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.sOQuickProcess.Step), false)]
  public virtual bool? SOQuickProcess { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSQuickProcessParameters.generateInvoice.Step), true)]
  public virtual bool? GenerateInvoice
  {
    get
    {
      return new bool?(this.GenerateInvoiceFromAppointment.HasValue && (this.GenerateInvoiceFromAppointment.Value || this.GenerateInvoiceFromServiceOrder.Value));
    }
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSQuickProcessParameters.srvOrdType>
  {
  }

  public abstract class allowInvoiceServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.allowInvoiceServiceOrder>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Allow Billing";
    }
  }

  public abstract class completeServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.completeServiceOrder>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Complete";
    }
  }

  public abstract class closeAppointment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.closeAppointment>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Close";
    }
  }

  public abstract class closeServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.closeServiceOrder>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Close";
    }
  }

  public abstract class emailInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.emailInvoice>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Email Invoice";
    }
  }

  public abstract class emailSalesOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.emailSalesOrder>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Email Sales Order/Quote";
    }
  }

  public abstract class emailSignedAppointment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.emailSignedAppointment>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Email Signed Appointment";
    }
  }

  public abstract class generateInvoiceFromAppointment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.generateInvoiceFromAppointment>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Generate Invoice From Appointment";
    }
  }

  public abstract class generateInvoiceFromServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.generateInvoiceFromServiceOrder>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Generate Invoice From Service Order";
    }
  }

  public abstract class payBill : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSQuickProcessParameters.payBill>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Pay Bill";
    }
  }

  public abstract class prepareInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.prepareInvoice>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Prepare Invoice";
    }
  }

  public abstract class releaseBill : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.releaseBill>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Release Bill";
    }
  }

  public abstract class releaseInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.releaseInvoice>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Release Invoice";
    }
  }

  public abstract class sOQuickProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.sOQuickProcess>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Use Sales Order Quick Processing";
    }
  }

  public abstract class generateInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSQuickProcessParameters.generateInvoice>
  {
    public class Step : FSQuickProcessParameters.FSQuickProcessDummyStepDefinition
    {
      public override string ActionName => "Generate Invoice";
    }
  }

  public abstract class FSQuickProcessDummyStepDefinition : PXQuickProcess.Step.IDefinition
  {
    public Type Graph => typeof (SvrOrdTypeMaint);

    public abstract string ActionName { get; }

    public string OnSuccessMessage => "";

    public string OnFailureMessage => "";
  }
}
