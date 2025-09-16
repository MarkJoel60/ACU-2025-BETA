// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.NotificationActivityTypeExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// Cache extension for add selector and adjust behavior of <see cref="P:PX.SM.Notification.Type" /> field.
/// </summary>
[Serializable]
public sealed class NotificationActivityTypeExt : PXCacheExtension<Notification>
{
  /// <summary>The type of the activity.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.EP.EPActivityType.Type" /> field.
  /// </value>
  [PXMergeAttributes]
  [PXSelector(typeof (Search<EPActivityType.type, Where<EPActivityType.classID, Equal<CRActivityClass.email>>>), new System.Type[] {typeof (EPActivityType.type), typeof (EPActivityType.description), typeof (EPActivityType.active)}, DescriptionField = typeof (EPActivityType.description))]
  [PXRestrictor(typeof (Where<EPActivityType.active, Equal<True>>), "Activity Type '{0}' is not active.", new System.Type[] {typeof (EPActivityType.type)})]
  [PXDefault(typeof (EPActivityType.type.email))]
  public string Type { get; set; }
}
