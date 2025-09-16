// Decompiled with JetBrains decompiler
// Type: PX.Data.PXParentUserAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXParentUserAttribute : PXParentAttribute
{
  public PXParentUserAttribute(System.Type usernameField)
    : base(BqlCommand.Compose(typeof (Select<,>), typeof (Users), typeof (Where<,>), typeof (Users.username), typeof (Equal<>), typeof (Current<>), usernameField))
  {
    if (usernameField == (System.Type) null)
      throw new ArgumentNullException(nameof (usernameField));
    if (!typeof (IBqlField).IsAssignableFrom(usernameField))
      throw new ArgumentException(PXLocalizer.LocalizeFormat("The type {0} must inherit the PX.Data.IBqlField interface.", (object) usernameField.FullName), nameof (usernameField));
  }
}
