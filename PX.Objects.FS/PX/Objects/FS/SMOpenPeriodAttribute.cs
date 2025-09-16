// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SMOpenPeriodAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.FS;

public class SMOpenPeriodAttribute : OpenPeriodAttribute
{
  private Type _TableSourceType;
  private Type _DependenceFieldType;

  public SMOpenPeriodAttribute(
    Type sourceType,
    Type dependenceFieldType,
    Type branchSourceType = null,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    Type useMasterCalendarSourceType = null,
    Type defaultType = null,
    bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
    bool useMasterOrganizationIDByDefault = false)
    : base(typeof (Search<FinPeriod.finPeriodID, Where<Current<CreateInvoiceFilter.postTo>, NotEqual<FSPostTo.Sales_Order_Module>, And<FinPeriod.status, Equal<FinPeriod.status.open>, Or<Current<CreateInvoiceFilter.postTo>, Equal<FSPostTo.Sales_Order_Module>>>>>), sourceType, branchSourceType, branchSourceFormulaType, organizationSourceType, useMasterCalendarSourceType, defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, useMasterOrganizationIDByDefault: useMasterOrganizationIDByDefault)
  {
    this._TableSourceType = BqlCommand.GetItemType(sourceType);
    this._DependenceFieldType = dependenceFieldType;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this._TableSourceType != (Type) null))
      return;
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(this._TableSourceType, this._DependenceFieldType.Name, new PXFieldUpdated((object) this, __methodptr(SourceFieldUpdated)));
  }

  protected void SourceFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    object period = (object) (string) cache.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    cache.SetDefaultExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) PeriodIDAttribute.FormatForDisplay((string) period));
  }

  protected override OpenPeriodAttribute.PeriodValidationResult ValidateOrganizationFinPeriodStatus(
    PXCache sender,
    object row,
    FinPeriod finPeriod)
  {
    OpenPeriodAttribute.PeriodValidationResult validationResult = base.ValidateOrganizationFinPeriodStatus(sender, row, finPeriod);
    if ((string) sender.GetValue(row, this._DependenceFieldType.Name) == "SO")
      return new OpenPeriodAttribute.PeriodValidationResult();
    if (!validationResult.HasWarningOrError && finPeriod.ARClosed.GetValueOrDefault())
      validationResult = this.HandleErrorThatPeriodIsClosed(sender, finPeriod, errorMessage: "The {0} financial period of the {1} company is closed in Accounts Receivable.");
    return validationResult;
  }
}
