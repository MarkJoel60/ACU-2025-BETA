// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRTaskSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class CRTaskSelectorAttribute : PXSelectorAttribute
{
  public CRTaskSelectorAttribute()
    : base(typeof (Search<CRActivity.noteID, Where<CRActivity.classID, Equal<CRActivityClass.task>, Or<CRActivity.classID, Equal<CRActivityClass.events>>>, OrderBy<Desc<CRActivity.createdDateTime>>>), new System.Type[4]
    {
      typeof (CRActivity.subject),
      typeof (CRActivity.priority),
      typeof (CRActivity.startDate),
      typeof (CRActivity.endDate)
    })
  {
    this.DescriptionField = typeof (CRActivity.subject);
  }
}
