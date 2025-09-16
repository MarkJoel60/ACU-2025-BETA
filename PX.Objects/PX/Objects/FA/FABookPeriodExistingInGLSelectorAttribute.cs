// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookPeriodExistingInGLSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.FA;

public class FABookPeriodExistingInGLSelectorAttribute(
  Type searchByDateType = null,
  Type defaultType = null,
  Type bookSourceType = null,
  bool isBookRequired = true,
  Type assetSourceType = null,
  Type dateType = null,
  Type branchSourceType = null,
  Type branchSourceFormulaType = null,
  Type organizationSourceType = null,
  Type[] fieldList = null) : FABookPeriodSelectorAttribute(typeof (Search2<FABookPeriod.finPeriodID, InnerJoin<FABook, On<FABookPeriod.bookID, Equal<FABook.bookID>>, LeftJoin<FinPeriod, On<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>, And<FABookPeriod.finPeriodID, Equal<FinPeriod.finPeriodID>, And<FABook.updateGL, Equal<True>>>>>>, Where<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>, And<Where<FinPeriod.finPeriodID, IsNotNull, Or<FABook.updateGL, NotEqual<True>>>>>>), defaultType: defaultType, bookSourceType: bookSourceType, isBookRequired: isBookRequired, assetSourceType: assetSourceType, dateType: dateType, branchSourceType: branchSourceType, branchSourceFormulaType: branchSourceFormulaType, organizationSourceType: organizationSourceType)
{
}
