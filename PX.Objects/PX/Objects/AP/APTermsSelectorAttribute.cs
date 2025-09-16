// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTermsSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.AP;

[PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.vendor>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
[PXRestrictor(typeof (Where<Current<APSetup.migrationMode>, NotEqual<True>, Or<PX.Objects.CS.Terms.installmentType, NotEqual<TermsInstallmentType.multiple>>>), "Cannot be empty.", new System.Type[] {})]
public class APTermsSelectorAttribute : PXAggregateAttribute
{
}
